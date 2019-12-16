///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of the behavior of material of the shader CG Framework/Flat Lighting/Six Lights.
///

using UnityEditor;
using Assets.CGF.Editor;
using CGF.Systems.Shaders.FlatLighting;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Editor of the behavior of material of the shader CG Framework/Flat Lighting/Six Lights.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor del comportamiento del material del shader CG Framework/Flat Lighting/Six Lights.
    /// </summary>
    /// \endspanish
    [CustomEditor(typeof(CGFFlatLightingSixLightsBehavior))]
    [CanEditMultipleObjects]
    public class CGFFlatLightingSixLightsBehaviorEditor : CGFFlatLightingSixLightsBaseEditor<CGFFlatLightingSixLightsBehavior>
    {
        SerializedProperty _renderMode;

        protected override void OnEnable()
        {
            base.OnEnable();

            _renderMode = serializedObject.FindProperty("_renderMode");

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
    }

}