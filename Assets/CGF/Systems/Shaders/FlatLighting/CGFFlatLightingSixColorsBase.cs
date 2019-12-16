///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: FlatLightingSixColors behavior base.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// FlatLightingSixColors behavior base.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base de los comportamientos FlatLightingSixColors.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingSixColorsBase : CGFFlatLightingColorBase
    {

        #region Public Variables

        public Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                SetColor("_BackColor", _backColor);
            }
        }

        public Texture BackTexture
        {
            get { return _backTexture; }
            set
            {
                _backTexture = value;
                SetTexture("_BackTexture", _backTexture);
            }
        }

        public Vector2 BackTextureTiling
        {
            get { return _backTextureTiling; }
            set
            {
                _backTextureTiling = value;
                SetTextureTiling("_BackTexture", _backTextureTiling);
            }
        }

        public Vector2 BackTextureOffset
        {
            get { return _backTextureOffset; }
            set
            {
                _backTextureOffset = value;
                SetTextureOffset("_BackTexture", _backTextureOffset);
            }
        }

        public float BackTextLevel
        {
            get { return _backTextLevel; }
            set
            {
                _backTextLevel = value;
                SetFloat("_BackTextureLevel", _backTextLevel);
            }
        }

        public bool BackGradient
        {
            get { return _backGradient; }
            set
            {
                _backGradient = value;
                SetFloat("_BackGradient", _backGradient);
            }
        }

        public Color BackTopColorGradient
        {
            get { return _backTopColorGradient; }
            set
            {
                _backTopColorGradient = value;
                SetColor("_BackTopColor", _backTopColorGradient);
            }
        }

        public float BackGradientCenter
        {
            get { return _backGradientCenter; }
            set
            {
                _backGradientCenter = value;
                SetFloat("_BackGradientCenter", _backGradientCenter);
            }
        }

        public float BackGradientWidth
        {
            get { return _backGradientWidth; }
            set
            {
                _backGradientWidth = value;
                SetFloat("_BackGradientWidth", _backGradientWidth);
            }
        }

        public bool BackGradientRevert
        {
            get { return _backGradientRevert; }
            set
            {
                _backGradientRevert = value;
                SetFloat("_BackGradientRevert", _backGradientRevert);
            }
        }

        public bool BackGradientChangeDirection
        {
            get { return _backGradientChangeDirection; }
            set
            {
                _backGradientChangeDirection = value;
                SetFloat("_BackGradientChangeDirection", _backGradientChangeDirection);
            }
        }

        public float BackGradientRotation
        {
            get { return _backGradientRotation; }
            set
            {
                _backGradientRotation = value;
                SetFloat("_BackGradientRotation", _backGradientRotation);
            }
        }

        public Color LeftColor
        {
            get { return _leftColor; }
            set
            {
                _leftColor = value;
                SetColor("_LeftColor", _leftColor);
            }
        }

        public Texture LeftTexture
        {
            get { return _leftTexture; }
            set
            {
                _leftTexture = value;
                SetTexture("_LeftTexture", _leftTexture);
            }
        }

        public Vector2 LeftTextureTiling
        {
            get { return _leftTextureTiling; }
            set
            {
                _leftTextureTiling = value;
                SetTextureTiling("_LeftTexture", _leftTextureTiling);
            }
        }

        public Vector2 LeftTextureOffset
        {
            get { return _leftTextureOffset; }
            set
            {
                _leftTextureOffset = value;
                SetTextureOffset("_LeftTexture", _leftTextureOffset);
            }
        }

        public float LeftTextLevel
        {
            get { return _leftTextLevel; }
            set
            {
                _leftTextLevel = value;
                SetFloat("_LeftTextureLevel", _leftTextLevel);
            }
        }

        public bool LeftGradient
        {
            get { return _leftGradient; }
            set
            {
                _leftGradient = value;
                SetFloat("_LeftGradient", _leftGradient);
            }
        }

        public Color LeftTopColorGradient
        {
            get { return _leftTopColorGradient; }
            set
            {
                _leftTopColorGradient = value;
                SetColor("_LeftTopColor", _leftTopColorGradient);
            }
        }

        public float LeftGradientCenter
        {
            get { return _leftGradientCenter; }
            set
            {
                _leftGradientCenter = value;
                SetFloat("_LeftGradientCenter", _leftGradientCenter);
            }
        }

        public float LeftGradientWidth
        {
            get { return _leftGradientWidth; }
            set
            {
                _leftGradientWidth = value;
                SetFloat("_LeftGradientWidth", _leftGradientWidth);
            }
        }

        public bool LeftGradientRevert
        {
            get { return _leftGradientRevert; }
            set
            {
                _leftGradientRevert = value;
                SetFloat("_LeftGradientRevert", _leftGradientRevert);
            }
        }

        public bool LeftGradientChangeDirection
        {
            get { return _leftGradientChangeDirection; }
            set
            {
                _leftGradientChangeDirection = value;
                SetFloat("_LeftGradientChangeDirection", _leftGradientChangeDirection);
            }
        }

        public float LeftGradientRotation
        {
            get { return _leftGradientRotation; }
            set
            {
                _leftGradientRotation = value;
                SetFloat("_LeftGradientRotation", _leftGradientRotation);
            }
        }

        public Color BottomColor
        {
            get { return _bottomColor; }
            set
            {
                _bottomColor = value;
                SetColor("_BottomColor", _bottomColor);
            }
        }

        public Texture BottomTexture
        {
            get { return _bottomTexture; }
            set
            {
                _bottomTexture = value;
                SetTexture("_BottomTexture", _bottomTexture);
            }
        }

        public Vector2 BottomTextureTiling
        {
            get { return _bottomTextureTiling; }
            set
            {
                _bottomTextureTiling = value;
                SetTextureTiling("_BottomTexture", _bottomTextureTiling);
            }
        }

        public Vector2 BottomTextureOffset
        {
            get { return _bottomTextureOffset; }
            set
            {
                _bottomTextureOffset = value;
                SetTextureOffset("_BottomTexture", _bottomTextureOffset);
            }
        }

        public float BottomTextLevel
        {
            get { return _bottomTextLevel; }
            set
            {
                _bottomTextLevel = value;
                SetFloat("_BottomTextureLevel", _bottomTextLevel);
            }
        }

        public bool BottomGradient
        {
            get { return _bottomGradient; }
            set
            {
                _bottomGradient = value;
                SetFloat("_BottomGradient", _bottomGradient);
            }
        }

        public Color BottomTopColorGradient
        {
            get { return _bottomTopColorGradient; }
            set
            {
                _bottomTopColorGradient = value;
                SetColor("_BottomTopColor", _bottomTopColorGradient);
            }
        }

        public float BottomGradientCenter
        {
            get { return _bottomGradientCenter; }
            set
            {
                _bottomGradientCenter = value;
                SetFloat("_BottomGradientCenter", _bottomGradientCenter);
            }
        }

        public float BottomGradientWidth
        {
            get { return _bottomGradientWidth; }
            set
            {
                _bottomGradientWidth = value;
                SetFloat("_BottomGradientWidth", _bottomGradientWidth);
            }
        }

        public bool BottomGradientRevert
        {
            get { return _bottomGradientRevert; }
            set
            {
                _bottomGradientRevert = value;
                SetFloat("_BottomGradientRevert", _bottomGradientRevert);
            }
        }

        public bool BottomGradientChangeDirection
        {
            get { return _bottomGradientChangeDirection; }
            set
            {
                _bottomGradientChangeDirection = value;
                SetFloat("_BottomGradientChangeDirection", _bottomGradientChangeDirection);
            }
        }

        public float BottomGradientRotation
        {
            get { return _bottomGradientRotation; }
            set
            {
                _bottomGradientRotation = value;
                SetFloat("_BottomGradientRotation", _bottomGradientRotation);
            }
        }

        #endregion

        #region Private Variables

        [SerializeField]
        private Color _backColor;

        [SerializeField]
        private Texture _backTexture;

        [SerializeField]
        private Vector2 _backTextureTiling;

        [SerializeField]
        private Vector2 _backTextureOffset;

        [SerializeField]
        private float _backTextLevel;

        [SerializeField]
        private bool _backGradient;

        [SerializeField]
        private Color _backTopColorGradient;

        [SerializeField]
        private float _backGradientCenter;

        [SerializeField]
        private float _backGradientWidth;

        [SerializeField]
        private bool _backGradientRevert;

        [SerializeField]
        private bool _backGradientChangeDirection;

        [SerializeField]
        private float _backGradientRotation;

        [SerializeField]
        private Color _leftColor;

        [SerializeField]
        private Texture _leftTexture;

        [SerializeField]
        private Vector2 _leftTextureTiling;

        [SerializeField]
        private Vector2 _leftTextureOffset;

        [SerializeField]
        private float _leftTextLevel;

        [SerializeField]
        private bool _leftGradient;

        [SerializeField]
        private Color _leftTopColorGradient;

        [SerializeField]
        private float _leftGradientCenter;

        [SerializeField]
        private float _leftGradientWidth;

        [SerializeField]
        private bool _leftGradientRevert;

        [SerializeField]
        private bool _leftGradientChangeDirection;

        [SerializeField]
        private float _leftGradientRotation;

        [SerializeField]
        private Color _bottomColor;

        [SerializeField]
        private Texture _bottomTexture;

        [SerializeField]
        private Vector2 _bottomTextureTiling;

        [SerializeField]
        private Vector2 _bottomTextureOffset;

        [SerializeField]
        private float _bottomTextLevel;

        [SerializeField]
        private bool _bottomGradient;

        [SerializeField]
        private Color _bottomTopColorGradient;

        [SerializeField]
        private float _bottomGradientCenter;

        [SerializeField]
        private float _bottomGradientWidth;

        [SerializeField]
        private bool _bottomGradientRevert;

        [SerializeField]
        private bool _bottomGradientChangeDirection;

        [SerializeField]
        private float _bottomGradientRotation;

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            base.CopyShaderParameter();

            BackColor = GetColor("_BackColor");

            BackTexture = GetTexture("_BackTexture");

            BackTextureTiling = GetTextureTiling("_BackTexture");

            BackTextureOffset = GetTextureOffset("_BackTexture");

            BackTextLevel = GetFloat("_BackTextureLevel");

            BackGradient = GetBoolean("_BackGradient");

            BackTopColorGradient = GetColor("_BackTopColor");

            BackGradientCenter = GetFloat("_BackGradientCenter");

            BackGradientWidth = GetFloat("_BackGradientWidth");

            BackGradientRevert = GetBoolean("_BackGradientRevert");

            BackGradientChangeDirection = GetBoolean("_BackGradientChangeDirection");

            BackGradientRotation = GetFloat("_BackGradientRotation");

            LeftColor = GetColor("_LeftColor");

            LeftTexture = GetTexture("_LeftTexture");

            LeftTextureTiling = GetTextureTiling("_LeftTexture");

            LeftTextureOffset = GetTextureOffset("_LeftTexture");

            LeftTextLevel = GetFloat("_LeftTextureLevel");

            LeftGradient = GetBoolean("_LeftGradient");

            LeftTopColorGradient = GetColor("_LeftTopColor");

            LeftGradientCenter = GetFloat("_LeftGradientCenter");

            LeftGradientWidth = GetFloat("_LeftGradientWidth");

            LeftGradientRevert = GetBoolean("_LeftGradientRevert");

            LeftGradientChangeDirection = GetBoolean("_LeftGradientChangeDirection");

            LeftGradientRotation = GetFloat("_LeftGradientRotation");

            BottomColor = GetColor("_BottomColor");

            BottomTexture = GetTexture("_BottomTexture");

            BottomTextureTiling = GetTextureTiling("_BottomTexture");

            BottomTextureOffset = GetTextureOffset("_BottomTexture");

            BottomTextLevel = GetFloat("_BottomTextureLevel");

            BottomGradient = GetBoolean("_BottomGradient");

            BottomTopColorGradient = GetColor("_BottomTopColor");

            BottomGradientCenter = GetFloat("_BottomGradientCenter");

            BottomGradientWidth = GetFloat("_BottomGradientWidth");

            BottomGradientRevert = GetBoolean("_BottomGradientRevert");

            BottomGradientChangeDirection = GetBoolean("_BottomGradientChangeDirection");

            BottomGradientRotation = GetFloat("_BottomGradientRotation");

        }

        protected override void SetShaderParameters()
        {

            base.SetShaderParameters();

            BackColor = _backColor;

            BackTexture = _backTexture;

            BackTextureTiling = _backTextureTiling;

            BackTextureOffset = _backTextureOffset;

            BackTextLevel = _backTextLevel;

            BackGradient = _backGradient;

            BackTopColorGradient = _backTopColorGradient;

            BackGradientCenter = _backGradientCenter;

            BottomGradientWidth = _backGradientWidth;

            BackGradientRevert = _backGradientRevert;

            BackGradientChangeDirection = _backGradientChangeDirection;

            BackGradientRotation = _backGradientRotation;

            LeftColor = _leftColor;

            LeftTexture = _leftTexture;

            LeftTextureTiling = _leftTextureTiling;

            LeftTextureOffset = _leftTextureOffset;

            LeftTextLevel = _leftTextLevel;

            LeftGradient = _leftGradient;

            LeftTopColorGradient = _leftTopColorGradient;

            LeftGradientCenter = _leftGradientCenter;

            LeftGradientWidth = _leftGradientWidth;

            LeftGradientRevert = _leftGradientRevert;

            LeftGradientChangeDirection = _leftGradientChangeDirection;

            LeftGradientRotation = _leftGradientRotation;

            BottomColor = _bottomColor;

            BottomTexture = _bottomTexture;

            BottomTextureTiling = _bottomTextureTiling;

            BottomTextureOffset = _bottomTextureOffset;

            BottomTextLevel = _bottomTextLevel;

            BottomGradient = _bottomGradient;

            BottomTopColorGradient = _bottomTopColorGradient;

            BottomGradientCenter = _bottomGradientCenter;

            BottomGradientWidth = _bottomGradientWidth;

            BottomGradientRevert = _bottomGradientRevert;

            BottomGradientChangeDirection = _bottomGradientChangeDirection;

            BottomGradientRotation = _bottomGradientRotation;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}