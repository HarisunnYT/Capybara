﻿///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 22/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Material editor of the shader Flat Lighting/Four Lights.
///

using UnityEngine;
using UnityEditor;
using CGF.Systems.Shaders.FlatLighting;

/// \english
/// <summary>
/// Material editor of the shader Flat Lighting/Four Lights.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Editor del material del shader Flat Lighting/Four Lights.
/// </summary>
/// \endspanish
[CanEditMultipleObjects]
public class CGFFlatLightingFourLightsMaterialEditor : CGFMaterialEditorClass
{

    #region Private Variables

    private bool _compactMode;

    private bool _showHeigthFogGizmo;

    private bool _showDistanceFogGizmo;

    private bool _showSimulatedLightGizmo;

    // 4 Lights By Normals
    MaterialProperty _Color;

    MaterialProperty _FrontLightLevel;
    MaterialProperty _RightLightLevel;
    MaterialProperty _TopLightLevel;
    MaterialProperty _RimLightLevel;

    MaterialProperty _FrontOpacityLevel;
    MaterialProperty _RightOpacityLevel;
    MaterialProperty _TopOpacityLevel;
    MaterialProperty _RimOpacityLevel;

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

    // Render Mode
    MaterialProperty _RenderMode;

    #endregion


    #region Main Methods

    protected override void GetProperties()
    {

        // 4 Lights By Normals
        _Color = FindProperty("_Color");

        _FrontLightLevel = FindProperty("_FrontLightLevel");
        _RightLightLevel = FindProperty("_RightLightLevel");
        _TopLightLevel = FindProperty("_TopLightLevel");
        _RimLightLevel = FindProperty("_RimLightLevel");

        _FrontOpacityLevel = FindProperty("_FrontOpacityLevel");
        _RightOpacityLevel = FindProperty("_RightOpacityLevel");
        _TopOpacityLevel = FindProperty("_TopOpacityLevel");
        _RimOpacityLevel = FindProperty("_RimOpacityLevel");

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

        // Render Mode
        _RenderMode = FindProperty("_RenderMode");

    }

    protected override void InspectorGUI()
    {

        // Render Settings
        float useAlpha = _RenderMode.floatValue == 1 ? 1 : 0;

        float useAlphaClip = _RenderMode.floatValue == 2 ? 1 : 0;

        float useAlphaAndAlphaClip = _RenderMode.floatValue == 1 || _RenderMode.floatValue == 2 ? 1 : 0;


        CGFMaterialEditorUtilitiesClass.BuildMaterialComponent(typeof(CGFFlatLightingFourLightsBehavior));

        CGFMaterialEditorUtilitiesClass.BuildMaterialTools("http://chloroplastgames.com/cg-framework-user-manual/");

        CGFMaterialEditorUtilitiesClass.ManageMaterialValues(this);

        _compactMode = CGFMaterialEditorUtilitiesClass.BuildTextureCompactMode(_compactMode);

        GUILayout.Space(25);

        CGFMaterialEditorUtilitiesClass.BuildRenderModeEnum(_RenderMode, this);

        GUILayout.Space(25);

        // 4 Lights By Normals With Transparency
        CGFMaterialEditorUtilitiesClass.BuildHeader("4 Lights By Normals", "4 lights by normal direction.");
        CGFMaterialEditorUtilitiesClass.BuildColor("Color  " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the normals.", _Color);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Light Level", "Brightness of the light of the front normals.", _FrontLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Light Level", "Brightness of the light of the right normals.", _RightLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Light Level", "Brightness of the light of the top normals.", _TopLightLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rim Light Level", "Brightness of the light of the rim normals.", _RimLightLevel);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Opacity Level", "Opacity of the color of the front normals.", _FrontOpacityLevel, useAlpha);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Opacity Level", "Opacity of the color of the right normals.", _RightOpacityLevel, useAlpha);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Opacity Level", "Opacity of the color of the top normals.", _TopOpacityLevel, useAlpha);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rim Opacity Level", "Opacity of the color of the rim normals.", _RimOpacityLevel, useAlpha);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Main Texture  " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of all normals.", _MainTex, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Front Texture  " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the front normals.", _FrontTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Right Texture  " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the right normals.", _RightTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Top Texture  " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the top normals.", _TopTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Rim Texture  " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the rim normals.", _RimTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Alpha Cutoff", "Alpha Cutoff value.", _Cutoff, useAlphaClip);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Main Texture Level", "Level of main texture in relation the source color.", _MainTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Texture Level", "Level of front texture in relation the source color.", _FrontTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Texture Level", "Level of right texture in relation the source color.", _RightTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Texture Level", "Level of top texture in relation the source color.", _TopTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rim Texture Level", "Level of rim texture in relation the source color.", _RimTextureLevel);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Color Gradient", "Color gradient.", _Gradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Top Color  " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _GradientTopColor, _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Gradient Center", "Gradient center.", _GradientCenter, _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Gradient Width", "Gradient width.", _GradientWidth, _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Gradient Revert", "Revert the ortientation of the gradient.", _GradientRevert, toggleLock: _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Gradient Change Direction", "Change direction of the gradient.", _GradientChangeDirection, toggleLock: _Gradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Gradient Rotation", "Gradient rotation.", _GradientRotation, _Gradient.floatValue);
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