///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: FlatLightingLight behavior base.
///

using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// FlatLightingLight behavior base.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base de los comportamientos FlatLightingLight.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingLightBase : CGFFlatLightingBase
    {

        #region Public Variables

        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                SetColor("_Color", _color);
            }
        }

        public float FrontLightLevel
        {
            get { return _frontLightLevel; }
            set
            {
                _frontLightLevel = value;
                SetFloat("_FrontLightLevel", _frontLightLevel);
            }
        }

        public float RightLightLevel
        {
            get { return _rightLightLevel; }
            set
            {
                _rightLightLevel = value;
                SetFloat("_RightLightLevel", _rightLightLevel);
            }
        }

        public float TopLightLevel
        {
            get { return _topLightLevel; }
            set
            {
                _topLightLevel = value;
                SetFloat("_TopLightLevel", _topLightLevel);
            }
        }

        public float FrontOpacityLevel
        {
            get { return _frontOpacityLevel; }
            set
            {
                _frontOpacityLevel = value;
                SetFloat("_FrontOpacityLevel", _frontOpacityLevel);
            }
        }

        public float RightOpacityLevel
        {
            get { return _rightOpacityLevel; }
            set
            {
                _rightOpacityLevel = value;
                SetFloat("_RightOpacityLevel", _rightOpacityLevel);
            }
        }

        public float TopOpacityLevel
        {
            get { return _topOpacityLevel; }
            set
            {
                _topOpacityLevel = value;
                SetFloat("_TopOpacityLevel", _topOpacityLevel);
            }
        }

        public bool Gradient
        {
            get { return _gradient; }
            set
            {
                _gradient = value;
                SetFloat("_Gradient", _gradient);
            }
        }

        public Color GradientTopColor
        {
            get { return _gradientTopColor; }
            set
            {
                _gradientTopColor = value;
                SetColor("_GradientTopColor", _gradientTopColor);
            }
        }

        public float GradientCenter
        {
            get { return _gradientCenter; }
            set
            {
                _gradientCenter = value;
                SetFloat("_GradientCenter", _gradientCenter);
            }
        }

        public float GradientWidth
        {
            get { return _gradientWidth; }
            set
            {
                _gradientWidth = value;
                SetFloat("_GradientWidth", _gradientWidth);
            }
        }

        public bool GradientRevert
        {
            get { return _gradientRevert; }
            set
            {
                _gradientRevert = value;
                SetFloat("_GradientRevert", _gradientRevert);
            }
        }

        public bool GradientChangeDirection
        {
            get { return _gradientChangeDirection; }
            set
            {
                _gradientChangeDirection = value;
                SetFloat("_GradientChangeDirection", _gradientChangeDirection);
            }
        }

        public float GradientRotation
        {
            get { return _gradientRotation; }
            set
            {
                _gradientRotation = value;
                SetFloat("_GradientRotation", _gradientRotation);
            }
        }

        #endregion

        #region Private Variables

        [SerializeField]
        private Color _color;

        [SerializeField]
        private float _frontLightLevel;

        [SerializeField]
        private float _rightLightLevel;

        [SerializeField]
        private float _topLightLevel;

        [SerializeField]
        private float _frontOpacityLevel;

        [SerializeField]
        private float _rightOpacityLevel;

        [SerializeField]
        private float _topOpacityLevel;

        [SerializeField]
        private bool _gradient;

        [SerializeField]
        private Color _gradientTopColor;

        [SerializeField]
        private float _gradientCenter;

        [SerializeField]
        private float _gradientWidth;

        [SerializeField]
        private bool _gradientRevert;

        [SerializeField]
        private bool _gradientChangeDirection;

        [SerializeField]
        private float _gradientRotation;

        #endregion

        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            base.CopyShaderParameter();

            Color = GetColor("_Color");

            FrontLightLevel = GetFloat("_FrontLightLevel");

            RightLightLevel = GetFloat("_RightLightLevel");

            TopLightLevel = GetFloat("_TopLightLevel");

            FrontOpacityLevel = GetFloat("_FrontOpacityLevel");

            RightOpacityLevel = GetFloat("_RightOpacityLevel");

            TopOpacityLevel = GetFloat("_TopOpacityLevel");

            Gradient = GetBoolean("_Gradient");

            GradientTopColor = GetColor("_GradientTopColor");

            GradientCenter = GetFloat("_GradientCenter");

            GradientWidth = GetFloat("_GradientWidth");

            GradientRevert = GetBoolean("_GradientRevert");

            GradientChangeDirection = GetBoolean("_GradientChangeDirection");

            GradientRotation = GetFloat("_GradientRotation");

        }

        protected override void SetShaderParameters()
        {

            base.SetShaderParameters();

            Color = _color;

            FrontLightLevel = _frontLightLevel;

            RightLightLevel = _rightLightLevel;

            TopLightLevel = _topLightLevel;

            FrontOpacityLevel = _frontOpacityLevel;

            RightOpacityLevel = _rightOpacityLevel;

            TopOpacityLevel = _topOpacityLevel;

            Gradient = _gradient;

            GradientTopColor = _gradientTopColor;

            GradientCenter = _gradientCenter;

            GradientWidth = _gradientWidth;

            GradientRevert = _gradientRevert;

            GradientChangeDirection = _gradientChangeDirection;

            GradientRotation = _gradientRotation;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}