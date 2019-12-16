///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: FlatLightingFourColors behavior base.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// FlatLightingFourColors behavior base.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base de los comportamientos FlatLightingFourColors.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingFourColorsBase : CGFFlatLightingColorBase
    {

        #region Public Variables

        public Color RimColor
        {
            get { return _rimColor; }
            set
            {
                _rimColor = value;
                SetColor("_RimColor", _rimColor);
            }
        }

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

        public Vector2 RimTextureOffset
        {
            get { return _rimTextureOffset; }
            set
            {
                _rimTextureOffset = value;
                SetTextureOffset("_RimTexture", _rimTextureOffset);
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

        public bool RimGradient
        {
            get { return _rimGradient; }
            set
            {
                _rimGradient = value;
                SetFloat("_RimGradient", _rimGradient);
            }
        }

        public Color RimTopColorGradient
        {
            get { return _rimTopColorGradient; }
            set
            {
                _rimTopColorGradient = value;
                SetColor("_RimTopColor", _rimTopColorGradient);
            }
        }

        public float RimGradientCenter
        {
            get { return _rimGradientCenter; }
            set
            {
                _rimGradientCenter = value;
                SetFloat("_RimGradientCenter", _rimGradientCenter);
            }
        }

        public float RimGradientWidth
        {
            get { return _rimGradientWidth; }
            set
            {
                _rimGradientWidth = value;
                SetFloat("_RimGradientWidth", _rimGradientWidth);
            }
        }

        public bool RimGradientRevert
        {
            get { return _rimGradientRevert; }
            set
            {
                _rimGradientRevert = value;
                SetFloat("_RimGradientRevert", _rimGradientRevert);
            }
        }

        public bool RimGradientChangeDirection
        {
            get { return _rimGradientChangeDirection; }
            set
            {
                _rimGradientChangeDirection = value;
                SetFloat("_RimGradientChangeDirection", _rimGradientChangeDirection);
            }
        }

        public float RimGradientRotation
        {
            get { return _rimGradientRotation; }
            set
            {
                _rimGradientRotation = value;
                SetFloat("_RimGradientRotation", _rimGradientRotation);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private Color _rimColor;

        [SerializeField]
        private Texture _rimTexture;

        [SerializeField]
        private Vector2 _rimTextureTiling;

        [SerializeField]
        private Vector2 _rimTextureOffset;

        [SerializeField]
        private float _rimTextureLevel;

        [SerializeField]
        private bool _rimGradient;

        [SerializeField]
        private Color _rimTopColorGradient;

        [SerializeField]
        private float _rimGradientCenter;

        [SerializeField]
        private float _rimGradientWidth;

        [SerializeField]
        private bool _rimGradientRevert;

        [SerializeField]
        private bool _rimGradientChangeDirection;

        [SerializeField]
        private float _rimGradientRotation;

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            base.CopyShaderParameter();

            RimColor = GetColor("_RimColor");

            RimTexture = GetTexture("_RimTexture");

            RimTextureTiling = GetTextureTiling("_RimTexture");

            RimTextureOffset = GetTextureOffset("_RimTexture");

            RimTextureLevel = GetFloat("_RimTextureLevel");

            RimGradient = GetBoolean("_RimGradient");

            RimTopColorGradient = GetColor("_RimTopColor");

            RimGradientCenter = GetFloat("_RimGradientCenter");

            RimGradientWidth = GetFloat("_RimGradientWidth");

            RimGradientRevert = GetBoolean("_RimGradientRevert");

            RimGradientChangeDirection = GetBoolean("_RimGradientChangeDirection");

            RimGradientRotation = GetFloat("_RimGradientRotation");

        }

        protected override void SetShaderParameters()
        {

            base.SetShaderParameters();

            RimColor = _rimColor;

            RimTexture = _rimTexture;

            RimTextureTiling = _rimTextureTiling;

            RimTextureOffset = _rimTextureOffset;

            RimTextureLevel = _rimTextureLevel;

            RimGradient = _rimGradient;

            RimTopColorGradient = _rimTopColorGradient;

            RimGradientCenter = _rimGradientCenter;

            RimGradientWidth = _rimGradientWidth;

            RimGradientRevert = _rimGradientRevert;

            RimGradientChangeDirection = _rimGradientChangeDirection;

            RimGradientRotation = _rimGradientRotation;

        }

#endregion


        #region Utility Events

        #endregion

    }

}