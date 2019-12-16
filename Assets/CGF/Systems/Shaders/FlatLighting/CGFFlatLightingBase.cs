///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: FlatLighting behavior base.
///

using Assets.CGF.Systems.Shaders;
using UnityEngine;

namespace CGF.Systems.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// FlatLighting behavior base.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base de los comportamientos FlatLighting.
    /// </summary>
    /// \endspanish
    [ExecuteInEditMode]
    public abstract class CGFFlatLightingBase : CGFShaderBehavior
    {

        #region Public Variables

        public Texture MainTexture
        {
            get { return _mainTexture; }
            set
            {
                _mainTexture = value;
                SetTexture("_MainTex", _mainTexture);
            }
        }

        public Vector2 MainTextureTiling
        {
            get { return _mainTextureTiling; }
            set
            {
                _mainTextureTiling = value;
                SetTextureTiling("_MainTex", _mainTextureTiling);
            }
        }

        public Vector2 MainTextureOffset
        {
            get { return _mainTextureOffset; }
            set
            {
                _mainTextureOffset = value;
                SetTextureOffset("_MainTex", _mainTextureOffset);
            }
        }

        public Texture FrontTexture
        {
            get { return _frontTexture; }
            set
            {
                _frontTexture = value;
                SetTexture("_FrontTexture", _frontTexture);
            }
        }

        public Vector2 FrontTextureTiling
        {
            get { return _frontTextureTiling; }
            set
            {
                _frontTextureTiling = value;
                SetTextureTiling("_FrontTexture", _frontTextureTiling);
            }
        }
        
        public Vector2 FrontTextureOffset
        {
            get { return _frontTextureOffset; }
            set
            {
                _frontTextureOffset = value;
                SetTextureOffset("_FrontTexture", _frontTextureOffset);
            }
        }

        public Texture RightTexture
        {
            get { return _rightTexture; }
            set
            {
                _rightTexture = value;
                SetTexture("_RightTexture", _rightTexture);
            }
        }

        public Vector2 RightTextureTiling
        {
            get { return _rightTextureTiling; }
            set
            {
                _rightTextureTiling = value;
                SetTextureTiling("_RightTexture", _rightTextureTiling);
            }
        }

        public Vector2 RightTextureOffset
        {
            get { return _rightTextureOffset; }
            set
            {
                _rightTextureOffset = value;
                SetTextureOffset("_RightTexture", _rightTextureOffset);
            }
        }

        public Texture TopTexture
        {
            get { return TopTexture; }
            set
            {
                _topTexture = value;
                SetTexture("_TopTexture", _topTexture);
            }
        }

        public Vector2 TopTextureTiling
        {
            get { return _topTextureTiling; }
            set
            {
                _topTextureTiling = value;
                SetTextureTiling("_TopTexture", _topTextureTiling);
            }
        }

        public Vector2 TopTextureOffset
        {
            get { return _topTextureOffset; }
            set
            {
                _topTextureOffset = value;
                SetTextureOffset("_TopTexture", _topTextureOffset);
            }
        }

        public float MainTextLevel
        {
            get { return _mainTextLevel; }
            set
            {
                _mainTextLevel = value;
                SetFloat("_MainTextureLevel", _mainTextLevel);
            }
        }

        public float FrontTextLevel
        {
            get { return _frontTextLevel; }
            set
            {
                _frontTextLevel = value;
                SetFloat("_FrontTextureLevel", _frontTextLevel);
            }
        }

        public float RightTextLevel
        {
            get { return _rightTextLevel; }
            set
            {
                _rightTextLevel = value;
                SetFloat("_RightTextureLevel", _rightTextLevel);
            }
        }

        public float TopTextLevel
        {
            get { return _topTextLevel; }
            set
            {
                _topTextLevel = value;
                SetFloat("_TopTextureLevel", _topTextLevel);
            }
        }

        public float AlphaCutoff
        {
            get { return _alphaCutoff; }
            set
            {
                _alphaCutoff = value;
                SetFloat("_Cutoff", _topTextLevel);
            }
        }

        public bool ViewDirection
        {
            get { return _viewDirection; }
            set
            {
                _viewDirection = value;
                SetFloat("_ViewDirection", _viewDirection);
            }
        }

        public bool HeightFog
        {
            get { return _heightFog; }
            set
            {
                _heightFog = value;
                SetFloat("_HeightFog", _heightFog);
            }
        }

        public Color HeightFogColor
        {
            get { return _heightFogColor; }
            set
            {
                _heightFogColor = value;
                SetColor("_HeightFogColor", _heightFogColor);
            }
        }

        public float HeightFogDensity
        {
            get { return _heightFogDensity; }
            set
            {
                _heightFogDensity = value;
                SetFloat("_HeightFogDensity", _heightFogDensity);
            }
        }

        public float HeightFogStartPosition
        {
            get { return _heightFogStartPosition; }
            set
            {
                _heightFogStartPosition = value;
                SetFloat("_HeightFogStartPosition", _heightFogStartPosition);
            }
        }

        public float FogHeight
        {
            get { return _fogHeight; }
            set
            {
                _fogHeight = value;
                SetFloat("_FogHeight", _fogHeight);
            }
        }

        public bool UseAlphaValue
        {
            get { return _useAlphaValue; }
            set
            {
                _useAlphaValue = value;
                SetFloat("_UseAlphaValue", _useAlphaValue);
            }
        }

        public bool LocalHeightFog
        {
            get { return _localHeightFog; }
            set
            {
                _localHeightFog = value;
                SetFloat("_LocalHeightFog", _localHeightFog);
            }
        }

        public bool DistanceFog
        {
            get { return _distanceFog; }
            set
            {
                _distanceFog = value;
                SetFloat("_DistanceFog", _distanceFog);
            }
        }

        public Color DistanceFogColor
        {
            get { return _distanceFogColor; }
            set
            {
                _distanceFogColor = value;
                SetColor("_DistanceFogColor", _distanceFogColor);
            }
        }

        public float DistanceFogStartPosition
        {
            get { return _distanceFogStartPosition; }
            set
            {
                _distanceFogStartPosition = value;
                SetFloat("_DistanceFogStartPosition", _distanceFogStartPosition);
            }
        }

        public float DistanceFogLength
        {
            get { return _distanceFogLength; }
            set
            {
                _distanceFogLength = value;
                SetFloat("_DistanceFogLength", _distanceFogLength);
            }
        }

        public float DistanceFogDensity
        {
            get { return _distanceFogDensity; }
            set
            {
                _distanceFogDensity = value;
                SetFloat("_DistanceFogDensity", _distanceFogDensity);
            }
        }

        public Vector3 WorldDistanceFogPosition
        {
            get { return _worldDistanceFogPosition; }
            set
            {
                _worldDistanceFogPosition = value;
                SetVector("_WorldDistanceFogPosition", _worldDistanceFogPosition);
            }
        }

        public bool UseAlpha
        {
            get { return _useAlpha; }
            set
            {
                _useAlpha = value;
                SetFloat("_UseAlpha", _useAlpha);
            }
        }

        public bool WorldDistanceFog
        {
            get { return _worldDistanceFog; }
            set
            {
                _worldDistanceFog = value;
                SetFloat("_WorldDistanceFog", _worldDistanceFog);
            }
        }

        public bool Light
        {
            get { return _light; }
            set
            {
                _light = value;
                SetFloat("_Light", _light);
            }
        }

        public bool DirectionalLight
        {
            get { return _directionalLight; }
            set
            {
                _directionalLight = value;
                SetFloat("_DirectionalLight", _directionalLight);
            }
        }

        public bool Ambient
        {
            get { return _ambient; }
            set
            {
                _ambient = value;
                SetFloat("_Ambient", _ambient);
            }
        }

        public bool SimulatedLight
        {
            get { return _simulatedLight; }
            set
            {
                _simulatedLight = value;
                SetFloat("_SimulatedLight", _simulatedLight);
            }
        }

        public Texture SimulatedLightRampTexture
        {
            get { return _simulatedLightRampTexture; }
            set
            {
                _simulatedLightRampTexture = value;
                SetTexture("_SimulatedLightRampTexture", _simulatedLightRampTexture);
            }
        }

        public Vector2 SimulatedLightRampTextureTiling
        {
            get { return _simulatedLightRampTextureTiling; }
            set
            {
                _simulatedLightRampTextureTiling = value;
                SetTextureTiling("_SimulatedLightRampTexture", _simulatedLightRampTextureTiling);
            }
        }

        public Vector2 SimulatedLightRampTextureOffset
        {
            get { return _simulatedLightRampTextureOffset; }
            set
            {
                _simulatedLightRampTextureOffset = value;
                SetTextureOffset("_SimulatedLightRampTexture", _simulatedLightRampTextureOffset);
            }
        }

        public float SimulatedLightLevel
        {
            get { return _simulatedLightLevel; }
            set
            {
                _simulatedLightLevel = value;
                SetFloat("_SimulatedLightLevel", _simulatedLightLevel);
            }
        }

        public Vector3 SimulatedLightPosition
        {
            get { return _simulatedLightPosition; }
            set
            {
                _simulatedLightPosition = value;
                SetVector("_SimulatedLightPosition", _simulatedLightPosition);
            }
        }

        public float SimulatedLightDistance
        {
            get { return _simulatedLightDistance; }
            set
            {
                _simulatedLightDistance = value;
                SetFloat("_SimulatedLightDistance", _simulatedLightDistance);
            }
        }

        public bool GradientRamp
        {
            get { return _gradientRamp; }
            set
            {
                _gradientRamp = value;
                SetFloat("_GradientRamp", _gradientRamp);
            }
        }

        public Color CenterColor
        {
            get { return _centerColor; }
            set
            {
                _centerColor = value;
                SetColor("_CenterColor", _centerColor);
            }
        }

        public bool UseExternalColor
        {
            get { return _useExternalColor; }
            set
            {
                _useExternalColor = value;
                SetFloat("_UseExternalColor", _useExternalColor);
            }
        }

        public Color ExternalColor
        {
            get { return _externalColor; }
            set
            {
                _externalColor = value;
                SetColor("_ExternalColor", _externalColor);
            }
        }

        public bool AdditiveSimulatedLight
        {
            get { return _additiveSimulatedLight; }
            set
            {
                _additiveSimulatedLight = value;
                SetFloat("_AdditiveSimulatedLight", _additiveSimulatedLight);
            }
        }

        public float AdditiveSimulatedLightLevel
        {
            get { return _additiveSimulatedLightLevel; }
            set
            {
                _additiveSimulatedLightLevel = value;
                SetFloat("_AdditiveSimulatedLightLevel", _additiveSimulatedLightLevel);
            }
        }

        public bool Posterize
        {
            get { return _posterize; }
            set
            {
                _posterize = value;
                SetFloat("_Posterize", _posterize);
            }
        }

        public float Steps
        {
            get { return _steps; }
            set
            {
                _steps = value;
                SetFloat("_Steps", _steps);
            }
        }

        public bool Lightmap
        {
            get { return _lightmap; }
            set
            {
                _lightmap = value;
                SetFloat("_Lightmap", _lightmap);
            }
        }

        public Color LightmapColor
        {
            get { return _lightmapColor; }
            set
            {
                _lightmapColor = value;
                SetColor("_LightmapColor", _lightmapColor);
            }
        }

        public float LightmapLevel
        {
            get { return _lightmapLevel; }
            set
            {
                _lightmapLevel = value;
                SetFloat("_LightmapLevel", _lightmapLevel);
            }
        }

        public float ShadowLevel
        {
            get { return _shadowLevel; }
            set
            {
                _shadowLevel = value;
                SetFloat("_ShadowLevel", _shadowLevel);
            }
        }

        public bool MultiplyLightmap
        {
            get { return _multiplyLightmap; }
            set
            {
                _multiplyLightmap = value;
                SetFloat("_MultiplyLightmap", _multiplyLightmap);
            }
        }

        public bool DesaturateLightColor
        {
            get { return _desaturateLightColor; }
            set
            {
                _desaturateLightColor = value;
                SetFloat("_DesaturateLightColor", _desaturateLightColor);
            }
        }

        #endregion


        #region Private Variables

        [SerializeField]
        private Texture _mainTexture;

        [SerializeField]
        private Vector2 _mainTextureTiling;

        [SerializeField]
        private Vector2 _mainTextureOffset;

        [SerializeField]
        private Texture _frontTexture;

        [SerializeField]
        private Vector2 _frontTextureTiling;

        [SerializeField]
        private Vector2 _frontTextureOffset;

        [SerializeField]
        private Texture _rightTexture;

        [SerializeField]
        private Vector2 _rightTextureTiling;

        [SerializeField]
        private Vector2 _rightTextureOffset;

        [SerializeField]
        private Texture _topTexture;

        [SerializeField]
        private Vector2 _topTextureTiling;

        [SerializeField]
        private Vector2 _topTextureOffset;

        [SerializeField]
        private float _mainTextLevel;

        [SerializeField]
        private float _frontTextLevel;

        [SerializeField]
        private float _rightTextLevel;

        [SerializeField]
        private float _topTextLevel;

        [SerializeField]
        private float _alphaCutoff;

        [SerializeField]
        private bool _viewDirection;

        [SerializeField]
        private bool _heightFog;

        [SerializeField]
        private Color _heightFogColor;

        [SerializeField]
        private float _heightFogDensity;

        [SerializeField]
        private float _heightFogStartPosition;

        [SerializeField]
        private float _fogHeight;

        [SerializeField]
        private bool _useAlphaValue;

        [SerializeField]
        private bool _localHeightFog;

        [SerializeField]
        private bool _distanceFog;

        [SerializeField]
        private Color _distanceFogColor;

        [SerializeField]
        private float _distanceFogStartPosition;

        [SerializeField]
        private float _distanceFogLength;

        [SerializeField]
        private float _distanceFogDensity;

        [SerializeField]
        private Vector3 _worldDistanceFogPosition;

        [SerializeField]
        private bool _useAlpha;

        [SerializeField]
        private bool _worldDistanceFog;

        [SerializeField]
        private bool _light;

        [SerializeField]
        private bool _directionalLight;

        [SerializeField]
        private bool _ambient;

        [SerializeField]
        private bool _simulatedLight;

        [SerializeField]
        private Texture _simulatedLightRampTexture;

        [SerializeField]
        private Vector2 _simulatedLightRampTextureTiling;

        [SerializeField]
        private Vector2 _simulatedLightRampTextureOffset;

        [SerializeField]
        private float _simulatedLightLevel;

        [SerializeField]
        private Vector3 _simulatedLightPosition;

        [SerializeField]
        private float _simulatedLightDistance;

        [SerializeField]
        private bool _gradientRamp;

        [SerializeField]
        private Color _centerColor;

        [SerializeField]
        private bool _useExternalColor;

        [SerializeField]
        private Color _externalColor;

        [SerializeField]
        private bool _additiveSimulatedLight;

        [SerializeField]
        private float _additiveSimulatedLightLevel;

        [SerializeField]
        private bool _posterize;

        [SerializeField]
        private float _steps;

        [SerializeField]
        private bool _lightmap;

        [SerializeField]
        private Color _lightmapColor;

        [SerializeField]
        private float _lightmapLevel;

        [SerializeField]
        private float _shadowLevel;

        [SerializeField]
        private bool _multiplyLightmap;

        [SerializeField]
        private bool _desaturateLightColor;

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

        protected override void CopyShaderParameter()
        {

            MainTexture = GetTexture("_MainTex");

            MainTextureTiling = GetTextureTiling("_MainTex");

            MainTextureOffset = GetTextureOffset("_MainTex");

            FrontTexture = GetTexture("_FrontTexture");

            FrontTextureTiling = GetTextureTiling("_FrontTexture");

            FrontTextureOffset = GetTextureOffset("_FrontTexture");

            RightTexture = GetTexture("_RightTexture");

            RightTextureTiling = GetTextureTiling("_RightTexture");

            RightTextureOffset = GetTextureOffset("_RightTexture");

            TopTexture = GetTexture("_TopTexture");

            TopTextureTiling = GetTextureTiling("_TopTexture");

            TopTextureOffset = GetTextureOffset("_TopTexture");

            MainTextLevel = GetFloat("_MainTextureLevel");

            FrontTextLevel = GetFloat("_FrontTextureLevel");

            RightTextLevel = GetFloat("_RightTextureLevel");

            TopTextLevel = GetFloat("_TopTextureLevel");

            AlphaCutoff = GetFloat("_Cutoff");

            ViewDirection = GetBoolean("_ViewDirection");

            HeightFog = GetBoolean("_HeightFog");

            HeightFogColor = GetColor("_HeightFogColor");

            HeightFogDensity = GetFloat("_HeightFogDensity");

            HeightFogStartPosition = GetFloat("_HeightFogStartPosition");

            FogHeight = GetFloat("_FogHeight");

            UseAlphaValue = GetBoolean("_UseAlphaValue");

            LocalHeightFog = GetBoolean("_LocalHeightFog");

            DistanceFog = GetBoolean("_DistanceFog");

            DistanceFogColor = GetColor("_DistanceFogColor");

            DistanceFogStartPosition = GetFloat("_DistanceFogStartPosition");

            DistanceFogLength = GetFloat("_DistanceFogLength");

            DistanceFogDensity = GetFloat("_DistanceFogDensity");

            UseAlpha = GetBoolean("_UseAlpha");

            WorldDistanceFog = GetBoolean("_WorldDistanceFog");

            WorldDistanceFogPosition = GetVector("_WorldDistanceFogPosition");

            Light = GetBoolean("_Light");

            DirectionalLight = GetBoolean("_DirectionalLight"); 

            Ambient = GetBoolean("_Ambient");

            SimulatedLight = GetBoolean("_SimulatedLight");

            SimulatedLightRampTexture = GetTexture("_SimulatedLightRampTexture");

            SimulatedLightRampTextureTiling = GetTextureTiling("_SimulatedLightRampTexture");

            SimulatedLightRampTextureOffset = GetTextureOffset("_SimulatedLightRampTexture");

            SimulatedLightLevel = GetFloat("_SimulatedLightLevel");

            SimulatedLightDistance = GetFloat("_SimulatedLightDistance");

            GradientRamp = GetBoolean("_GradientRamp");

            CenterColor = GetColor("_CenterColor");

            UseExternalColor = GetBoolean("_UseExternalColor");

            ExternalColor = GetColor("_ExternalColor");

            AdditiveSimulatedLight = GetBoolean("_AdditiveSimulatedLight");

            AdditiveSimulatedLightLevel = GetFloat("_AdditiveSimulatedLightLevel");

            Posterize = GetBoolean("_Posterize");

            Steps = GetFloat("_Steps");

            Lightmap = GetBoolean("_Lightmap");

            LightmapColor = GetColor("_LightmapColor");

            LightmapLevel = GetFloat("_LightmapLevel");

            ShadowLevel = GetFloat("_ShadowLevel");

            MultiplyLightmap = GetBoolean("_MultiplyLightmap");

            DesaturateLightColor = GetBoolean("_DesaturateLightColor");

        }

        protected override void SetShaderParameters()
        {

            MainTexture = _mainTexture;

            MainTextureTiling = _mainTextureTiling;

            MainTextureOffset = _mainTextureOffset;

            FrontTexture = _frontTexture;

            FrontTextureTiling = _frontTextureTiling;

            FrontTextureOffset = _frontTextureOffset;

            RightTexture = _rightTexture;

            RightTextureTiling = _rightTextureTiling;

            RightTextureOffset = _rightTextureOffset;

            TopTexture = _topTexture;

            TopTextureTiling = _topTextureTiling;

            TopTextureOffset = _topTextureOffset;

            MainTextLevel = _mainTextLevel;

            FrontTextLevel = _frontTextLevel;

            RightTextLevel = _rightTextLevel;

            TopTextLevel = _topTextLevel;

            AlphaCutoff = _alphaCutoff;

            ViewDirection = _viewDirection;

            HeightFog = _heightFog;

            HeightFogColor = _heightFogColor;

            HeightFogDensity = _heightFogDensity;

            HeightFogStartPosition = _heightFogStartPosition;

            FogHeight = _fogHeight;

            UseAlphaValue = _useAlphaValue;

            LocalHeightFog = _localHeightFog;

            DistanceFog = _distanceFog;

            DistanceFogColor = _distanceFogColor;

            DistanceFogStartPosition = _distanceFogStartPosition;

            DistanceFogLength = _distanceFogLength;

            DistanceFogDensity = _distanceFogDensity;

            UseAlpha = _useAlpha;

            WorldDistanceFog = _worldDistanceFog;

            WorldDistanceFogPosition = _worldDistanceFogPosition;

            Light = _light;

            DirectionalLight = _directionalLight;

            Ambient = _ambient;

            SimulatedLight = _simulatedLight;

            SimulatedLightRampTexture = _simulatedLightRampTexture;

            SimulatedLightRampTextureTiling = _simulatedLightRampTextureTiling;

            SimulatedLightRampTextureOffset = _simulatedLightRampTextureOffset;

            SimulatedLightLevel = _simulatedLightLevel;

            SimulatedLightDistance = _simulatedLightDistance;

            GradientRamp = _gradientRamp;

            CenterColor = _centerColor;

            UseExternalColor = _useExternalColor;

            ExternalColor = _externalColor;

            AdditiveSimulatedLight = _additiveSimulatedLight;

            AdditiveSimulatedLightLevel = _additiveSimulatedLightLevel;

            Posterize = _posterize;

            Steps = _steps;

            Lightmap = _lightmap;

            LightmapColor = _lightmapColor;

            LightmapLevel = _lightmapLevel;

            ShadowLevel = _shadowLevel;

            MultiplyLightmap = _multiplyLightmap;

            DesaturateLightColor = _desaturateLightColor;

        }

        #endregion


        #region Utility Events

        #endregion

    }

}