///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of the behavior of material of the shader CG Framework/Flat Lighting/Four Colors Blend.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;
using CGF.Systems.Shaders.FlatLighting;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Editor of the behavior of material of the shader CG Framework/Flat Lighting/Four Colors Blend.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor de los comportamientos del material del shader CG Framework/Flat Lighting/Four Colors Blend.
    /// </summary>
    /// \endspanish
    [CustomEditor(typeof(CGFFlatLightingFourColorsBlendBehavior))]
    [CanEditMultipleObjects]
    public class CGFFlatLightingFourColorsBlendBehaviorEditor : CGFFlatLightingFourColorsBaseEditor<CGFFlatLightingFourColorsBlendBehavior>
    {

        SerializedProperty _blendMode;

        protected override void OnEnable()
        {
            base.OnEnable();

            _blendMode = serializedObject.FindProperty("_blendMode");

            CGFEditorUtilitiesClass.SetBlendMode(myMaterial.objectReferenceValue as Material, (CGFEditorUtilitiesClass.BlendMode)_blendMode.floatValue);
        }

        protected override void DrawMode()
        {
            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildBlendModeEnum(_blendMode, myMaterial);

            EditorGUILayout.EndVertical();
        }

        protected override void DrawFog(string fogDescription, string fogType, string endName, SerializedProperty fog, SerializedProperty fogColor, SerializedProperty fogStartPosition, SerializedProperty fogEnd, SerializedProperty fogDensity, SerializedProperty alpha, SerializedProperty localFog)
        {
            DrawHeaderWithKeyword(fogType + " Fog", fogDescription, fog);
            CGFEditorUtilitiesClass.SetKeyword(myMaterial, fog, true);
            EditorGUI.BeginDisabledGroup(fog.boolValue == false);
            CGFEditorUtilitiesClass.BuildColor(fogType + " Fog Color" + " (RGB)", "Color of the fog.", fogColor);
            CGFEditorUtilitiesClass.BuildFloat(fogType + " Fog Start Position ", "Start point of the fog.", fogStartPosition);
            CGFEditorUtilitiesClass.BuildFloat(endName, "End point of the fog.", fogEnd);
            CGFEditorUtilitiesClass.BuildFloatSlider(fogType + " Fog Density", "Level of fog in relation the source color.", fogDensity, 0, 1);
            CGFEditorUtilitiesClass.BuildBoolean("Use Alpha", "If enabled fog doesn't affect the transparent parts of the source color.", alpha);
            CGFEditorUtilitiesClass.BuildBoolean("Local " + fogType + " Fog", "If enabled the fog is created based on the center of the world.", localFog);
            EditorGUI.EndDisabledGroup();
        }

    }

}