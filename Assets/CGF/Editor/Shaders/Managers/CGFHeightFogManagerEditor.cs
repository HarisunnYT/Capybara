///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of CGFHeightFogManager.
///


using UnityEditor;
using CGF.Systems.Shaders.Managers;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Editor of CGFHeightFogManager.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor de CGFHeightFogManager.
    /// </summary>
    /// \endspanish
    [CustomEditor(typeof(CGFHeightFogManager))]
    [CanEditMultipleObjects]
    public class CGFHeightFogManagerEditor : CGFShaderManagerEditor<CGFHeightFogManager>
    {

        SerializedProperty _heightFog;

        SerializedProperty _heightFogColor;

        SerializedProperty _heightFogDensity;

        SerializedProperty _heightFogStartPosition;

        SerializedProperty _fogHeight;

        SerializedProperty _useAlphaValue;

        SerializedProperty _localHeightFog;

        protected override void OnEnable()
        {
            base.OnEnable();

            _heightFog = serializedObject.FindProperty("_heightFog");

            _heightFogColor = serializedObject.FindProperty("_heightFogColor");

            _heightFogDensity = serializedObject.FindProperty("_heightFogDensity");

            _heightFogStartPosition = serializedObject.FindProperty("_heightFogStartPosition");

            _fogHeight = serializedObject.FindProperty("_fogHeight");

            _useAlphaValue = serializedObject.FindProperty("_useAlphaValue");

            _localHeightFog = serializedObject.FindProperty("_localHeightFog");
        }

        private void OnSceneGUI()
        {
            CGFEditorUtilitiesExtendedClass.DrawHeightFogSphereHandle(_heightFog, _heightFogStartPosition, _fogHeight, _localHeightFog, true, this);
        }

        protected override void DrawContent()
        {

            DrawFog("Height", "Fog by vertex height.", "Height", _heightFog, _heightFogColor, _heightFogStartPosition, _fogHeight, _heightFogDensity, _useAlphaValue, _localHeightFog);

        }

    }

}
