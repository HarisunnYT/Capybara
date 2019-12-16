///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: FlatLightingSixLights behavior base.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// FlatLightingSixLights behavior base.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base de los comportamientos FlatLightingSixLights.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingSixLightsBase : CGFFlatLightingLightBase
    {

        #region Public Variables

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

        public float LeftTextLevel
        {
            get { return _leftTextLevel; }
            set
            {
                _leftTextLevel = value;
                SetFloat("_LeftTextureLevel", _leftTextLevel);
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

        public float BottomTextLevel
        {
            get { return _bottomTextLevel; }
            set
            {
                _bottomTextLevel = value;
                SetFloat("_BottomTextureLevel", _bottomTextLevel);
            }
        }

        public float BackLightLevel
        {
            get { return _backLightLevel; }
            set
            {
                _backLightLevel = value;
                SetFloat("_BackLightLevel", _backLightLevel);
            }
        }

        public float LeftLightLevel
        {
            get { return _leftLightLevel; }
            set
            {
                _leftLightLevel = value;
                SetFloat("_LeftLightLevel", _leftLightLevel);
            }
        }

        public float BottomLightLevel
        {
            get { return _bottomLightLevel; }
            set
            {
                _bottomLightLevel = value;
                SetFloat("_BottomLightLevel", _bottomLightLevel);
            }
        }

        public float BackOpacityLevel
        {
            get { return _backOpacityLevel; }
            set
            {
                _backOpacityLevel = value;
                SetFloat("_BackOpacityLevel", _backOpacityLevel);
            }
        }

        public float LeftOpacityLevel
        {
            get { return _leftOpacityLevel; }
            set
            {
                _leftOpacityLevel = value;
                SetFloat("_LeftOpacityLevel", _leftOpacityLevel);
            }
        }

        public float BottomOpacityLevel
        {
            get { return _bottomOpacityLevel; }
            set
            {
                _bottomOpacityLevel = value;
                SetFloat("_BottomOpacityLevel", _bottomOpacityLevel);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private Texture _leftTexture;

        [SerializeField]
        private Vector2 _leftTextureTiling;

        [SerializeField]
        private Vector2 _leftTextureOffset;

        [SerializeField]
        private Texture _backTexture;       

        [SerializeField]
        private Vector2 _backTextureTiling;

        [SerializeField]
        private Vector2 _backTextureOffset;

        [SerializeField]
        private Texture _bottomTexture;
      
        [SerializeField]
        private Vector2 _bottomTextureTiling;

        [SerializeField]
        private Vector2 _bottomTextureOffset;

        [SerializeField]
        private float _leftTextLevel;

        [SerializeField]
        private float _backTextLevel;

        [SerializeField]
        private float _bottomTextLevel;

        [SerializeField]
        private float _backLightLevel;

        [SerializeField]
        private float _leftLightLevel;

        [SerializeField]
        private float _bottomLightLevel;

        [SerializeField]
        private float _backOpacityLevel;

        [SerializeField]
        private float _leftOpacityLevel;

        [SerializeField]
        private float _bottomOpacityLevel;

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            base.CopyShaderParameter();

            LeftTexture = GetTexture("_LeftTexture");

            LeftTextureTiling = GetTextureTiling("_LeftTexture");

            LeftTextureOffset = GetTextureOffset("_LeftTexture");

            BackTexture = GetTexture("_BackTexture");

            BackTextureTiling = GetTextureTiling("_BackTexture");

            BackTextureOffset = GetTextureOffset("_BackTexture");

            BottomTexture = GetTexture("_BottomTexture");

            BottomTextureTiling = GetTextureTiling("_BottomTexture");

            BottomTextureOffset = GetTextureOffset("_BottomTexture");

            LeftTextLevel = GetFloat("_LeftTextureLevel");

            BackTextLevel = GetFloat("_MainTextureLevel");

            BottomTextLevel = GetFloat("_BottomTextureLevel");

            BackLightLevel = GetFloat("_BackLightLevel");

            LeftLightLevel = GetFloat("_BackLightLevel");

            BottomLightLevel = GetFloat("_BackLightLevel");

            BackOpacityLevel = GetFloat("_BackOpacityLevel");

            LeftOpacityLevel = GetFloat("_BackOpacityLevel");

            BottomOpacityLevel = GetFloat("_BackOpacityLevel");

        }

        protected override void SetShaderParameters()
        {

            base.SetShaderParameters();

            LeftTexture = _leftTexture;

            LeftTextureTiling = _leftTextureTiling;

            LeftTextureOffset = LeftTextureOffset;

            BackTexture = _backTexture;

            BackTextureTiling = _backTextureTiling;

            BackTextureOffset = _backTextureOffset;

            BottomTexture = _bottomTexture;

            BottomTextureTiling = _bottomTextureTiling;

            BottomTextureOffset = _bottomTextureOffset;

            LeftTextLevel = _leftTextLevel;

            BackTextLevel = _backTextLevel;

            BottomTextLevel = _bottomTextLevel;

            BackLightLevel = _backLightLevel;

            LeftLightLevel = _leftLightLevel;

            BottomLightLevel = _bottomLightLevel;

            BackOpacityLevel = _backOpacityLevel;

            LeftOpacityLevel = _leftOpacityLevel;

            BottomOpacityLevel = _bottomOpacityLevel;

        }

        #endregion

        #region Utility Events

        #endregion

    }

}