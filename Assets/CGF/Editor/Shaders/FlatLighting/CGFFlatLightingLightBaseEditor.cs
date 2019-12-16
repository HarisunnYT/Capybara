///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Base of editors of the behaviors FlatLighting.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Base of editors of the behaviors FlatLighting.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base del editor de los comportamientos FlatLighting.
    /// </summary>
    /// \endspanish
    public class CGFFlatLightingLightBaseEditor<T> : CGFFlatLightingBaseEditor<T> where T : Component
    {
        SerializedProperty _color;

        SerializedProperty _frontLightLevel;
        SerializedProperty _rightLightLevel;
        SerializedProperty _topLightLevel;

        SerializedProperty _frontOpacityLevel;
        SerializedProperty _rightOpacityLevel;
        SerializedProperty _topOpacityLevel;

        SerializedProperty _gradient;
        SerializedProperty _gradientTopColor;
        SerializedProperty _gradientCenter;
        SerializedProperty _gradientWidth;
        SerializedProperty _gradientRevert;
        SerializedProperty _gradientChangeDirection;
        SerializedProperty _gradientRotation;

        protected override void OnEnable()
        {
            base.OnEnable();

            _color = serializedObject.FindProperty("_color");

            _frontLightLevel = serializedObject.FindProperty("_frontLightLevel");
            _rightLightLevel = serializedObject.FindProperty("_rightLightLevel");
            _topLightLevel = serializedObject.FindProperty("_topLightLevel");

            _frontOpacityLevel = serializedObject.FindProperty("_frontOpacityLevel");
            _rightOpacityLevel = serializedObject.FindProperty("_rightOpacityLevel");
            _topOpacityLevel = serializedObject.FindProperty("_topOpacityLevel");

            _gradient = serializedObject.FindProperty("_gradient");
            _gradientTopColor = serializedObject.FindProperty("_gradientTopColor");
            _gradientCenter = serializedObject.FindProperty("_gradientCenter");
            _gradientWidth = serializedObject.FindProperty("_gradientWidth");
            _gradientRevert = serializedObject.FindProperty("_gradientRevert");
            _gradientChangeDirection = serializedObject.FindProperty("_gradientChangeDirection");
            _gradientRotation = serializedObject.FindProperty("_gradientRotation");
        }

        protected override void DrawColorsByNormals()
        {
            base.DrawColorsByNormals();

            CGFEditorUtilitiesClass.BuildColor("Color" + _colorType, "Color of the normals.", _color);

            DrawColorLightLevel();

            DrawColorOpacityLevel();

            EditorGUILayout.EndVertical();
        }

        protected virtual void DrawColorLightLevel()
        {
            CGFEditorUtilitiesClass.BuildFloatSlider("Front Light Level", "Brightness of the light of the front normals.", _frontLightLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Right Light Level", "Brightness of the light of the right normals.", _rightLightLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Top Light Level", "Brightness of the light of the top normals.", _topLightLevel, 0, 1);
        }

        protected virtual void DrawColorOpacityLevel()
        {
            GUILayout.Space(10);
            CGFEditorUtilitiesClass.BuildFloatSlider("Front Opacity Level", "Opacity of the color of the front normals.", _frontOpacityLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Right Opacity Level", "Opacity of the color of the right normals.", _rightOpacityLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Top Opacity Level", "Opacity of the color of the top normals.", _topOpacityLevel, 0, 1);
        }

        protected override void DrawColorGradient()
        {
            
            DrawGradient("", _gradient, _gradientTopColor, _gradientCenter, _gradientWidth, _gradientRevert, _gradientChangeDirection, _gradientRotation);
            
        }
    }

}