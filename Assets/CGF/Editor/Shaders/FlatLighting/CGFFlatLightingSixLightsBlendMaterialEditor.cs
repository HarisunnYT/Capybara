///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 22/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Material editor of the shader Flat Lighting/Six Lights Blend.
///

using UnityEngine;
using UnityEditor;
using CGF.Systems.Shaders.FlatLighting;

/// \english
/// <summary>
/// Material editor of the shader Flat Lighting/Six Lights Blend.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Editor del material del shader Flat Lighting/Six Lights Blend.
/// </summary>
/// \endspanish
[CanEditMultipleObjects]
public class CGFFlatLightingSixLightsBlendMaterialEditor : CGFMaterialEditorClass
{

    #region Private Variables

    private bool _compactMode;

    private bool _showHeigthFogGizmo;

    private bool _showDistanceFogGizmo;

    private bool _showSimulatedLightGizmo;

    // 6 Lights By Normals With Transparency
    MaterialProperty _Color;

    MaterialProperty _FrontLightLevel;
    MaterialProperty _RightLightLevel;
    MaterialProperty _TopLightLevel;
    MaterialProperty _BackLightLevel;
    MaterialProperty _LeftLightLevel;
    MaterialProperty _BottomLightLevel;

    MaterialProperty _FrontOpacityLevel;
    MaterialProperty _RightOpacityLevel;
    MaterialProperty _TopOpacityLevel;
    MaterialProperty _BackOpacityLevel;
    MaterialProperty _LeftOpacityLevel;
    MaterialProperty _BottomOpacityLevel;

    MaterialProperty _MainTex;
    MaterialProperty _FrontTexture;
    MaterialProperty _RightTexture;
    MaterialProperty _TopTexture;
    MaterialProperty _BackTexture;
    MaterialProperty _LeftTexture;
    MaterialProperty _BottomTexture;
    MaterialProperty _UseAlphaClip;
    MaterialProperty _Cutoff;

    MaterialProperty _MainTextureLevel;
    MaterialProperty _FrontTextureLevel;
    MaterialProperty _RightTextureLevel;
    MaterialProperty _TopTextureLevel;
    MaterialProperty _BackTextureLevel;
    MaterialProperty _LeftTextureLevel;
    MaterialProperty _BottomTextureLevel;

    MaterialProperty _Gradient;
    MaterialProperty _GradientTopColor;
    MaterialProperty _GradientCenter;
    MaterialProperty _GradientWidth;
    MaterialProperty _GradientRevert;
    MaterialProperty _GradientChangeDirection;
    MaterialProperty _GradientRotation;

    MaterialProperty _ViewDirection;

    // Height Fog
    MaterialProperty _HeightFog;
    MaterialProperty _HeightFogColor;
    MaterialProperty _HeightFogStartPosition;
    MaterialProperty _FogHeight;
    MaterialProperty _HeightFogDensity;
    MaterialProperty _UseAlphaValue;
    MaterialProperty _LocalHeightFog;

    // Distance Fog
    MaterialProperty _DistanceFog;
    MaterialProperty _DistanceFogColor;
    MaterialProperty _DistanceFogStartPosition;
    MaterialProperty _DistanceFogLength;
    MaterialProperty _DistanceFogDensity;
    MaterialProperty _UseAlpha;
    MaterialProperty _WorldDistanceFog;
    MaterialProperty _WorldDistanceFogPosition;

    // Light
    MaterialProperty _Light;
    MaterialProperty _DirectionalLight;
    MaterialProperty _Ambient;

    // Simulated Light
    MaterialProperty _SimulatedLight;
    MaterialProperty _SimulatedLightRampTexture;
    MaterialProperty _SimulatedLightLevel;
    MaterialProperty _SimulatedLightPosition;
    MaterialProperty _SimulatedLightDistance;
    MaterialProperty _GradientRamp;
    MaterialProperty _CenterColor;
    MaterialProperty _UseExternalColor;
    MaterialProperty _ExternalColor;
    MaterialProperty _AdditiveSimulatedLight;
    MaterialProperty _AdditiveSimulatedLightLevel;
    MaterialProperty _Posterize;
    MaterialProperty _Steps;

    // Lightmap
    MaterialProperty _Lightmap;
    MaterialProperty _LightmapColor;
    MaterialProperty _LightmapLevel;
    MaterialProperty _ShadowLevel;
    MaterialProperty _MultiplyLightmap;
    MaterialProperty _DesaturateLightColor;

    // Blend Mode
    MaterialProperty _BlendMode;

    #endregion


    #region Main Methods

    protected override void GetProperties()
    {

        // 6 Lights By Normals With Transparency
        _Color = FindProperty("_Color");

        _FrontLightLevel = FindProperty("_FrontLightLevel");
        _RightLightLevel = FindProperty("_RightLightLevel");
        _TopLightLevel = FindProperty("_TopLightLevel");
        _BackLightLevel = FindProperty("_BackLightLevel");
        _LeftLightLevel = FindProperty("_LeftLightLevel");
        _BottomLightLevel = FindProperty("_BottomLightLevel");

        _FrontOpacityLevel = FindProperty("_FrontOpacityLevel");
        _RightOpacityLevel = FindProperty("_RightOpacityLevel");
        _TopOpacityLevel = FindProperty("_TopOpacityLevel");
        _BackOpacityLevel = FindProperty("_BackOpacityLevel");
        _LeftOpacityLevel = FindProperty("_LeftOpacityLevel");
        _BottomOpacityLevel = FindProperty("_BottomOpacityLevel");

        _MainTex = FindProperty("_MainTex");
        _FrontTexture = FindProperty("_FrontTexture");
        _RightTexture = FindProperty("_RightTexture");
        _TopTexture = FindProperty("_TopTexture");
        _BackTexture = FindProperty("_BackTexture");
        _LeftTexture = FindProperty("_LeftTexture");
        _BottomTexture = FindProperty("_BottomTexture");
        _UseAlphaClip = FindProperty("_UseAlphaClip");
        _Cutoff = FindProperty("_Cutoff");

        _MainTextureLevel = FindProperty("_MainTextureLevel");
        _FrontTextureLevel = FindProperty("_FrontTextureLevel");
        _RightTextureLevel = FindProperty("_RightTextureLevel");
        _TopTextureLevel = FindProperty("_TopTextureLevel");
        _BackTextureLevel = FindProperty("_BackTextureLevel");
        _LeftTextureLevel = FindProperty("_LeftTextureLevel");
        _BottomTextureLevel = FindProperty("_BottomTextureLevel");

        _Gradient = FindProperty("_Gradient");
        _GradientTopColor = FindProperty("_GradientTopColor");
        _GradientCenter = FindProperty("_GradientCenter");
        _GradientWidth = FindProperty("_GradientWidth");
        _GradientRevert = FindProperty("_GradientRevert");
        _GradientChangeDirection = FindProperty("_GradientChangeDirection");
        _GradientRotation = FindProperty("_GradientRotation");

        _ViewDirection = FindProperty("_ViewDirection");

        // Height Fog
        _HeightFog = FindProperty("_HeightFog");
        _HeightFogColor = FindProperty("_HeightFogColor");
        _HeightFogStartPosition = FindProperty("_HeightFogStartPosition");
        _FogHeight = FindProperty("_FogHeight");
        _HeightFogDensity = FindProperty("_HeightFogDensity");
        _UseAlphaValue = FindProperty("_UseAlphaValue");
        _LocalHeightFog = FindProperty("_LocalHeightFog");

        // Distance Fog
        _DistanceFog = FindProperty("_DistanceFog");
        _DistanceFogColor = FindProperty("_DistanceFogColor");
        _DistanceFogStartPosition = FindProperty("_DistanceFogStartPosition");
        _DistanceFogLength = FindProperty("_DistanceFogLength");
        _DistanceFogDensity = FindProperty("_DistanceFogDensity");
        _UseAlpha = FindProperty("_UseAlpha");
        _WorldDistanceFog = FindProperty("_WorldDistanceFog");
        _WorldDistanceFogPosition = FindProperty("_WorldDistanceFogPosition");

        // Light
        _Light = FindProperty("_Light");
        _DirectionalLight = FindProperty("_DirectionalLight");
        _Ambient = FindProperty("_Ambient");

        // Simulated Light
        _SimulatedLight = FindProperty("_SimulatedLight");
        _SimulatedLightRampTexture = FindProperty("_SimulatedLightRampTexture");
        _SimulatedLightLevel = FindProperty("_SimulatedLightLevel");
        _SimulatedLightPosition = FindProperty("_SimulatedLightPosition");
        _SimulatedLightDistance = FindProperty("_SimulatedLightDistance");
        _GradientRamp = FindProperty("_GradientRamp");
        _CenterColor = FindProperty("_CenterColor");
        _UseExternalColor = FindProperty("_UseExternalColor");
        _ExternalColor = FindProperty("_ExternalColor");
        _AdditiveSimulatedLight = FindProperty("_AdditiveSimulatedLight");
        _AdditiveSimulatedLightLevel = FindProperty("_AdditiveSimulatedLightLevel");
        _Posterize = FindProperty("_Posterize");
        _Steps = FindProperty("_Steps");

        // Lightmap
        _Lightmap = FindProperty("_Lightmap");
        _LightmapColor = FindProperty("_LightmapColor");
        _LightmapLevel = FindProperty("_LightmapLevel");
        _ShadowLevel = FindProperty("_ShadowLevel");
        _MultiplyLightmap = FindProperty("_MultiplyLightmap");
        _DesaturateLightColor = FindProperty("_DesaturateLightColor");

        // Blend Mode
        _BlendMode = FindProperty("_BlendMode");

    }

    protected override void InspectorGUI()
    {

        CGFMaterialEditorUtilitiesClass.BuildMaterialComponent(typeof(CGFFlatLightingSixLightsBlendBehavior));

        CGFMaterialEditorUtilitiesClass.BuildMaterialTools("https://twitter.com/");

        CGFMaterialEditorUtilitiesClass.ManageMaterialValues(this);

        _compactMode = CGFMaterialEditorUtilitiesClass.BuildTextureCompactMode(_compactMode);

        GUILayout.Space(25);

        CGFMaterialEditorUtilitiesClass.BuildBlendModeEnum(_BlendMode, this);

        GUILayout.Space(25);

        // 6 Lights By Normals With Transparency
        CGFMaterialEditorUtilitiesClass.BuildHeader("6 Lights By Normals", "6 lights by normal direction.");
        CGFMaterialEditorUtilitiesClass.BuildColor("Color (RGBA)", "Color of the normals.", _Color);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Light Level", "Brightness of the light of the front normals.", _FrontLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Light Level", "Brightness of the light of the right normals.", _RightLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Light Level", "Brightness of the light of the top normals.", _TopLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Light Level", "Brightness of the light of the back normals.", _BackLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Left Light Level", "Brightness of the light of the left normals.", _LeftLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Bottom Light Level", "Brightness of the light of the bottom normals.", _BottomLightLevel);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Opacity Level", "Opacity of the color of the front normals.", _FrontOpacityLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Opacity Level", "Opacity of the color of the right normals.", _RightOpacityLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Opacity Level", "Opacity of the color of the top normals.", _TopOpacityLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Opacity Level", "Opacity of the color of the back normals.", _BackOpacityLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Left Opacity Level", "Opacity of the color of the left normals.", _LeftOpacityLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Bottom Opacity Level", "Opacity of the color of the bottom normals.", _BottomOpacityLevel);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Main Texture (RGBA)", "Texture of all normals.", _MainTex, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Front Texture (RGBA)", "Texture of the front normals.", _FrontTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Right Texture (RGBA)", "Texture of the right normals.", _RightTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Top Texture (RGBA)", "Texture of the top normals.", _TopTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Back Texture (RGBA)", "Texture of the back normals.", _BackTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Left Texture (RGBA)", "Texture of the left normals.", _LeftTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Bottom Texture (RGBA)", "Texture of the bottom normals.", _BottomTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Use Alpha Clip", "Enables Alpha Clip.", _UseAlphaClip, true);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Alpha Cutoff", "Alpha Cutoff value.", _Cutoff, _UseAlphaClip.floatValue);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Main Texture Level", "Level of main texture in relation the source color.", _MainTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Texture Level", "Level of front texture in relation the source color.", _FrontTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Texture Level", "Level of right texture in relation the source color.", _RightTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Texture Level", "Level of top texture in relation the source color.", _TopTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Texture Level", "Level of back texture in relation the source color.", _BackTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Left Texture Level", "Level of left texture in relation the source color.", _LeftTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Bottom Texture Level", "Level of bottom texture in relation the source color.", _BottomTextureLevel);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Color Gradient", "Color gradient.", _Gradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Top Color (RGBA)", "Color of the top part of the gradient.", _GradientTopColor, _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Gradient Center", "Gradient center.", _GradientCenter, _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Gradient Width", "Gradient width.", _GradientWidth, _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Gradient Revert", "Revert the ortientation of the gradient.", _GradientRevert, toggleLock: _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Gradient Change Direction", "Change direction of the gradient.", _GradientChangeDirection, toggleLock: _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Gradient Rotation", "Gradient rotation.", _GradientRotation, _Gradient.floatValue);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("View direction", "If enabled the color is applied based on the view direction.", _ViewDirection);

        GUILayout.Space(25);

        // Height Fog
        CGFMaterialEditorUtilitiesExtendedClass.BuildHeightFog(_HeightFog, _HeightFogColor, _HeightFogStartPosition, _FogHeight, _HeightFogDensity, _UseAlphaValue, _LocalHeightFog, 1.0f);

        _showHeigthFogGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(_showHeigthFogGizmo, "Height Fog Gizmo", "If enabled show height fog gizmo.", _HeightFog.floatValue, _HeightFog);

        // Distance Fog
        CGFMaterialEditorUtilitiesExtendedClass.BuildDistanceFog(_DistanceFog, _DistanceFogColor, _DistanceFogStartPosition, _DistanceFogLength, _DistanceFogDensity, _UseAlpha, _WorldDistanceFog, _WorldDistanceFogPosition, 1.0f);

        _showDistanceFogGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(_showDistanceFogGizmo, "Distance Fog Gizmo", "If enabled show distance fog gizmo.", _DistanceFog.floatValue, _DistanceFog);

        // Light
        CGFMaterialEditorUtilitiesExtendedClass.BuildLight(_Light, _DirectionalLight, _Ambient);

        // Simulated Light
        CGFMaterialEditorUtilitiesExtendedClass.BuildSimulatedLight(_SimulatedLight, _SimulatedLightRampTexture, _SimulatedLightLevel, _SimulatedLightPosition, _SimulatedLightDistance, _GradientRamp, _CenterColor, _UseExternalColor, _ExternalColor, _AdditiveSimulatedLight, _AdditiveSimulatedLightLevel, _Posterize, _Steps, this, _compactMode);

        _showSimulatedLightGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(_showSimulatedLightGizmo, "Simulated Light Gizmo", "If enabled show simulated light gizmo.", _SimulatedLight.floatValue, _SimulatedLight);

        // Lightmap
        CGFMaterialEditorUtilitiesExtendedClass.BuildLightmap(_Lightmap, _LightmapColor, _LightmapLevel, _ShadowLevel, _MultiplyLightmap, _DesaturateLightColor);

        CGFMaterialEditorUtilitiesClass.BuildOtherSettings(true, true, false, false, this);

    }

    protected void OnSceneGUI()
    {

        CGFMaterialEditorUtilitiesExtendedClass.DrawHeightFogArrowHandle(_HeightFog, _HeightFogStartPosition, _FogHeight, _LocalHeightFog, _showHeigthFogGizmo, this);

        CGFMaterialEditorUtilitiesExtendedClass.DrawDistanceFogSphereHandle(_DistanceFog, _DistanceFogStartPosition, _DistanceFogLength, _WorldDistanceFog, _WorldDistanceFogPosition, _showDistanceFogGizmo, this);

        CGFMaterialEditorUtilitiesExtendedClass.DrawSphereHandle(_SimulatedLight, _SimulatedLightPosition, _SimulatedLightDistance, _CenterColor, _showSimulatedLightGizmo, true, this);

    }

    #endregion

}