///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 22/03/2018
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
/// Material editor of the shader Flat Lighting/Six Colors.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Editor del material del shader Flat Lighting/Six Colors.
/// </summary>
/// \endspanish
[CanEditMultipleObjects]
public class CGFFlatLightingSixColorsMaterialEditor : CGFMaterialEditorClass
{

    #region Private Variables

    private bool _compactMode;

    private bool _showHeigthFogGizmo;

    private bool _showDistanceFogGizmo;

    private bool _showSimulatedLightGizmo;

    // 6 Colors By Normals With Transparency
    MaterialProperty _FrontColor;
    MaterialProperty _RightColor;
    MaterialProperty _TopColor;
    MaterialProperty _BackColor;
    MaterialProperty _LeftColor;
    MaterialProperty _BottomColor;

    MaterialProperty _MainTex;
    MaterialProperty _FrontTexture;
    MaterialProperty _RightTexture;
    MaterialProperty _TopTexture;
    MaterialProperty _BackTexture;
    MaterialProperty _LeftTexture;
    MaterialProperty _BottomTexture;
    MaterialProperty _Cutoff;

    MaterialProperty _MainTextureLevel;
    MaterialProperty _FrontTextureLevel;
    MaterialProperty _RightTextureLevel;
    MaterialProperty _TopTextureLevel;
    MaterialProperty _BackTextureLevel;
    MaterialProperty _LeftTextureLevel;
    MaterialProperty _BottomTextureLevel;

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

    MaterialProperty _BackGradient;
    MaterialProperty _BackTopColor;
    MaterialProperty _BackGradientCenter;
    MaterialProperty _BackGradientWidth;
    MaterialProperty _BackGradientRevert;
    MaterialProperty _BackGradientChangeDirection;
    MaterialProperty _BackGradientRotation;

    MaterialProperty _LeftGradient;
    MaterialProperty _LeftTopColor;
    MaterialProperty _LeftGradientCenter;
    MaterialProperty _LeftGradientWidth;
    MaterialProperty _LeftGradientRevert;
    MaterialProperty _LeftGradientChangeDirection;
    MaterialProperty _LeftGradientRotation;

    MaterialProperty _BottomGradient;
    MaterialProperty _BottomTopColor;
    MaterialProperty _BottomGradientCenter;
    MaterialProperty _BottomGradientWidth;
    MaterialProperty _BottomGradientRevert;
    MaterialProperty _BottomGradientChangeDirection;
    MaterialProperty _BottomGradientRotation;

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

        // 6 Colors By Normals With Transparency
        _FrontColor = FindProperty("_FrontColor");
        _RightColor = FindProperty("_RightColor");
        _TopColor = FindProperty("_TopColor");
        _BackColor = FindProperty("_BackColor");
        _LeftColor = FindProperty("_LeftColor");
        _BottomColor = FindProperty("_BottomColor");

        _MainTex = FindProperty("_MainTex");
        _FrontTexture = FindProperty("_FrontTexture");
        _RightTexture = FindProperty("_RightTexture");
        _TopTexture = FindProperty("_TopTexture");
        _BackTexture = FindProperty("_BackTexture");
        _LeftTexture = FindProperty("_LeftTexture");
        _BottomTexture = FindProperty("_BottomTexture");
        _Cutoff = FindProperty("_Cutoff");

        _MainTextureLevel = FindProperty("_MainTextureLevel");
        _FrontTextureLevel = FindProperty("_FrontTextureLevel");
        _RightTextureLevel = FindProperty("_RightTextureLevel");
        _TopTextureLevel = FindProperty("_TopTextureLevel");
        _BackTextureLevel = FindProperty("_BackTextureLevel");
        _LeftTextureLevel = FindProperty("_LeftTextureLevel");
        _BottomTextureLevel = FindProperty("_BottomTextureLevel");

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

        _BackGradient = FindProperty("_BackGradient");
        _BackTopColor = FindProperty("_BackTopColor");
        _BackGradientCenter = FindProperty("_BackGradientCenter");
        _BackGradientWidth = FindProperty("_BackGradientWidth");
        _BackGradientRevert = FindProperty("_BackGradientRevert");
        _BackGradientChangeDirection = FindProperty("_BackGradientChangeDirection");
        _BackGradientRotation = FindProperty("_BackGradientRotation");

        _LeftGradient = FindProperty("_LeftGradient");
        _LeftTopColor = FindProperty("_LeftTopColor");
        _LeftGradientCenter = FindProperty("_LeftGradientCenter");
        _LeftGradientWidth = FindProperty("_LeftGradientWidth");
        _LeftGradientRevert = FindProperty("_LeftGradientRevert");
        _LeftGradientChangeDirection = FindProperty("_LeftGradientChangeDirection");
        _LeftGradientRotation = FindProperty("_LeftGradientRotation");

        _BottomGradient = FindProperty("_BottomGradient");
        _BottomTopColor = FindProperty("_BottomTopColor");
        _BottomGradientCenter = FindProperty("_BottomGradientCenter");
        _BottomGradientWidth = FindProperty("_BottomGradientWidth");
        _BottomGradientRevert = FindProperty("_BottomGradientRevert");
        _BottomGradientChangeDirection = FindProperty("_BottomGradientChangeDirection");
        _BottomGradientRotation = FindProperty("_BottomGradientRotation");

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


        CGFMaterialEditorUtilitiesClass.BuildMaterialComponent(typeof(CGFFlatLightingSixColorsBehavior));

        CGFMaterialEditorUtilitiesClass.BuildMaterialTools("http://chloroplastgames.com/cg-framework-user-manual/");

        CGFMaterialEditorUtilitiesClass.ManageMaterialValues(this);

        _compactMode = CGFMaterialEditorUtilitiesClass.BuildTextureCompactMode(_compactMode);

        GUILayout.Space(25);

        CGFMaterialEditorUtilitiesClass.BuildRenderModeEnum(_RenderMode, this);

        GUILayout.Space(25);

        // 6 Colors By Normals With Transparency
        CGFMaterialEditorUtilitiesClass.BuildHeader("6 Colors By Normals", "6 colors by normal direction.");
        CGFMaterialEditorUtilitiesClass.BuildColor("Front Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the front normals.", _FrontColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Right Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the right normals.", _RightColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top normals.", _TopColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Back Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the back normals.", _BackColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Left Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the left normals.", _LeftColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Bottom Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the bottom normals.", _BottomColor);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Main Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of all normals.", _MainTex, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Front Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the front normals.", _FrontTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Right Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the right normals.", _RightTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Top Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the top normals.", _TopTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Back Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the back normals.", _BackTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Left Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the left normals.", _LeftTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildTexture("Bottom Texture " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Texture of the bottom normals.", _BottomTexture, this, true, _compactMode);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Alpha Cutoff", "Alpha Cutoff value.", _Cutoff, useAlphaClip);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Main Texture Level", "Level of main texture in relation the source color.", _MainTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Front Texture Level", "Level of front texture in relation the source color.", _FrontTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Right Texture Level", "Level of right texture in relation the source color.", _RightTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Top Texture Level", "Level of top texture in relation the source color.", _TopTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Texture Level", "Level of back texture in relation the source color.", _BackTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Left Texture Level", "Level of left texture in relation the source color.", _LeftTextureLevel);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Bottom Texture Level", "Level of bottom texture in relation the source color.", _BottomTextureLevel);
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
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Back Color Gradient", "Back color gradient.", _BackGradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Back Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _BackTopColor, _BackGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Gradient Center", "Gradient center.", _BackGradientCenter, _BackGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Back Gradient Width", "Gradient width.", _BackGradientWidth, _BackGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Back Gradient Revert", "Revert the ortientation of the gradient.", _BackGradientRevert, toggleLock: _BackGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Back Gradient Change Direction", "Change direction of the gradient.", _BackGradientChangeDirection, toggleLock: _BackGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Back Gradient Rotation", "Gradient rotation.", _BackGradientRotation, _BackGradient.floatValue);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Left Color Gradient", "Rim color gradient.", _LeftGradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Left Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _LeftTopColor, _LeftGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Left Gradient Center", "Gradient center.", _LeftGradientCenter, _LeftGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Left Gradient Width", "Gradient width.", _LeftGradientWidth, _LeftGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Left Gradient Revert", "Revert the ortientation of the gradient.", _LeftGradientRevert, toggleLock: _LeftGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Left Gradient Change Direction", "Change direction of the gradient.", _LeftGradientChangeDirection, toggleLock: _LeftGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Left Gradient Rotation", "Gradient rotation.", _LeftGradientRotation, _LeftGradient.floatValue);
        GUILayout.Space(10);
        CGFMaterialEditorUtilitiesClass.BuildKeyword("Bottom Color Gradient", "Rim color gradient.", _BottomGradient, true);
        CGFMaterialEditorUtilitiesClass.BuildColor("Bottom Top Color " + CGFMaterialEditorUtilitiesExtendedClass.CheckRenderMode(_RenderMode.floatValue), "Color of the top part of the gradient.", _BottomTopColor, _BottomGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Bottom Gradient Center", "Gradient center.", _BottomGradientCenter, _BottomGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildFloat("Bottom Gradient Width", "Bottom Gradient width.", _BottomGradientWidth, _BottomGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Bottom Gradient Revert", "Revert the ortientation of the gradient.", _BottomGradientRevert, toggleLock: _BottomGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildToggleFloat("Bottom Gradient Change Direction", "Change direction of the gradient.", _BottomGradientChangeDirection, toggleLock: _BottomGradient.floatValue);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Bottom Gradient Rotation", "Gradient rotation.", _BottomGradientRotation, _BottomGradient.floatValue);
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