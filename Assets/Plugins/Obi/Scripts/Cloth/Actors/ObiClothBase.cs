using UnityEngine;
using System;
using System.Collections;

namespace Obi
{
    public abstract class ObiClothBase : ObiActor
    {

        [SerializeField] protected bool m_SelfCollisions = false;

        // distance constraints:
        [SerializeField] protected bool _distanceConstraintsEnabled = true;
        [SerializeField] protected float _stretchCompliance = 0;
        [SerializeField] [Range(0, 1)] protected float _maxCompression = 0;

        // bend constraints:
        [SerializeField] protected bool _bendConstraintsEnabled = true;
        [SerializeField] protected float _bendCompliance = 0;
        [SerializeField] [Range(0, 0.1f)] protected float _maxBending = 0;

        // aerodynamics
        [SerializeField] protected bool _aerodynamicsEnabled = true;
        [SerializeField] protected float _drag = 0.05f;
        [SerializeField] protected float _lift = 0.05f;

        [HideInInspector] [NonSerialized] protected int trianglesOffset = 0;   /**< Offset of deformable triangles in curent solver*/

        public abstract ObiClothBlueprintBase clothBlueprintBase
        {
            get;
        }

        public bool selfCollisions
        {
            get { return m_SelfCollisions; }
            set { if (value != m_SelfCollisions) { m_SelfCollisions = value; SetSelfCollisions(m_SelfCollisions); } }
        }

        public bool distanceConstraintsEnabled
        {
            get { return _distanceConstraintsEnabled; }
            set { if (value != _distanceConstraintsEnabled) { _distanceConstraintsEnabled = value; PushDistanceConstraints(_distanceConstraintsEnabled, _stretchCompliance, _maxCompression); ; } }
        }

        public float stretchCompliance
        {
            get { return _stretchCompliance; }
            set { _stretchCompliance = value; PushDistanceConstraints(_distanceConstraintsEnabled, _stretchCompliance, _maxCompression); }
        }

        public float maxCompression
        {
            get { return _maxCompression; }
            set { _maxCompression = value; PushDistanceConstraints(_distanceConstraintsEnabled, _stretchCompliance, _maxCompression); }
        }

        public bool bendConstraintsEnabled
        {
            get { return _bendConstraintsEnabled; }
            set { if (value != _bendConstraintsEnabled) { _bendConstraintsEnabled = value; PushBendConstraints(_bendConstraintsEnabled, _bendCompliance, _maxBending); } }
        }

        public float bendCompliance
        {
            get { return _bendCompliance; }
            set { _bendCompliance = value; PushBendConstraints(_bendConstraintsEnabled, _bendCompliance, _maxBending); }
        }

        public float maxBending
        {
            get { return _maxBending; }
            set { _maxBending = value; PushBendConstraints(_bendConstraintsEnabled, _bendCompliance, _maxBending); }
        }

        public bool aerodynamicsEnabled
        {
            get { return _aerodynamicsEnabled; }
            set { if (value != _aerodynamicsEnabled) { _aerodynamicsEnabled = value; PushAerodynamicConstraints(_aerodynamicsEnabled, _drag, _lift); } }
        }

        public float drag
        {
            get { return _drag; }
            set { _drag = value; PushAerodynamicConstraints(_aerodynamicsEnabled, _drag, _lift); }
        }

        public float lift
        {
            get { return _lift; }
            set { _lift = value; PushAerodynamicConstraints(_aerodynamicsEnabled, _drag, _lift); }
        }

        public override bool usesCustomExternalForces
        {
            get { return true; }
        }

		public override void LoadBlueprint(ObiSolver solver)
        {
            base.LoadBlueprint(solver);

            // find our offset in the deformable triangles array.
            trianglesOffset = Oni.GetDeformableTriangleCount(m_Solver.OniSolver);

            // Send deformable triangle indices to the solver:
            UpdateDeformableTriangles();

		}

		public override void UnloadBlueprint(ObiSolver solver)
		{
            int index = m_Solver.actors.IndexOf(this);

            if (index >= 0 && blueprint != null && clothBlueprintBase.deformableTriangles != null)
            {
                // remove triangles:
                Oni.RemoveDeformableTriangles(m_Solver.OniSolver, clothBlueprintBase.deformableTriangles.Length / 3, trianglesOffset);

                // update all following actor's triangle offset:
                for (int i = index + 1; i < m_Solver.actors.Count; i++)
                {
                    ObiClothBase clothActor = solver.actors[i] as ObiClothBase;
                    if (clothActor != null)
                        clothActor.trianglesOffset -= clothBlueprintBase.deformableTriangles.Length / 3;
                }
            }

            base.UnloadBlueprint(solver);
		}

		public virtual void UpdateDeformableTriangles()
        {
            if (clothBlueprintBase != null && clothBlueprintBase.deformableTriangles != null)
            {
                // Send deformable triangle indices to the solver:
                int[] solverTriangles = new int[clothBlueprintBase.deformableTriangles.Length];
                for (int i = 0; i < clothBlueprintBase.deformableTriangles.Length; ++i)
                {
                    solverTriangles[i] = solverIndices[clothBlueprintBase.deformableTriangles[i]];
                }
                Oni.SetDeformableTriangles(m_Solver.OniSolver, solverTriangles, solverTriangles.Length / 3, trianglesOffset);
            }
        }
    }
}
