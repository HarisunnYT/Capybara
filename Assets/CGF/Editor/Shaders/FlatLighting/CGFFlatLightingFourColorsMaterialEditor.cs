///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 11/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Material editor of the shader Flat Lighting/Four Colors.
///

using UnityEngine;
using UnityEditor;
using CGF.Systems.Shaders.FlatLighting;

/// \english
/// <summary>
/// Material editor of the shader Flat Lighting/Four Colors.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Editor del material del shader Flat Lighting/Four Colors.
/// </summary>
/// \endspanish
[CanEditMultipleObjects]
public class CGFFlatLightingFourColorsMaterialEditor : CGFMaterialEditorClass
{

    #region Private Variables

    private bool _compactMode;

    private bool _showHeigthFogGizmo;

    private bool _showDistanceFogGizmo;

    private bool _showSimulatedLightGizmo;

    // 4 Colors By Normals With Transparency
    MaterialProperty _FrontColor;
    MaterialProperty _RightColor;
    MaterialProperty _TopColor;
    MaterialProperty _RimColor;

    MaterialProperty _MainTex;
    MaterialProperty _FrontTexture;
    MaterialProperty _RightTexture;
    MaterialProperty _TopTexture;
    MaterialProperty _RimTexture;
    MaterialProperty _Cutoff;

    MaterialProperty _MainTextureLevel;
    MaterialProperty _FrontTextureLevel;
    MaterialProperty _RightTextureLevel;
    MaterialProperty _TopTextureLevel;
    MaterialProperty _RimTextureLevel;

    MaterialProperty _FrontGradient;
    MaterialProperty _FrontTopColor;
    MaterialProperty _FrontGradientCenter;
    MaterialProperty _FrontGradientWidth;
    MaterialProperty _FrontGradientRevert;
    MaterialProperty _FrontGradientChangeDirection;
    MaterialProperty _FrontGradientRotation;

    MaterialProperty _RightGradient;
    MaterialProperty _RightTopColor;
    MaterialProperty _RightGradientCenter;
    MaterialProperty _RightGradientWidth;
    MaterialProperty _RightGradientRevert;
    MaterialProperty _RightGradientChangeDirection;
    MaterialProperty _RightGradientRotation;

    MaterialProperty _TopGradient;
    MaterialProperty _TopTopColor;
    MaterialProperty _TopGradientCenter;
    MaterialProperty _TopGradientWidth;
    MaterialProperty _TopGradientRevert;
    MaterialProperty _TopGradientChangeDirection;
    MaterialProperty _TopGradientRotation;

    MaterialProperty _RimGradient;
    MaterialProperty _RimTopColor;
    MaterialProperty _RimGradientCenter;
    MaterialProperty _RimGradientWidth;
    MaterialProperty _RimGradientRevert;
    MaterialProperty _RimGradientChangeDirection;
    MaterialProperty _RimGradientRotation;

    MaterialProperty _OpacityLevel;

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

    // Render Mode
    MaterialProperty _RenderMode;

    #endregion


    #region Main Methods

    protected override void GetProperties()
    {

        // 4 Colors By Normals With Transparency
        _FrontColor = FindProperty("_FrontColor");
        _RightColor = FindProperty("_RightColor");
        _TopColor = FindProperty("_TopColor");
        _RimColor = FindProperty("_RimColor");

        _MainTex = FindProperty("_MainTex");
        _FrontTexture = FindProperty("_FrontTexture");
        _RightTexture = FindProperty("_RightTexture");
        _TopTexture = FindProperty("_TopTexture");
        _RimTexture = FindProperty("_RimTexture");
        _Cutoff = FindProperty("_Cutoff");

        _MainTextureLevel = FindProperty("_MainTextureLevel");
        _FrontTextureLevel = FindProperty("_FrontTextureLevel");
        _RightTextureLevel = FindProperty("_RightTextureLevel");
        _TopTextureLevel = FindProperty("_TopTextureLevel");
        _RimTextureLevel = FindProperty("_RimTextureLevel");

        _FrontGradient = FindProperty("_FrontGradient");
        _FrontTopColor = FindProperty("_FrontTopColor");
        _FrontGradientCenter = FindProperty("_FrontGradientCenter");
        _FrontGradientWidth = FindProperty("_FrontGradientWidth");
        _FrontGradientRevert = FindProperty("_FrontGradientRevert");
        _FrontGradientChangeDirection = FindProperty("_FrontGradientChangeDirection");
        _FrontGradientRotation = FindProperty("_FrontGradientRotation");

        _RightGradient = FindProperty("_RightGradient");
        _RightTopColor = FindProperty("_RightTopColor");
        _RightGradientCenter = FindProperty("_RightGradientCenter");
        _RightGradientWidth = FindProperty("_RightGradientWidth");
        _RightGradientRevert = FindProperty("_RightGradientRevert");
        _RightGradientChangeDirection = FindProperty("_RightGradientChangeDirection");
        _RightGradientRotation = FindProperty("_RightGradientRotation");

        _TopGradient = FindProperty("_TopGradient");
        _TopTopColor = FindProperty("_TopTopColor");
        _TopGradientCenter = FindProperty("_TopGradientCenter");
        _TopGradientWidth = FindProperty("_TopGradientWidth");
        _TopGradientRevert = FindProperty("_TopGradientRevert");
        _TopGradientChangeDirection = FindProperty("_TopGradientChangeDirection");
        _TopGradientRotation = FindProperty("_TopGradientRotation");

        _RimGradient = FindProperty("_RimGradient");
        _RimTopColor = FindProperty("_RimTopColor");
        _RimGradientCenter = FindProperty("_RimGradientCenter");
        _RimGradientWidth = FindProperty("_RimGradientWidth");
        _RimGradientRevert = FindProperty("_RimGradientRevert");
        _RimGradientChangeDirection = FindProperty("_RimGradientChangeDirection");
        _RimGradientRotation = FindProperty("_RimGradientRotation");

        _OpacityLevel = FindProperty("_OpacityLevel");

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

        // Render Mode
        _RenderMode = FindProperty("_RenderMode");

    }

    protected override void InspectorGUI()
    {
        // Render Settings
        float useAlpha = _RenderMode.floatValue == 1 ? 1 : 0;

        float useAlphaClip = _RenderMode.floatValue == 2 ? 1 : 0;

        float useAlphaAndAlphaClip = _RenderMode.floatValue == 1 || _RenderMode.floatValue == 2 ? 1 : 0;

        CGFMaterialEditorUtilitiesClass.BuildMaterialComponent(typeof(CGFFlatLightingFourColorsBehavior));

        CGFMaterialEditorUtilitiesClass.BuildMaterialTools("http://chloroplastgames.com/cg-framework-user-manual/");

        CGFMaterialEditorUtilitiesClass.ManageMaterialValues(this);

        _compactMode = CGFMaterialEditorUtilitiesClass.BuildTextureCompactMode(_compactMode);

        GUILayout.Space(25);

        CGFMaterialEditorUtilitiesClass.BuildRenderModeEnum(_RenderMode, this);

        GUILayout.Space(25);

        // 4 Colors By Normals With Transparency
        CGFMaterialEditorUtilitiesClass.BuildHeader("4 Colors By Normals", "4 colors by normal direction.");
        CGFMaterialEditorUtilitiesClass.BuildColor("Front Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the front normals.", _FrontColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Right Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the right normals.", _RightColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top normals.", _TopColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Rim Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the rim normals.", _RimColor);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildTexture("Main Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of all normals.", _MainTex, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Front Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the front normals.", _FrontTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Right Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the right normals.", _RightTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Top Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the top normals.", _TopTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Rim Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the rim normals.", _RimTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Alpha Cutoff", "Alpha Cutoff value.", _Cutoff, useAlphaClip);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildSlider("Main Texture Level", "Level of main texture in relation the source color.", _MainTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Texture Level", "Level of front texture in relation the source color.", _FrontTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Texture Level", "Level of right texture in relation the source color.", _RightTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Texture Level", "Level of top texture in relation the source color.", _TopTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rim Texture Level", "Level of rim texture in relation the source color.", _RimTextureLevel);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildKeyword("Front Color Gradient", "Front color gradient.", _FrontGradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Front Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _FrontTopColor, _FrontGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Gradient Center", "Gradient center.", _FrontGradientCenter, _FrontGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Front Gradient Width", "Gradient width.", _FrontGradientWidth, _FrontGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Front Gradient Revert", "Revert the ortientation of the gradient.", _FrontGradientRevert, toggleLock: _FrontGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Front Gradient Change Direction", "Change direction of the gradient.", _FrontGradientChangeDirection, toggleLock: _FrontGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Gradient Rotation", "Gradient rotation.", _FrontGradientRotation, _FrontGradient.floatValue);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildKeyword("Right Color Gradient", "Right color gradient.", _RightGradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Right Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _RightTopColor, _RightGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Gradient Center", "Gradient center.", _RightGradientCenter, _RightGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Right Gradient Width", "Gradient width.", _RightGradientWidth, _RightGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Right Gradient Revert", "Revert the ortientation of the gradient.", _RightGradientRevert, toggleLock: _RightGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Right Gradient Change Direction", "Change direction of the gradient.", _RightGradientChangeDirection, toggleLock: _RightGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Gradient Rotation", "Gradient rotation.", _RightGradientRotation, _RightGradient.floatValue);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildKeyword("Top Color Gradient", "Top color gradient.", _TopGradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Top Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _TopTopColor, _TopGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Gradient Center", "Gradient center.", _TopGradientCenter, _TopGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Top Gradient Width", "Gradient width.", _TopGradientWidth, _TopGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Top Gradient Revert", "Revert the ortientation of the gradient.", _TopGradientRevert, toggleLock: _TopGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Top Gradient Change Direction", "Change direction of the gradient.", _TopGradientChangeDirection, toggleLock: _TopGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Gradient Rotation", "Gradient rotation.", _TopGradientRotation, _TopGradient.floatValue);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildKeyword("Rim Color Gradient", "Rim color gradient.", _RimGradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Rim Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _RimTopColor, _RimGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rim Gradient Center", "Gradient center.", _RimGradientCenter, _RimGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Rim Gradient Width", "Gradient width.", _RimGradientWidth, _RimGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Rim Gradient Revert", "Revert the ortientation of the gradient.", _RimGradientRevert, toggleLock: _RimGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Rim Gradient Change Direction", "Change direction of the gradient.", _RimGradientChangeDirection, toggleLock: _RimGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rim Gradient Rotation", "Gradient rotation.", _RimGradientRotation, _RimGradient.floatValue);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildSlider("Opacity level", "Overall opacity value.", _OpacityLevel, useAlpha);

        GUILayout.Space(10);

        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("View direction", "If enabled the color is applied based on the view direction.", _ViewDirection);

        GUILayout.Space(25);

        // Height Fog
        CGFMaterialEditorUtilitiesExtendedClass.BuildHeightFog(_HeightFog, _HeightFogColor, _HeightFogStartPosition, _FogHeight, _HeightFogDensity, _UseAlphaValue, _LocalHeightFog, useAlphaAndAlphaClip);

        _showHeigthFogGizmo = CGFMaterialEditorUtilitiesExtendedClass.BuildShowGizmo(_showHeigthFogGizmo, "Height Fog Gizmo", "If enabled show height fog gizmo.", _HeightFog.floatValue, _HeightFog);

        // Distance Fog
        CGFMaterialEditorUtilitiesExtendedClass.BuildDistanceFog(_DistanceFog, _DistanceFogColor, _DistanceFogStartPosition, _DistanceFogLength, _DistanceFogDensity, _UseAlpha, _WorldDistanceFog, _WorldDistanceFogPosition, useAlphaAndAlphaClip);

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