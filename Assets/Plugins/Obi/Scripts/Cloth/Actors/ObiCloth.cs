using UnityEngine;
using System.Collections;

namespace Obi
{
    [AddComponentMenu("Physics/Obi/Obi Cloth", 900)]
    [RequireComponent(typeof(MeshFilter))]
    public class ObiCloth : ObiClothBase
    {
        [SerializeField] protected ObiClothBlueprint m_ClothBlueprint;

        // volume constraints:
        [SerializeField] protected bool _volumeConstraintsEnabled = true;
        [SerializeField] protected float _compressionCompliance = 1;
        [SerializeField] protected float _pressure = 1;

        // tethers
        [SerializeField] protected bool _tetherConstraintsEnabled = true;
        [SerializeField] protected float _tetherCompliance = 1;
        [SerializeField] [Range(0.1f, 2)]protected float _tetherScale = 1;

        [SerializeField] protected ObiClothRenderer m_renderer;

        public override ObiActorBlueprint blueprint
        {
            get { return m_ClothBlueprint; }
        }

        public override ObiClothBlueprintBase clothBlueprintBase
        {
            get { return m_ClothBlueprint; }
        }

        public bool volumeConstraintsEnabled
        {
            get { return _volumeConstraintsEnabled; }
            set { if (value != _volumeConstraintsEnabled) { _tetherConstraintsEnabled = value; PushVolumeConstraints(_volumeConstraintsEnabled, _compressionCompliance, _pressure); } }
        }

        public float compressionCompliance
        {
            get { return _compressionCompliance; }
            set { _compressionCompliance = value; PushVolumeConstraints(_volumeConstraintsEnabled, _compressionCompliance, _pressure); }
        }

        public float pressure
        {
            get { return _pressure; }
            set { _pressure = value; PushVolumeConstraints(_volumeConstraintsEnabled, _compressionCompliance, _pressure); }
        }

        public bool tetherConstraintsEnabled
        {
            get { return _tetherConstraintsEnabled; }
            set { if (value != _tetherConstraintsEnabled) { _tetherConstraintsEnabled = value; PushTetherConstraints(_tetherConstraintsEnabled, _tetherCompliance, _tetherScale); } }
        }

        public float tetherCompliance
        {
            get { return _tetherCompliance; }
            set { _tetherCompliance = value; PushTetherConstraints(_tetherConstraintsEnabled, _tetherCompliance, _tetherScale); }
        }

        public float tetherScale
        {
            get { return _tetherScale; }
            set { _tetherScale = value; PushTetherConstraints(_tetherConstraintsEnabled, _tetherCompliance, _tetherScale); }
        }

        public ObiClothBlueprint clothBlueprint
        {
            get { return m_ClothBlueprint; }
            set{
                if (m_ClothBlueprint != value)
                {
                    RemoveFromSolver();
                    ClearState();
                    m_ClothBlueprint = value;
                    AddToSolver();
                }
            }
        }

        public override void LoadBlueprint(ObiSolver solver)
        {
            base.LoadBlueprint(solver);
            SetupRuntimeConstraints();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            SetupRuntimeConstraints();
        }

        private void SetupRuntimeConstraints()
        {
            PushDistanceConstraints(_distanceConstraintsEnabled, _stretchCompliance, _maxCompression);
            PushBendConstraints(_bendConstraintsEnabled, _bendCompliance, _maxBending);
            PushAerodynamicConstraints(_aerodynamicsEnabled, _drag, _lift);
            PushVolumeConstraints(_volumeConstraintsEnabled, _compressionCompliance, _pressure);
            PushTetherConstraints(_tetherConstraintsEnabled, _tetherCompliance, _tetherScale);
            SetSelfCollisions(m_SelfCollisions);
        }

    }

}