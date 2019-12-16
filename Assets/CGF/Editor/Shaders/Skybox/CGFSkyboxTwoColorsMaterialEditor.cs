///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 13/05/2019
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: Pau Elias Soriano
/// Description: Editor of material with shader Skybox/Two Colors.
///

using UnityEngine;
using UnityEditor;

/// \english
/// <summary>
/// Editor of material with shader Skybox/Two Colors.
/// </summary>
/// \endenglish
/// \spanish
/// <summary>
/// Editor del material con el shader Skybox/Two Colors.
/// </summary>
/// \endspanish
public class CGFSkyboxTwoColorsMaterialEditor : CGFMaterialEditorClass
{

    #region Private Variables

    MaterialProperty _TopColor;
    MaterialProperty _BottomColor;
    MaterialProperty _Intensity;
    MaterialProperty _Exposure;
    MaterialProperty _RotationX;
    MaterialProperty _RotationY;

    #endregion


    #region Main Methods

    protected override void GetProperties()
    {

        _TopColor = FindProperty("_TopColor");
        _BottomColor = FindProperty("_BottomColor");
        _Intensity = FindProperty("_Intensity");
        _Exposure = FindProperty("_Exposure");
        _RotationX = FindProperty("_RotationX");
        _RotationY = FindProperty("_RotationY");

    }

    protected override void InspectorGUI()
    {

        CGFMaterialEditorUtilitiesClass.BuildMaterialTools("http://chloroplastgames.com/cg-framework-user-manual/");

        CGFMaterialEditorUtilitiesClass.ManageMaterialValues(this);

        GUILayout.Space(25);

        CGFMaterialEditorUtilitiesClass.BuildColor("Top Color (RGB)", "Color of the top part of the sky.", _TopColor);
        CGFMaterialEditorUtilitiesClass.BuildColor("Bottom Color (RGB)", "Color of the bottom part of the sky.", _BottomColor);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Intensity", "Intensity of the skybox color light.", _Intensity);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Exposure", "Adjusts the brightness of the skybox.", _Exposure);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rotation X", "Manages the pitch of the skybox.", _RotationX);
        CGFMaterialEditorUtilitiesClass.BuildSlider("Rotation Y", "Manages the yaw of the skybox.", _RotationY);

        GUILayout.Space(25);

        CGFMaterialEditorUtilitiesClass.BuildOtherSettings(true, false, false, false, this);

    }

    #endregion

}