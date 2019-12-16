///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of CGFDistanceFogManager.
///

using UnityEditor;
using CGF.Systems.Shaders.Managers;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Editor of CGFDistanceFogManager.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor de CGFDistanceFogManager.
    /// </summary>
    /// \endspanish
    [CustomEditor(typeof(CGFDistanceFogManager))]
    [CanEditMultipleObjects]
    public class CGFDistanceFogManagerEditor : CGFShaderManagerEditor<CGFDistanceFogManager>
    {

        private static SerializedProperty _distanceFog;

        private static SerializedProperty _distanceFogColor;

        private static SerializedProperty _distanceFogStartPosition;

        private static SerializedProperty _distanceFogLength;

        private static SerializedProperty _distanceFogDensity;

        private static SerializedProperty _useAlpha;

        private static SerializedProperty _worldDistanceFog;

        private static SerializedProperty _worldDistanceFogPosition;

        protected override void OnEnable()
        {

            base.OnEnable();

            GetProperties();

        }

        private void GetProperties()
        {
            _distanceFog = serializedObject.FindProperty("_distanceFog");

            _distanceFogColor = serializedObject.FindProperty("_distanceFogColor");

            _distanceFogStartPosition = serializedObject.FindProperty("_distanceFogStartPosition");

            _distanceFogLength = serializedObject.FindProperty("_distanceFogLength");

            _distanceFogDensity = serializedObject.FindProperty("_distanceFogDensity");

            _useAlpha = serializedObject.FindProperty("_useAlpha");

            _worldDistanceFog = serializedObject.FindProperty("_worldDistanceFog");

            _worldDistanceFogPosition = serializedObject.FindProperty("_worldDistanceFogPosition");
        }

        private void OnSceneGUI()
        {

            CGFEditorUtilitiesExtendedClass.DrawDistanceFogSphereHandle(_distanceFog, _distanceFogStartPosition, _distanceFogLength, _worldDistanceFog, _worldDistanceFogPosition, true, this);

        }

        protected override void DrawContent()
        {

            GetProperties();

            DrawFog("Distance", "Fog by camera distance.", "Distance Fog Length", _distanceFog, _distanceFogColor, _distanceFogStartPosition, _distanceFogLength, _distanceFogDensity, _useAlpha, _worldDistanceFog, _worldDistanceFogPosition);

        }

    }

}
