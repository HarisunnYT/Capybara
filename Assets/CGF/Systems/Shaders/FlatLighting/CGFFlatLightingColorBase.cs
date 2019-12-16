///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: FlatLightingColor behavior base.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// FlatLightingColor behavior base.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base de los comportamientos FlatLightingColor.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingColorBase : CGFFlatLightingBase
    {

        #region Public Variables

        public Color FrontColor
        {
            get { return _frontColor; }
            set
            {
                _frontColor = value;
                SetColor("_FrontColor", _frontColor);
            }
        }

        public Color RightColor
        {
            get { return _rightColor; }
            set
            {
                _rightColor = value;
                SetColor("_RightColor", _rightColor);
            }
        }

        public Color TopColor
        {
            get { return _topColor; }
            set
            {
                _topColor = value;
                SetColor("_TopColor", _topColor);
            }
        }

        public bool FrontGradient
        {
            get { return _frontGradient; }
            set
            {
                _frontGradient = value;
                SetFloat("_FrontGradient", _frontGradient);
            }
        }

        public Color FrontTopColorGradient
        {
            get { return _frontTopColorGradient; }
            set
            {
                _frontTopColorGradient = value;
                SetColor("_FrontTopColor", _frontTopColorGradient);
            }
        }

        public float FrontGradientCenter
        {
            get { return _frontGradientCenter; }
            set
            {
                _frontGradientCenter = value;
                SetFloat("_FrontGradientCenter", _frontGradientCenter);
            }
        }

        public float FrontGradientWidth
        {
            get { return _frontGradientWidth; }
            set
            {
                _frontGradientWidth = value;
                SetFloat("_FrontGradientWidth", _frontGradientWidth);
            }
        }

        public bool FrontGradientRevert
        {
            get { return _frontGradientRevert; }
            set
            {
                _frontGradientRevert = value;
                SetFloat("_FrontGradientRevert", _frontGradientRevert);
            }
        }

        public bool FrontGradientChangeDirection
        {
            get { return _frontGradientChangeDirection; }
            set
            {
                _frontGradientChangeDirection = value;
                SetFloat("_FrontGradientChangeDirection", _frontGradientChangeDirection);
            }
        }

        public float FrontGradientRotation
        {
            get { return _frontGradientRotation; }
            set
            {
                _frontGradientRotation = value;
                SetFloat("_FrontGradientRotation", _frontGradientRotation);
            }
        }

        public bool RightGradient
        {
            get { return _rightGradient; }
            set
            {
                _rightGradient = value;
                SetFloat("_RightGradient", _rightGradient);
            }
        }

        public Color RightTopColorGradient
        {
            get { return _rightTopColorGradient; }
            set
            {
                _rightTopColorGradient = value;
                SetColor("_RightTopColor", _rightTopColorGradient);
            }
        }

        public float RightGradientCenter
        {
            get { return _rightGradientCenter; }
            set
            {
                _rightGradientCenter = value;
                SetFloat("_RightGradientCenter", _rightGradientCenter);
            }
        }

        public float RightGradientWidth
        {
            get { return _rightGradientWidth; }
            set
            {
                _rightGradientWidth = value;
                SetFloat("_RightGradientWidth", _rightGradientWidth);
            }
        }

        public bool RightGradientRevert
        {
            get { return _rightGradientRevert; }
            set
            {
                _rightGradientRevert = value;
                SetFloat("_RightGradientRevert", _rightGradientRevert);
            }
        }

        public bool RightGradientChangeDirection
        {
            get { return _rightGradientChangeDirection; }
            set
            {
                _rightGradientChangeDirection = value;
                SetFloat("_RightGradientChangeDirection", _rightGradientChangeDirection);
            }
        }

        public float RightGradientRotation
        {
            get { return _rightGradientRotation; }
            set
            {
                _rightGradientRotation = value;
                SetFloat("_RightGradientRotation", _rightGradientRotation);
            }
        }

        public bool TopGradient
        {
            get { return _topGradient; }
            set
            {
                _topGradient = value;
                SetFloat("_TopGradient", _topGradient);
            }
        }

        public Color TopTopColorGradient
        {
            get { return _topTopColorGradient; }
            set
            {
                _topTopColorGradient = value;
                SetColor("_TopTopColor", _topTopColorGradient);
            }
        }

        public float TopGradientCenter
        {
            get { return _topGradientCenter; }
            set
            {
                _topGradientCenter = value;
                SetFloat("_TopGradientCenter", _topGradientCenter);
            }
        }

        public float TopGradientWidth
        {
            get { return _topGradientWidth; }
            set
            {
                _topGradientWidth = value;
                SetFloat("_TopGradientWidth", _topGradientWidth);
            }
        }

        public bool TopGradientRevert
        {
            get { return _topGradientRevert; }
            set
            {
                _topGradientRevert = value;
                SetFloat("_TopGradientRevert", _topGradientRevert);
            }
        }

        public bool TopGradientChangeDirection
        {
            get { return _topGradientChangeDirection; }
            set
            {
                _topGradientChangeDirection = value;
                SetFloat("_TopGradientChangeDirection", _topGradientChangeDirection);
            }
        }

        public float TopGradientRotation
        {
            get { return _topGradientRotation; }
            set
            {
                _topGradientRotation = value;
                SetFloat("_TopGradientRotation", _topGradientRotation);
            }
        }

        public float OpacityLevel
        {
            get { return _opacityLevel; }
            set
            {
                _opacityLevel = value;
                SetFloat("_OpacityLevel", _opacityLevel);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private Color _frontColor;

        [SerializeField]
        private Color _rightColor;

        [SerializeField]
        private Color _topColor;

        [SerializeField]
        private bool _frontGradient;

        [SerializeField]
        private Color _frontTopColorGradient;

        [SerializeField]
        private float _frontGradientCenter;

        [SerializeField]
        private float _frontGradientWidth;

        [SerializeField]
        private bool _frontGradientRevert;
        
        [SerializeField]
        private bool _frontGradientChangeDirection;

        [SerializeField]
        private float _frontGradientRotation;

        [SerializeField]
        private bool _rightGradient;

        [SerializeField]
        private Color _rightTopColorGradient;

        [SerializeField]
        private float _rightGradientCenter;

        [SerializeField]
        private float _rightGradientWidth;

        [SerializeField]
        private bool _rightGradientRevert;

        [SerializeField]
        private bool _rightGradientChangeDirection;

        [SerializeField]
        private float _rightGradientRotation;

        [SerializeField]
        private bool _topGradient;

        [SerializeField]
        private Color _topTopColorGradient;

        [SerializeField]
        private float _topGradientCenter;

        [SerializeField]
        private float _topGradientWidth;

        [SerializeField]
        private bool _topGradientRevert;

        [SerializeField]
        private bool _topGradientChangeDirection;

        [SerializeField]
        private float _topGradientRotation;

        [SerializeField]
        private float _opacityLevel;

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {
            base.CopyShaderParameter();

            FrontColor = GetColor("_FrontColor");

            RightColor = GetColor("_RightColor");

            TopColor = GetColor("_TopColor");

            FrontGradient = GetBoolean("_FrontGradient");

            FrontTopColorGradient = GetColor("_FrontTopColor");

            FrontGradientCenter = GetFloat("_FrontGradientCenter");

            FrontGradientWidth = GetFloat("_FrontGradientWidth");

            FrontGradientRevert = GetBoolean("_FrontGradientRevert");

            FrontGradientChangeDirection = GetBoolean("_FrontGradientChangeDirection");

            FrontGradientRotation = GetFloat("_FrontGradientRotation");

            RightGradient = GetBoolean("_RightGradient");

            RightTopColorGradient = GetColor("_RightTopColor");

            RightGradientCenter = GetFloat("_RightGradientCenter");

            RightGradientWidth = GetFloat("_RightGradientWidth");

            RightGradientRevert = GetBoolean("_RightGradientRevert");

            RightGradientChangeDirection = GetBoolean("_RightGradientChangeDirection");

            RightGradientRotation = GetFloat("_RightGradientRotation");

            TopGradient = GetBoolean("_TopGradient");

            TopTopColorGradient = GetColor("_TopTopColor");

            TopGradientCenter = GetFloat("_TopGradientCenter");

            TopGradientWidth = GetFloat("_TopGradientWidth");

            TopGradientRevert = GetBoolean("_TopGradientRevert");

            TopGradientChangeDirection = GetBoolean("_TopGradientChangeDirection");

            TopGradientRotation = GetFloat("_TopGradientRotation");

            OpacityLevel = GetFloat("_OpacityLevel");
        }

        protected override void SetShaderParameters()
        {
            base.SetShaderParameters();

            FrontColor = _frontColor;

            RightColor = _rightColor;

            TopColor = _topColor;

            FrontGradient = _frontGradient;

            FrontTopColorGradient = _frontTopColorGradient;

            FrontGradientCenter = _frontGradientCenter;

            TopGradientWidth = _topGradientWidth;

            FrontGradientRevert = _frontGradientRevert;

            FrontGradientChangeDirection = _frontGradientChangeDirection;

            FrontGradientRotation = _frontGradientRotation;

            RightGradient = _rightGradient;

            RightTopColorGradient = _rightTopColorGradient;

            RightGradientCenter = _rightGradientCenter;

            RightGradientWidth = _rightGradientWidth;

            RightGradientRevert = _rightGradientRevert;

            RightGradientChangeDirection = _rightGradientChangeDirection;

            RightGradientRotation = _rightGradientRotation;

            TopGradient = _topGradient;

            TopTopColorGradient = _topTopColorGradient;

            TopGradientCenter = _topGradientCenter;

            FrontGradientWidth = _frontGradientWidth;

            TopGradientRevert = _topGradientRevert;

            TopGradientChangeDirection = _topGradientChangeDirection;

            TopGradientRotation = _topGradientRotation;

            OpacityLevel = _opacityLevel;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}