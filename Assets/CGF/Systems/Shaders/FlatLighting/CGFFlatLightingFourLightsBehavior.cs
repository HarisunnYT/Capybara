///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Behaviors that allows to the attatched gameobject sets the material with the shader CG Framework/Flat Lighting/Four Lights.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Behaviors that allows to the attatched gameobject sets the material with the shader CG Framework/Flat Lighting/Four Lights.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Comportamiento que permite al gameobject asociado configurar el material con el shader CG Framework/Flat Lighting/Four Lights.
    /// </summary>
    /// \endspanish
    public class CGFFlatLightingFourLightsBehavior : CGFFlatLightingFourLightsBase
    {

        #region Public Variables

        public float RenderMode
        {
            get { return _renderMode; }
            set
            {
                _renderMode = value;
                SetFloat("_RenderMode", _renderMode);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private float _renderMode;

        #endregion


        #region Main Methods

        protected override void Awake()
        {
            base.Awake();

            _shaderName = "CG Framework/Flat Lighting/Four Lights";
        }

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            base.CopyShaderParameter();

            RenderMode = GetFloat("_RenderMode");

        }

        protected override void SetShaderParameters()
        {

            base.SetShaderParameters();

            RenderMode = _renderMode;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}