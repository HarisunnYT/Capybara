///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: FlatLightingFourLight behavior base.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// FlatLightingFourLight behavior base.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base de los comportamientos FlatLightingFourLight.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingFourLightsBase : CGFFlatLightingLightBase
    {

        #region Public Variables

        public Texture RimTexture
        {
            get { return _rimTexture; }
            set
            {
                _rimTexture = value;
                SetTexture("_RimTexture", _rimTexture);
            }
        }

        public Vector2 RimTextureTiling
        {
            get { return _rimTextureTiling; }
            set
            {
                _rimTextureTiling = value;
                SetTextureTiling("_RimTexture", _rimTextureTiling);
            }
        }

        public float RimTextureLevel
        {
            get { return _rimTextureLevel; }
            set
            {
                _rimTextureLevel = value;
                SetFloat("_RimTextureLevel", _rimTextureLevel);
            }
        }

        public Vector2 RimTextureOffset
        {
            get { return _rimTextureOffset; }
            set
            {
                _rimTextureOffset = value;
                SetTextureOffset("_RimTexture", _rimTextureOffset);
            }
        }

        public float RimLightLevel
        {
            get { return _rimLightLevel; }
            set
            {
                _rimLightLevel = value;
                SetFloat("_RimLightLevel", _rimLightLevel);
            }
        }

        public float RimOpacityLevel
        {
            get { return _rimOpacityLevel; }
            set
            {
                _rimOpacityLevel = value;
                SetFloat("_RimOpacityLevel", _rimOpacityLevel);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private Texture _rimTexture;

        [SerializeField]
        private Vector2 _rimTextureTiling;

        [SerializeField]
        private float _rimTextureLevel;

        [SerializeField]
        private Vector2 _rimTextureOffset;

        [SerializeField]
        private float _rimLightLevel;

        [SerializeField]
        private float _rimOpacityLevel;

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            base.CopyShaderParameter();

            RimTexture = GetTexture("_RimTexture"); ;

            RimTextureLevel = GetFloat("_RimTextureLevel");

            RimTextureTiling = GetTextureTiling("_RimTexture");

            RimTextureOffset = GetTextureOffset("_RimTexture");

            RimLightLevel = GetFloat("_RimLightLevel");

            RimOpacityLevel = GetFloat("_RimOpacityLevel");

        }

        protected override void SetShaderParameters()
        {

            base.SetShaderParameters();

            RimTexture = _rimTexture;

            RimTextureLevel = _rimTextureLevel;

            RimTextureTiling = _rimTextureTiling;

            RimTextureOffset = _rimTextureOffset;

            RimLightLevel = _rimLightLevel;

            RimOpacityLevel = _rimOpacityLevel;

        }

        #endregion

        #region Utility Events

        #endregion

    }

}