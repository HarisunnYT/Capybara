///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Gestor que permite al Gameobject asociado modificar los valores de los shaders que tengan la propiedad de Simulated Light.
///

using UnityEngine;

namespace CGF.Systems.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Manager that allows to the attatched gameobject sets the values of the shaders with the feature of Simulated Light.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Gestor que permite al Gameobject asociado modificar los valores de los shaders que tengan la propiedad de Simulated Light.
    /// </summary>
    /// \endspanish
    public class CGFSimulatedLightManager : CGFShaderManager
    {

        #region Public Variables

        #endregion


        #region Private Variables

        [SerializeField]
        private bool _simulatedLight = false;

        [SerializeField]
        private Texture _simulatedLightRampTexture;

        [SerializeField]
        private Vector2 _simulatedLightRampTextureTiling = Vector2.one;

        [SerializeField]
        private Vector2 _simulatedLightRampTextureOffset = Vector2.zero;

        [SerializeField]
        private float _simulatedLightLevel = 1;

        [SerializeField]
        private Vector3 _simulatedLightPosition = Vector3.zero;

        [SerializeField]
        private float _simulatedLightDistance = 10;

        [SerializeField]
        private bool _gradientRamp = false;

        [SerializeField]
        private Color _centerColor = new Color(1, 1, 1, 1);

        [SerializeField]
        private bool _useExternalColor = false;

        [SerializeField]
        private Color _externalColor = new Color(1, 0, 0, 1);

        [SerializeField]
        private bool _additiveSimulatedLight = false;

        [SerializeField]
        private float _additiveSimulatedLightLevel = 0.5f;

        [SerializeField]
        private bool _posterize = false;

        [SerializeField]
        private float _steps = 2;

        #endregion


        #region Main Methods

        protected override void Awake()
        {

            _propertyReference = "_SimulatedLight";

            base.Awake();

        }

        #endregion


        #region Utility Methods

        public override void SetParameters()
        {

            if (_materials != null)
            {

                for (int i = 0; i < _materials.Count; i++)
                {

                    SetFloat(_materials[i], "_SimulatedLight", _simulatedLight, true);

                    SetTexture(_materials[i], "_SimulatedLightRampTexture", _simulatedLightRampTexture);

                    SetTextureTiling(_materials[i], "_SimulatedLightRampTexture", _simulatedLightRampTextureTiling);

                    SetTextureOffset(_materials[i], "_SimulatedLightRampTexture", _simulatedLightRampTextureOffset);

                    SetFloat(_materials[i], "_SimulatedLightLevel", _simulatedLightLevel);

                    SetVector(_materials[i], "_SimulatedLightPosition", _simulatedLightPosition);

                    SetFloat(_materials[i], "_SimulatedLightDistance", _simulatedLightDistance);

                    SetFloat(_materials[i], "_GradientRamp", _gradientRamp);

                    SetColor(_materials[i], "_CenterColor", _centerColor);

                    SetFloat(_materials[i], "_UseExternalColor", _useExternalColor);

                    SetColor(_materials[i], "_ExternalColor", _externalColor);

                    SetFloat(_materials[i], "_AdditiveSimulatedLight", _additiveSimulatedLight);

                    SetFloat(_materials[i], "_AdditiveSimulatedLightLevel", _additiveSimulatedLightLevel);

                    SetFloat(_materials[i], "_Posterize", _posterize);

                    SetFloat(_materials[i], "_Steps", _steps);

                }

            }

        }

        #endregion


        #region Utility Events

        #endregion

    }

}