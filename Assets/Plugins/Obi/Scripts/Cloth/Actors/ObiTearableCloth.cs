using UnityEngine;
using Unity.Profiling;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Obi
{
    [AddComponentMenu("Physics/Obi/Obi Tearable Cloth", 901)]
    [RequireComponent(typeof(MeshFilter))]
    public class ObiTearableCloth : ObiClothBase
    {
        static ProfilerMarker m_TearingPerfMarker = new ProfilerMarker("ClothTearing");

        public ObiTearableClothBlueprint m_TearableClothBlueprint;

        public bool tearingEnabled = true;
        public float tearResistanceMultiplier = 1000;                   /**< Factor that controls how much a structural cloth spring can stretch before breaking.*/
        public int tearRate = 1;
        [Range(0, 1)] public float tearDebilitation = 0.5f;

        public override ObiActorBlueprint blueprint
        {
            get { return m_TearableClothBlueprint; }
        }

        public override ObiClothBlueprintBase clothBlueprintBase
        {
            get { return m_TearableClothBlueprint; }
        }

        public ObiTearableClothBlueprint clothBlueprint
        {
            get { return m_TearableClothBlueprint; }
            set
            {
                if (m_TearableClothBlueprint != value)
                {
                    RemoveFromSolver();
                    ClearState();
                    m_TearableClothBlueprint = value;
                    AddToSolver();
                }
            }
        }

        [HideInInspector] [NonSerialized] public float[] tearResistance;    /**< Per-particle tear resistances.*/
        [HideInInspector] [NonSerialized] public int[] deformableTriangles; /**< Current deformable triangles.*/
        [HideInInspector] [NonSerialized] public HalfEdgeMesh topology;     /**< Current topology, used to store tearing information.*/

        public delegate void ClothTornCallback(ObiTearableCloth cloth, ObiConstraintTornEventArgs tearInfo);

        public event ClothTornCallback OnConstraintTorn;  /**< Called when a constraint is torn.*/

        public class ObiConstraintTornEventArgs
        {
            public StructuralConstraint edge;       /**< info about the edge being torn.*/
            public int particleIndex;   /**< index of the particle being torn*/
            public List<HalfEdgeMesh.Face> updatedFaces;

            public ObiConstraintTornEventArgs(StructuralConstraint edge, int particle, List<HalfEdgeMesh.Face> updatedFaces)
            {
                this.edge = edge;
                this.particleIndex = particle;
                this.updatedFaces = updatedFaces;
            }
        }

        public override void LoadBlueprint(ObiSolver solver)
        {
            // Grab a copy of the serialized topology reference. This happens when duplicating a cloth.
            if (topology != null && topology.ContainsData)
                topology = new HalfEdgeMesh(topology);
            // Or a copy of the shared topology, if there is no valid reference to a topology.
            else if (m_TearableClothBlueprint != null && m_TearableClothBlueprint.Topology != null)
                topology = new HalfEdgeMesh(m_TearableClothBlueprint.Topology);

            //Copy tear resistance array:
            tearResistance = new float[m_TearableClothBlueprint.tearResistance.Length];
            for (int i = 0; i < tearResistance.Length; ++i)
                tearResistance[i] = m_TearableClothBlueprint.tearResistance[i];

            //Copy deformable triangles array:
            deformableTriangles = new int[m_TearableClothBlueprint.deformableTriangles.Length];
            for (int i = 0; i < deformableTriangles.Length; ++i)
                deformableTriangles[i] = m_TearableClothBlueprint.deformableTriangles[i];

            base.LoadBlueprint(solver);

            SetupRuntimeConstraints();
        }

        private void SetupRuntimeConstraints()
        {
            PushDistanceConstraints(_distanceConstraintsEnabled, _stretchCompliance, _maxCompression);
            PushBendConstraints(_bendConstraintsEnabled, _bendCompliance, _maxBending);
            PushAerodynamicConstraints(_aerodynamicsEnabled, _drag, _lift);
            SetSelfCollisions(selfCollisions);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            SetupRuntimeConstraints();
        }

        public override void Substep(float stepTime)
        {
            base.Substep(stepTime);

            if (isActiveAndEnabled && tearingEnabled)
                ApplyTearing();
        }

        private void ApplyTearing()
        {
            using (m_TearingPerfMarker.Auto())
            {

                List<StructuralConstraint> tornEdges = new List<StructuralConstraint>();

                var dc = GetConstraintsByType(Oni.ConstraintType.Distance) as ObiConstraints<ObiDistanceConstraintsBatch>;
                for (int j = 0; j < dc.batches.Count; ++j)
                {
                    var batch = dc.batches[j] as ObiDistanceConstraintsBatch;

                    float[] forces = new float[batch.activeConstraintCount];
                    Oni.GetBatchConstraintForces(batch.oniBatch, forces, batch.activeConstraintCount, 0);

                    for (int i = 0; i < forces.Length; i++)
                    {
                        float p1Resistance = tearResistance[solver.particleToActor[batch.particleIndices[i * 2]].indexInActor];
                        float p2Resistance = tearResistance[solver.particleToActor[batch.particleIndices[i * 2 + 1]].indexInActor];

                        // average particle resistances:
                        float resistance = (p1Resistance + p2Resistance) * 0.5f * tearResistanceMultiplier;

                        if (-forces[i] > resistance)
                        { // units are newtons.
                            tornEdges.Add(new StructuralConstraint(batch, i, forces[i]));
                        }
                    }
                }

                if (tornEdges.Count > 0)
                {

                    // sort edges by tear force:
                    tornEdges.Sort(delegate (StructuralConstraint x, StructuralConstraint y)
                    {
                        return x.force.CompareTo(y.force);
                    });

                    int tornCount = 0;
                    for (int i = 0; i < tornEdges.Count; i++)
                    {
                        if (Tear(tornEdges[i]))
                            tornCount++;
                        if (tornCount >= tearRate)
                            break;
                    }

                    // update solver deformable triangle indices:
                    if (tornCount > 0)
                        UpdateDeformableTriangles();

                }

            }

        }

        /**
         * Tears a cloth distance constraint, affecting both the physical representation of the cloth and its mesh.
         */
        public bool Tear(StructuralConstraint edge)
        {
            // don't allow splitting if there are no free particles left in the pool.
            if (activeParticleCount >= m_TearableClothBlueprint.particleCount)
                return false;

            // get particle indices at both ends of the constraint:
            ParticlePair indices = edge.batchIndex.GetParticleIndices(edge.constraintIndex);

            // Try to perform a split operation on the topology. If we cannot perform it, bail out.
            Vector3 point, normal;
            HashSet<int> updatedHalfEdges = new HashSet<int>();
            List<HalfEdgeMesh.Face> updatedFaces = new List<HalfEdgeMesh.Face>();
            if (!TopologySplitAttempt(ref indices.first, ref indices.second, out point, out normal, updatedFaces, updatedHalfEdges))
                return false;

            int splitActorIndex = solver.particleToActor[indices.first].indexInActor;

            // Weaken edges around the cut:
            WeakenCutPoint(splitActorIndex, point, normal);

            // split the particle in two, adding a new active particle:
            SplitParticle(splitActorIndex);

            // update constraints:
            UpdateTornDistanceConstraints(updatedHalfEdges);
            UpdateTornBendConstraints(indices.first);

            if (OnConstraintTorn != null)
                OnConstraintTorn(this, new ObiConstraintTornEventArgs(edge, splitActorIndex, updatedFaces));

            return true;
        }


        private bool TopologySplitAttempt(ref int splitSolverIndex,
                                          ref int intactSolverIndex,
                                          out Vector3 point,
                                          out Vector3 normal,
                                          List<HalfEdgeMesh.Face> updatedFaces,
                                          HashSet<int> updatedHalfEdges)
        {

            // we will first try to split the particle with higher mass, so swap them if needed.
            if (m_Solver.invMasses[splitSolverIndex] > m_Solver.invMasses[intactSolverIndex])
                ObiUtils.Swap(ref splitSolverIndex, ref intactSolverIndex);

            // Calculate the splitting plane:
            point = m_Solver.positions[splitSolverIndex];
            Vector3 v2 = m_Solver.positions[intactSolverIndex];
            normal = (v2 - point).normalized;

            // Try to split the vertex at that particle. 
            // If we cannot not split the higher mass particle, try the other one. If that fails too, we cannot tear this edge.
            if (m_Solver.invMasses[splitSolverIndex] == 0 ||
                !SplitTopologyAtVertex(solver.particleToActor[splitSolverIndex].indexInActor, new Plane(normal, point), updatedFaces, updatedHalfEdges))
            {
                // Try to split the other particle:
                ObiUtils.Swap(ref splitSolverIndex, ref intactSolverIndex);

                point = m_Solver.positions[splitSolverIndex];
                v2 = m_Solver.positions[intactSolverIndex];
                normal = (v2 - point).normalized;

                if (m_Solver.invMasses[splitSolverIndex] == 0 ||
                    !SplitTopologyAtVertex(solver.particleToActor[splitSolverIndex].indexInActor, new Plane(normal, point), updatedFaces, updatedHalfEdges))
                    return false;
            }
            return true;
        }

        private void SplitParticle(int splitActorIndex)
        {
            int splitSolverIndex = solverIndices[splitActorIndex];
            int newSolverIndex = solverIndices[activeParticleCount];

            // halve the original particle's mass and radius:
            m_Solver.invMasses[splitSolverIndex] *= 2;
            m_Solver.principalRadii[splitSolverIndex] *= 0.5f;

            // create a copy of the original particle:
            tearResistance[activeParticleCount] = tearResistance[splitActorIndex];

            CopyParticle(splitActorIndex, activeParticleCount);
            ActivateParticle(activeParticleCount);
        }

        private void WeakenCutPoint(int splitActorIndex, Vector3 point, Vector3 normal)
        {

            int weakPt1 = -1;
            int weakPt2 = -1;
            float weakestValue = float.MaxValue;
            float secondWeakestValue = float.MaxValue;

            foreach (HalfEdgeMesh.Vertex v in topology.GetNeighbourVerticesEnumerator(topology.vertices[splitActorIndex]))
            {
                Vector3 neighbour = m_Solver.positions[solverIndices[v.index]];
                float weakness = Mathf.Abs(Vector3.Dot(normal, (neighbour - point).normalized));

                if (weakness < weakestValue)
                {
                    secondWeakestValue = weakestValue;
                    weakestValue = weakness;
                    weakPt2 = weakPt1;
                    weakPt1 = v.index;
                }
                else if (weakness < secondWeakestValue)
                {
                    secondWeakestValue = weakness;
                    weakPt2 = v.index;
                }
            }

            // reduce tear resistance at the weak spots of the cut, to encourage coherent tear formation.
            if (weakPt1 >= 0) tearResistance[weakPt1] *= 1 - tearDebilitation;
            if (weakPt2 >= 0) tearResistance[weakPt2] *= 1 - tearDebilitation;
        }

        private void ClassifyFaces(HalfEdgeMesh.Vertex vertex,
                                   Plane plane,
                                   List<HalfEdgeMesh.Face> side1,
                                   List<HalfEdgeMesh.Face> side2)
        {
            foreach (HalfEdgeMesh.Face face in topology.GetNeighbourFacesEnumerator(vertex))
            {
                HalfEdgeMesh.HalfEdge e1 = topology.halfEdges[face.halfEdge];
                HalfEdgeMesh.HalfEdge e2 = topology.halfEdges[e1.nextHalfEdge];
                HalfEdgeMesh.HalfEdge e3 = topology.halfEdges[e2.nextHalfEdge];

                // Skip this face if it doesn't contain the vertex being split.
                // This can happen because edge pair links are not updated in a vertex split operation,
                // so split vertices still "see" faces at the other side of the cut as adjacent.
                if (e1.endVertex != vertex.index &&
                    e2.endVertex != vertex.index &&
                    e3.endVertex != vertex.index)
                    continue;

                // calculate actual face center from deformed vertex positions:
                Vector3 faceCenter = (m_Solver.positions[solverIndices[e1.endVertex]] +
                                      m_Solver.positions[solverIndices[e2.endVertex]] +
                                      m_Solver.positions[solverIndices[e3.endVertex]]) * 0.33f;

                if (plane.GetSide(faceCenter))
                    side1.Add(face);
                else
                    side2.Add(face);
            }
        }

        private bool SplitTopologyAtVertex(int vertexIndex,
                                           Plane plane,
                                           List<HalfEdgeMesh.Face> updatedFaces,
                                           HashSet<int> updatedEdgeIndices)
        {
            if (vertexIndex < 0 || vertexIndex >= topology.vertices.Count)
                return false;

            updatedFaces.Clear();
            updatedEdgeIndices.Clear();
            HalfEdgeMesh.Vertex vertex = topology.vertices[vertexIndex];

            // classify adjacent faces depending on which side of the plane they're at:
            var otherSide = new List<HalfEdgeMesh.Face>();
            ClassifyFaces(vertex, plane, updatedFaces, otherSide);

            // guard against pathological case in which all particles are in one side of the plane:
            if (otherSide.Count == 0 || updatedFaces.Count == 0)
                return false;

            // create a new vertex:
            var newVertex = new HalfEdgeMesh.Vertex();
            newVertex.position = vertex.position;
            newVertex.index = topology.vertices.Count;
            newVertex.halfEdge = vertex.halfEdge;

            // rearrange edges at the updated side:
            foreach (HalfEdgeMesh.Face face in updatedFaces)
            {
                // find half edges that start and end at the split vertex:
                HalfEdgeMesh.HalfEdge e1 = topology.halfEdges[face.halfEdge];
                HalfEdgeMesh.HalfEdge e2 = topology.halfEdges[e1.nextHalfEdge];
                HalfEdgeMesh.HalfEdge e3 = topology.halfEdges[e2.nextHalfEdge];

                var in_ = e1;
                var out_ = e2;

                if (e1.endVertex == vertex.index)
                    in_ = e1;
                else if (topology.GetHalfEdgeStartVertex(e1) == vertex.index)
                    out_ = e1;

                if (e2.endVertex == vertex.index)
                    in_ = e2;
                else if (topology.GetHalfEdgeStartVertex(e2) == vertex.index)
                    out_ = e2;

                if (e3.endVertex == vertex.index)
                    in_ = e3;
                else if (topology.GetHalfEdgeStartVertex(e3) == vertex.index)
                    out_ = e3;

                // stitch edges to new vertex:
                in_.endVertex = newVertex.index;
                topology.halfEdges[in_.index] = in_;
                newVertex.halfEdge = out_.index;

                // store edges to be updated:
                updatedEdgeIndices.UnionWith(new int[]
                {
                    in_.index, in_.pair, out_.index, out_.pair
                });
            }

            // add new vertex:
            topology.vertices.Add(newVertex);
            topology.restNormals.Add(topology.restNormals[vertexIndex]);
            topology.restOrientations.Add(topology.restOrientations[vertexIndex]);

            //TODO: update mesh info. (mesh cannot be closed now)

            return true;
        }


        private void UpdateTornDistanceConstraints(HashSet<int> updatedHalfEdges)
        {
            var distanceConstraints = GetConstraintsByType(Oni.ConstraintType.Distance) as ObiConstraints<ObiDistanceConstraintsBatch>;

            foreach (int halfEdgeIndex in updatedHalfEdges)
            {
                HalfEdgeMesh.HalfEdge e = topology.halfEdges[halfEdgeIndex];
                Vector2Int constraintDescriptor = m_TearableClothBlueprint.distanceConstraintMap[halfEdgeIndex];

                // skip edges with no associated constraint (border half-edges)
                if (constraintDescriptor.x > -1)
                {
                    // get batch and index of the constraint:
                    var batch = distanceConstraints.batches[constraintDescriptor.x] as ObiDistanceConstraintsBatch;
                    int index = batch.GetConstraintIndex(constraintDescriptor.y);

                    // update constraint particle indices:
                    batch.particleIndices[index * 2] = solverIndices[topology.GetHalfEdgeStartVertex(e)];
                    batch.particleIndices[index * 2 + 1] = solverIndices[e.endVertex];

                    // make sure the constraint is active, in case it is a newly added one.
                    batch.ActivateConstraint(index);
                }

                // update deformable triangles:
                if (e.indexInFace > -1)
                {
                    deformableTriangles[e.face * 3 + e.indexInFace] = e.endVertex;
                }
            }
        }

        private void UpdateTornBendConstraints(int splitSolverIndex)
        {
            var bendConstraints = GetConstraintsByType(Oni.ConstraintType.Bending) as ObiConstraints<ObiBendConstraintsBatch>;

            foreach (ObiBendConstraintsBatch batch in bendConstraints.batches)
            {
                // iterate in reverse order so that swapping due to deactivation does not cause us to skip constraints.
                for (int i = batch.activeConstraintCount - 1; i >= 0; --i)
                {
                    if (batch.particleIndices[i * 3] == splitSolverIndex ||
                        batch.particleIndices[i * 3 + 1] == splitSolverIndex ||
                        batch.particleIndices[i * 3 + 2] == splitSolverIndex)
                    {
                        batch.DeactivateConstraint(i);
                    }
                }
            }
        }

        public override void UpdateDeformableTriangles()
        {
            if (deformableTriangles != null)
            {
                // Send deformable triangle indices to the solver:
                int[] solverTriangles = new int[deformableTriangles.Length];
                for (int i = 0; i < deformableTriangles.Length; ++i)
                {
                    solverTriangles[i] = solverIndices[deformableTriangles[i]];
                }
                Oni.SetDeformableTriangles(m_Solver.OniSolver, solverTriangles, solverTriangles.Length / 3, trianglesOffset);
            }
        }

        public void OnDrawGizmosSelected()
        {

            /*if (solver == null || !isLoaded) return;

            Color[] co = new Color[12]{
                Color.red,
                Color.yellow,
                Color.blue,
                Color.white,
                Color.black,
                Color.green,
                Color.cyan,
                Color.magenta,
                Color.gray,
                new Color(1,0.7f,0.1f),
                new Color(0.1f,0.6f,0.5f),
                new Color(0.8f,0.1f,0.6f)
            };

            var constraints = GetConstraintsByType(Oni.ConstraintType.Distance) as ObiConstraints<ObiDistanceConstraintsBatch>;


            int j = 0;
            foreach (ObiDistanceConstraintsBatch batch in constraints.batches){

                //Gizmos.color = Color.green;//co[j%12];



                for (int i = 0; i < batch.activeConstraintCount; ++i)
                {

                    Gizmos.color = new Color(0, 0, 1, 0.75f);//co[j % 12];
                    if (j == btch && i == ctr)
                        Gizmos.color = Color.green;

                    Gizmos.DrawLine(solver.positions[batch.particleIndices[i*2]],
                                    solver.positions[batch.particleIndices[i*2+1]]);
                }
                j++;
            }




            /*if (!InSolver) return;

            var constraints = GetConstraints(Oni.ConstraintType.Bending) as ObiRuntimeConstraints<ObiBendConstraintsBatch>;

            int j = 0;
            foreach (ObiBendConstraintsBatch batch in constraints.GetBatches())
            {

                for (int i = 0; i < batch.activeConstraintCount; ++i)
                {
                    Gizmos.color = new Color(1,0,0,0.2f);//co[j % 12];
                    if (j == btch && i == ctr)
                        Gizmos.color = Color.green;
                    
                    Gizmos.DrawLine(GetParticlePosition(batch.springIndices[i * 2]),
                                    GetParticlePosition(batch.springIndices[i * 2 + 1]));
                }
                j++;
            }*/


        }

        int btch = 0;
        int ctr = 0;
        public void Update()
        {

            /*var constraints = GetConstraintsByType(Oni.ConstraintType.Distance) as ObiRuntimeConstraints<ObiDistanceConstraintsBatch>;

            if (Input.GetKeyDown(KeyCode.UpArrow)){
                ctr++;
                if (ctr >= constraints.GetBatches()[btch].activeConstraintCount)
                {
                    btch++;
                    ctr = 0;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ctr--;
                if (ctr < 0)
                {
                    btch--;
                    ctr = constraints.GetBatches()[btch].activeConstraintCount-1;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)) {

                Tear(new StructuralConstraint(constraints.GetBatches()[btch] as IStructuralConstraintBatch,ctr,0));
                solver.UpdateActiveParticles();

                UpdateDeformableTriangles();
            }*/

        }

    }

}