///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of the behavior of material of the shader CG Framework/Flat Lighting/Six Colors.
///

using UnityEditor;
using Assets.CGF.Editor;
using CGF.Systems.Shaders.FlatLighting;
using UnityEngine;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Editor of the behavior of material of the shader CG Framework/Flat Lighting/Six Colors.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor del comportamiento del material del shader CG Framework/Flat Lighting/Six Colors.
    /// </summary>
    /// \endspanish
    [CustomEditor(typeof(CGFFlatLightingSixColorsBehavior))]
    [CanEditMultipleObjects]
    public class CGFFlatLightingSixColorsBehaviorEditor : CGFFlatLightingSixColorsBaseEditor<CGFFlatLightingSixColorsBehavior>
    {

        SerializedProperty _renderMode;

        protected override void OnEnable()
        {
            base.OnEnable();

            _renderMode = serializedObject.FindProperty("_renderMode");

            CGFEditorUtilitiesClass.SetBlendMode(myMaterial.objectReferenceValue as Material, (CGFEditorUtilitiesClass.BlendMode)_renderMode.floatValue);
        }

        protected override void DrawMode()
        {
            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildRenderModeEnum(_renderMode, myMaterial);
            if (_renderMode.floatValue == 3 || _renderMode.floatValue == 0)
            {
                _colorType = " (RGB)";
            }
            else
            {
                _colorType = " (RGBA)";
            }
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