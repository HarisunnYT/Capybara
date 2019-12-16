///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of the behavior of material of the shader CG Framework/Flat Lighting/Six Colors Blend.
///

using UnityEditor;
using Assets.CGF.Editor;
using CGF.Systems.Shaders.FlatLighting;
using UnityEngine;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Editor of the behavior of material of the shader CG Framework/Flat Lighting/Six Colors Blend.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor del comportamiento del material del shader CG Framework/Flat Lighting/Six Colors Blend.
    /// </summary>
    /// \endspanish
    [CustomEditor(typeof(CGFFlatLightingSixColorsBlendBehavior))]
    [CanEditMultipleObjects]
    public class CGFFlatLightingSixColorsBlendBehaviorEditor : CGFFlatLightingSixColorsBaseEditor<CGFFlatLightingSixColorsBlendBehavior>
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
    }

}