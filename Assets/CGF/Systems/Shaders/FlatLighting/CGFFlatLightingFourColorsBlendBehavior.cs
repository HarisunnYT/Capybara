///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Behaviors that allows to the attatched gameobject sets the material with the shader CG Framework/Flat Lighting/Four Colors Blend.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Behaviors that allows to the attatched gameobject sets the material with the shader CG Framework/Flat Lighting/Four Colors Blend.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Comportamiento que permite al gameobject asociado configurar el material con el shader CG Framework/Flat Lighting/Four Colors Blend.
    /// </summary>
    /// \endspanish
    public class CGFFlatLightingFourColorsBlendBehavior : CGFFlatLightingFourColorsBase
    {

        #region Public Variables

        public float BlendMode
        {
            get { return _blendMode; }
            set
            {
                _blendMode = value;
                SetFloat("_BlendMode", _blendMode);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private float _blendMode;

        #endregion


        #region Main Methods

        protected override void Awake()
        {

            base.Awake();

            _shaderName = "CG Framework/Flat Lighting/Four Colors Blend";

        }

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {
            base.CopyShaderParameter();

            BlendMode = GetFloat("_BlendMode");
        }

        protected override void SetShaderParameters()
        {
            base.SetShaderParameters();

            BlendMode = _blendMode;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}