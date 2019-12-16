///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Base of editors of the behaviors FlatLighting.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Base of editors of the behaviors FlatLighting.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base del editor de los comportamientos FlatLighting.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingBaseEditor<T> : UnityEditor.Editor where T : Component
    {

        SerializedProperty _mainTexture;
        SerializedProperty _mainTextureTiling;
        SerializedProperty _mainTextureOffset;

        SerializedProperty _frontTexture;
        SerializedProperty _frontTextureTiling;
        SerializedProperty _frontTextureOffset;

        SerializedProperty _rightTexture;
        SerializedProperty _rightTextureTiling;
        SerializedProperty _rightTextureOffset;

        SerializedProperty _topTexture;
        SerializedProperty _topTextureTiling;
        SerializedProperty _topTextureOffset;

        SerializedProperty _mainTextLevel;
        SerializedProperty _frontTextLevel;
        SerializedProperty _rightTextLevel;
        SerializedProperty _topTextLevel;

        SerializedProperty _viewDirection;

        SerializedProperty _heightFog;
        SerializedProperty _heightFogColor;
        SerializedProperty _heightFogDensity;
        SerializedProperty _heightFogStartPosition;
        SerializedProperty _fogHeight;
        SerializedProperty _useAlphaValue;
        SerializedProperty _localHeightFog;

        SerializedProperty _distanceFog;
        SerializedProperty _distanceFogColor;
        SerializedProperty _distanceFogStartPosition;
        SerializedProperty _distanceFogLength;
        SerializedProperty _distanceFogDensity;
        SerializedProperty _useAlpha;
        SerializedProperty _worldDistanceFog;
        SerializedProperty _worldDistanceFogPosition;

        SerializedProperty _light;
        SerializedProperty _directionalLight;
        SerializedProperty _ambient;

        SerializedProperty _simulatedLight;
        SerializedProperty _simulatedLightRampTexture;
        SerializedProperty _simulatedLightRampTextureTiling;
        SerializedProperty _simulatedLightRampTextureOffset;
        SerializedProperty _simulatedLightLevel;
        SerializedProperty _simulatedLightPosition;
        SerializedProperty _simulatedLightDistance;
        SerializedProperty _gradientRamp;
        SerializedProperty _centerColor;
        SerializedProperty _useExternalColor;
        SerializedProperty _externalColor;
        SerializedProperty _additiveSimulatedLight;
        SerializedProperty _additiveSimulatedLightLevel;
        SerializedProperty _posterize;
        SerializedProperty _steps;

        SerializedProperty _lightmap;
        SerializedProperty _lightmapColor;
        SerializedProperty _lightmapLevel;
        SerializedProperty _shadowLevel;
        SerializedProperty _multiplyLightmap;
        SerializedProperty _desaturateLightColor;

        

        protected SerializedProperty myMaterial;

        protected string _colorsByNormals;
        protected string _colorType;

        protected virtual void OnEnable()
        {
            _colorsByNormals = "Colors By Normals";
            _colorType = " (RGB)";

            _mainTexture = serializedObject.FindProperty("_mainTexture");
            _mainTextureTiling = serializedObject.FindProperty("_mainTextureTiling");
            _mainTextureOffset = serializedObject.FindProperty("_mainTextureOffset");
            _frontTexture = serializedObject.FindProperty("_frontTexture");
            _frontTextureTiling = serializedObject.FindProperty("_frontTextureTiling");
            _frontTextureOffset = serializedObject.FindProperty("_frontTextureOffset");
            _rightTexture = serializedObject.FindProperty("_rightTexture");
            _rightTextureTiling = serializedObject.FindProperty("_rightTextureTiling");
            _rightTextureOffset = serializedObject.FindProperty("_rightTextureOffset");
            _topTexture = serializedObject.FindProperty("_topTexture");
            _topTextureTiling = serializedObject.FindProperty("_topTextureTiling");
            _topTextureOffset = serializedObject.FindProperty("_topTextureOffset");

            _mainTextLevel = serializedObject.FindProperty("_mainTextLevel");
            _frontTextLevel = serializedObject.FindProperty("_frontTextLevel");
            _rightTextLevel = serializedObject.FindProperty("_rightTextLevel");
            _topTextLevel = serializedObject.FindProperty("_topTextLevel");

            _viewDirection = serializedObject.FindProperty("_viewDirection");

            _heightFog = serializedObject.FindProperty("_heightFog");
            _heightFogColor = serializedObject.FindProperty("_heightFogColor");
            _heightFogDensity = serializedObject.FindProperty("_heightFogDensity");
            _heightFogStartPosition = serializedObject.FindProperty("_heightFogStartPosition");
            _fogHeight = serializedObject.FindProperty("_fogHeight");
            _useAlphaValue = serializedObject.FindProperty("_useAlphaValue");
            _localHeightFog = serializedObject.FindProperty("_localHeightFog");

            _distanceFog = serializedObject.FindProperty("_distanceFog");
            _distanceFogColor = serializedObject.FindProperty("_distanceFogColor");
            _distanceFogStartPosition = serializedObject.FindProperty("_distanceFogStartPosition");
            _distanceFogLength = serializedObject.FindProperty("_distanceFogLength");
            _distanceFogDensity = serializedObject.FindProperty("_distanceFogDensity");
            _useAlpha = serializedObject.FindProperty("_useAlpha");
            _worldDistanceFog = serializedObject.FindProperty("_worldDistanceFog");
            _worldDistanceFogPosition = serializedObject.FindProperty("_worldDistanceFogPosition");

            _light = serializedObject.FindProperty("_light");
            _directionalLight = serializedObject.FindProperty("_directionalLight");
            _ambient = serializedObject.FindProperty("_ambient");

            _simulatedLight = serializedObject.FindProperty("_simulatedLight");
            _simulatedLightRampTexture = serializedObject.FindProperty("_simulatedLightRampTexture");
            _simulatedLightRampTextureTiling = serializedObject.FindProperty("_simulatedLightRampTextureTiling");
            _simulatedLightRampTextureOffset = serializedObject.FindProperty("_simulatedLightRampTextureOffset");
            _simulatedLightLevel = serializedObject.FindProperty("_simulatedLightLevel");
            _simulatedLightPosition = serializedObject.FindProperty("_simulatedLightPosition");
            _simulatedLightDistance = serializedObject.FindProperty("_simulatedLightDistance");
            _gradientRamp = serializedObject.FindProperty("_gradientRamp");
            _centerColor = serializedObject.FindProperty("_centerColor");
            _useExternalColor = serializedObject.FindProperty("_useExternalColor");
            _externalColor = serializedObject.FindProperty("_externalColor");
            _additiveSimulatedLight = serializedObject.FindProperty("_additiveSimulatedLight");
            _additiveSimulatedLightLevel = serializedObject.FindProperty("_additiveSimulatedLightLevel");
            _posterize = serializedObject.FindProperty("_posterize");
            _steps = serializedObject.FindProperty("_steps");

            _lightmap = serializedObject.FindProperty("_lightmap");
            _lightmapColor = serializedObject.FindProperty("_lightmapColor");
            _lightmapLevel = serializedObject.FindProperty("_lightmapLevel");
            _shadowLevel = serializedObject.FindProperty("_shadowLevel");
            _multiplyLightmap = serializedObject.FindProperty("_multiplyLightmap");
            _desaturateLightColor = serializedObject.FindProperty("_desaturateLightColor");

            myMaterial = serializedObject.FindProperty("_myMaterial");
        }

        public override void OnInspectorGUI()
        {

            serializedObject.Update();

            CGFEditorUtilitiesClass.BuildComponentTools("http://chloroplastgames.com/cg-framework-user-manual/", serializedObject);

            CGFEditorUtilitiesClass.ManageComponentValues<T>();

            CGFEditorUtilitiesClass.BackUpComponentValues<T>(serializedObject);

            DrawMode();

            GUILayout.Space(25);

            DrawColorsByNormals();

            GUILayout.Space(10);

            DrawTextures();

            GUILayout.Space(10);

            DrawTextureLevel();

            GUILayout.Space(10);

            DrawColorGradient();

            GUILayout.Space(10);

            DrawOpacityLevel();

            GUILayout.Space(10);

            DrawViewDirection();

            GUILayout.Space(10);

            DrawFog();

            GUILayout.Space(10);

            DrawLight();

            GUILayout.Space(10);

            DrawSimulatedLight();

            GUILayout.Space(10);

            DrawLightmap();

            if (serializedObject != null && serializedObject.targetObject != null)
            {
                serializedObject.ApplyModifiedProperties();
            }           

        }

        protected virtual void DrawMode()
        {

        }

        protected virtual void DrawColorsByNormals()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(_colorsByNormals, EditorStyles.boldLabel);

            EditorGUILayout.Space();
        }

        protected virtual void DrawTextures()
        {
            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildTexture("Main Texture" + _colorType, "Texture of all normals.", _mainTexture, _mainTextureTiling, _mainTextureOffset);
            CGFEditorUtilitiesClass.BuildTexture("Front Texture" + _colorType, "Texture of the front normals.", _frontTexture, _frontTextureTiling, _frontTextureOffset);
            CGFEditorUtilitiesClass.BuildTexture("Right Texture" + _colorType, "Texture of the right normals.", _rightTexture, _rightTextureTiling, _rightTextureOffset);
            CGFEditorUtilitiesClass.BuildTexture("Top Texture" + _colorType, "Texture of the top normals.", _topTexture, _topTextureTiling, _topTextureOffset);
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawTextureLevel()
        {
            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildFloatSlider("Main Texture Level", "Level of main texture in relation the source color.", _mainTextLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Front Texture Level", "Level of front texture in relation the source color.", _frontTextLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Right Texture Level", "Level of right texture in relation the source color.", _rightTextLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Top Texture Level", "Level of top texture in relation the source color.", _topTextLevel, 0, 1);
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawColorGradient()
        {

        }

        protected virtual void DrawGradient(string colorType, SerializedProperty colorGradient, SerializedProperty topColorGradient, SerializedProperty gradientCenter, SerializedProperty gradientWidth, SerializedProperty gradientRevert, SerializedProperty gradientChangeDirection, SerializedProperty gradientRotation)
        {
            EditorGUILayout.BeginVertical();
            DrawKeyword(colorType + "Color Gradient", colorGradient);
            CGFEditorUtilitiesClass.SetKeyword(myMaterial, colorGradient, true);
            EditorGUI.BeginDisabledGroup(colorGradient.boolValue == false);
            CGFEditorUtilitiesClass.BuildColor(colorType + "Top Color Gradient" + _colorType, "Color of the top part of the gradient.", topColorGradient);
            CGFEditorUtilitiesClass.BuildFloatSlider(colorType + "Gradient Center", "Gradient center.", gradientCenter, 0, 1);
            CGFEditorUtilitiesClass.BuildFloat(colorType + "Gradient Width", "Gradient center.", gradientWidth);
            CGFEditorUtilitiesClass.BuildBoolean(colorType + "Gradient Revert", "Revert the ortientation of the gradient.", gradientRevert);
            CGFEditorUtilitiesClass.BuildBoolean(colorType + "Gradient Change Direction", "Change direction of the gradient.", gradientChangeDirection);
            CGFEditorUtilitiesClass.BuildFloat(colorType + "Gradient Rotation", "Gradient rotation.", gradientRotation);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawOpacityLevel()
        {

        }

        protected virtual void DrawViewDirection()
        {
            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildBoolean("View direction", "If enabled the color is applied based on the view direction.", _viewDirection);
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawFog()
        {
            
            DrawFog("Fog by vertex height.", "Height", "Height", _heightFog, _heightFogColor, _heightFogStartPosition, _fogHeight, _heightFogDensity, _useAlphaValue, _localHeightFog);
            GUILayout.Space(10);
            DrawFog("Fog by camera distance.", "Distance", "Distance Fog End Position", _distanceFog, _distanceFogColor, _distanceFogStartPosition, _distanceFogLength, _distanceFogDensity, _useAlpha, _worldDistanceFogPosition, _worldDistanceFog);
            
        }

        protected virtual void DrawFog(string fogDescription, string fogType, string endName, SerializedProperty fog, SerializedProperty fogColor, SerializedProperty fogStartPosition, SerializedProperty fogEnd, SerializedProperty fogDensity, SerializedProperty alpha, SerializedProperty localFog)
        {
            
            DrawHeaderWithKeyword(fogType + " Fog", fogDescription, fog);
            CGFEditorUtilitiesClass.SetKeyword(myMaterial, fog, true);
            EditorGUI.BeginDisabledGroup(fog.boolValue == false);
            CGFEditorUtilitiesClass.BuildColor(fogType + " Fog Color" + _colorType, "Color of the fog.", fogColor);
            CGFEditorUtilitiesClass.BuildFloat(fogType + " Fog Start Position ", "Start point of the fog.", fogStartPosition);
            CGFEditorUtilitiesClass.BuildFloat(endName, "End point of the fog.", fogEnd);
            CGFEditorUtilitiesClass.BuildFloatSlider(fogType + " Fog Density", "Level of fog in relation the source color.", fogDensity, 0, 1);
            CGFEditorUtilitiesClass.BuildBoolean("Use Alpha", "If enabled fog doesn't affect the transparent parts of the source color.", alpha);
            CGFEditorUtilitiesClass.BuildBoolean("Local " + fogType + " Fog", "If enabled the fog is created based on the center of the world.", localFog);
            EditorGUI.EndDisabledGroup();
            
        }

        protected virtual void DrawFog(string fogDescription, string fogType, string endName, SerializedProperty fog, SerializedProperty fogColor, SerializedProperty fogStartPosition, SerializedProperty fogEnd, SerializedProperty fogDensity, SerializedProperty alpha, SerializedProperty worldFogPosition, SerializedProperty localFog)
        {

            DrawHeaderWithKeyword(fogType + " Fog", fogDescription, fog);
            CGFEditorUtilitiesClass.SetKeyword(myMaterial, fog, true);
            EditorGUI.BeginDisabledGroup(fog.boolValue == false);
            CGFEditorUtilitiesClass.BuildColor(fogType + " Fog Color" + _colorType, "Color of the fog.", fogColor);
            CGFEditorUtilitiesClass.BuildFloat(fogType + " Fog Start Position ", "Start point of the fog.", fogStartPosition);
            CGFEditorUtilitiesClass.BuildFloat(endName, "End point of the fog.", fogEnd);
            CGFEditorUtilitiesClass.BuildFloatSlider(fogType + " Fog Density", "Level of fog in relation the source color.", fogDensity, 0, 1);
            CGFEditorUtilitiesClass.BuildBoolean("Use Alpha", "If enabled fog doesn't affect the transparent parts of the source color.", alpha);
            CGFEditorUtilitiesClass.BuildBoolean("Local " + fogType + " Fog", "If enabled the fog is created based on the center of the world.", localFog);
            EditorGUI.BeginDisabledGroup(localFog.boolValue == false);
            CGFEditorUtilitiesClass.BuildBoolean("Word Distance Fog Position", "DESCRIPTION.", worldFogPosition);
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();

        }

        protected virtual void DrawLight()
        {
            EditorGUILayout.BeginVertical();
            DrawHeaderWithKeyword("Light", "Light and Ambient light.", _light);
            CGFEditorUtilitiesClass.SetKeyword(myMaterial, _light, true);
            EditorGUI.BeginDisabledGroup(_light.boolValue == false);
            CGFEditorUtilitiesClass.BuildBoolean("Directional Light", "If enabled directional light affect to the source mesh.", _directionalLight);
            CGFEditorUtilitiesClass.BuildBoolean("Ambient", "If enabled ambinet light affect to the source mesh.", _ambient);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawHeaderWithKeyword(string label, string description, SerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent(label, description), EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(property, GUIContent.none, GUILayout.Width(10));
            EditorGUIUtility.labelWidth = 0.1f;
            EditorGUILayout.LabelField("Enable");
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.EndHorizontal();
        }

        protected virtual void DrawKeyword(string label, SerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label);
            GUILayout.FlexibleSpace();
            EditorGUILayout.PropertyField(property, GUIContent.none, GUILayout.Width(10));
            EditorGUIUtility.labelWidth = 0.1f;
            EditorGUILayout.LabelField("Enable");
            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.EndHorizontal();
        }

        protected virtual void DrawSimulatedLight()
        {
            
            DrawHeaderWithKeyword("Simulated Light", "Simulated Light.", _simulatedLight);
            CGFEditorUtilitiesClass.SetKeyword(myMaterial, _simulatedLight, true);
            EditorGUI.BeginDisabledGroup(_simulatedLight.boolValue == false);
            EditorGUI.BeginDisabledGroup(_gradientRamp.boolValue == true);
            CGFEditorUtilitiesClass.BuildTexture("Simulated Light Ramp Texture (RGB)" + _colorType, "Color ramp of the simulated light based on a texture. The top part of the texture is the center of the simulated light and the bottom part is the external part of the simulated light.", _simulatedLightRampTexture, _simulatedLightRampTextureTiling, _simulatedLightRampTextureOffset);
            EditorGUI.EndDisabledGroup();
            CGFEditorUtilitiesClass.BuildFloatSlider("Simulated Light Level", "Level of simulated light in relation the source color.", _simulatedLightLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildVector3("Simulated Light Position", "World position of the simulated light.", _simulatedLightPosition);
            CGFEditorUtilitiesClass.BuildFloat("Simulated Light Distance", "Simulated light circunference diameter.", _simulatedLightDistance);
            CGFEditorUtilitiesClass.BuildBoolean("Gradient Ramp", "If enabled uses a gradient ramp between two colors instead a ramp texture.", _gradientRamp);
            EditorGUI.BeginDisabledGroup(_gradientRamp.boolValue == false);
            CGFEditorUtilitiesClass.BuildColor("Center Color (RGB)", "Color of the center of the simulated light if gradient ramp is enabled.", _centerColor);
            CGFEditorUtilitiesClass.BuildBoolean("Use External Color", "If enabled uses a color for the external part of the light instead de source color.", _useExternalColor);
            EditorGUI.BeginDisabledGroup(_useExternalColor.boolValue == false);
            CGFEditorUtilitiesClass.BuildColor("External Color (RGB)", "Color of the expernal part of the simulated light if gradient ramp is enabled.", _externalColor);
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();
            CGFEditorUtilitiesClass.BuildBoolean("Additive Simulated Light", "If enabled adds the simulated light color to the source color.", _additiveSimulatedLight);
            EditorGUI.BeginDisabledGroup(_additiveSimulatedLight.boolValue == false);
            CGFEditorUtilitiesClass.BuildFloatSlider("Additive Simulated Light Level", "Level of simulated light addition in relation the source color.", _additiveSimulatedLightLevel, 0, 1);
            EditorGUI.EndDisabledGroup();
            CGFEditorUtilitiesClass.BuildBoolean("Posterize", "If enabled converts the ramp texture or the gradient ramp to multiple regions of fewer tones.", _posterize);
            EditorGUI.BeginDisabledGroup(_posterize.boolValue == false);
            CGFEditorUtilitiesClass.BuildFloatSlider("Steps", "Color steps of the gradient ramp between center color and external color.", _steps, 2, 20);
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndDisabledGroup();
        }

        protected virtual void DrawLightmap()
        {
            EditorGUILayout.BeginVertical();
            DrawHeaderWithKeyword("Lightmap", "Lightmap.", _lightmap);
            EditorGUI.BeginDisabledGroup(_lightmap.boolValue == false);
            CGFEditorUtilitiesClass.SetKeyword(myMaterial, _lightmap, true);
            CGFEditorUtilitiesClass.BuildColor("Lightmap Color" + _colorType, "Color of the lightmap.", _lightmapColor);
            CGFEditorUtilitiesClass.BuildFloatSlider("Lightmap Level", "Level of light of the lightmap in relation the source color.", _lightmapLevel, 0, 20);
            CGFEditorUtilitiesClass.BuildFloatSlider("Shadow Level", "Level of shadow of the lightmap in relation the source color.", _shadowLevel, -1, 1);
            CGFEditorUtilitiesClass.BuildBoolean("Multply Lightmap", "If enabled the lightmap color is multiplied by the source color.", _multiplyLightmap);
            CGFEditorUtilitiesClass.BuildBoolean("Desaturate Light Color", "If enabled color of the light of the lightmap is desaturated to grey scale.", _desaturateLightColor);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
        }
    }
}