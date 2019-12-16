///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Base of editors of the behaviors FlatLightingColor.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Base of editors of the behaviors FlatLightingColor.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base del editor de los comportamientos FlatLightingColor.
    /// </summary>
    /// \endspanish
    public class CGFFlatLightingColorBaseEditor<T> : CGFFlatLightingBaseEditor<T> where T : Component
    {
        SerializedProperty _frontColor;
        SerializedProperty _rightColor;
        SerializedProperty _topColor;

        SerializedProperty _frontGradient;
        SerializedProperty _frontTopColorGradient;
        SerializedProperty _frontGradientCenter;
        SerializedProperty _frontGradientWidth;
        SerializedProperty _frontGradientRevert;
        SerializedProperty _frontGradientChangeDirection;
        SerializedProperty _frontGradientRotation;

        SerializedProperty _rightGradient;
        SerializedProperty _rightTopColorGradient;
        SerializedProperty _rightGradientCenter;
        SerializedProperty _rightGradientWidth;
        SerializedProperty _rightGradientRevert;
        SerializedProperty _rightGradientChangeDirection;
        SerializedProperty _rightGradientRotation;

        SerializedProperty _topGradient;
        SerializedProperty _topTopColorGradient;
        SerializedProperty _topGradientCenter;
        SerializedProperty _topGradientWidth;
        SerializedProperty _topGradientRevert;
        SerializedProperty _topGradientChangeDirection;
        SerializedProperty _topGradientRotation;

        SerializedProperty _opacityLevel;

        protected override void OnEnable()
        {
            base.OnEnable();

            _frontColor = serializedObject.FindProperty("_frontColor");
            _rightColor = serializedObject.FindProperty("_rightColor");
            _topColor = serializedObject.FindProperty("_topColor");

            _frontGradient = serializedObject.FindProperty("_frontGradient");
            _frontTopColorGradient = serializedObject.FindProperty("_frontTopColorGradient");
            _frontGradientCenter = serializedObject.FindProperty("_frontGradientCenter");
            _frontGradientWidth = serializedObject.FindProperty("_frontGradientWidth");
            _frontGradientRevert = serializedObject.FindProperty("_frontGradientRevert");
            _frontGradientChangeDirection = serializedObject.FindProperty("_frontGradientChangeDirection");
            _frontGradientRotation = serializedObject.FindProperty("_frontGradientRotation");

            _rightGradient = serializedObject.FindProperty("_rightGradient");
            _rightTopColorGradient = serializedObject.FindProperty("_rightTopColorGradient");
            _rightGradientCenter = serializedObject.FindProperty("_rightGradientCenter");
            _rightGradientWidth = serializedObject.FindProperty("_rightGradientWidth");
            _rightGradientRevert = serializedObject.FindProperty("_rightGradientRevert");
            _rightGradientChangeDirection = serializedObject.FindProperty("_rightGradientChangeDirection");
            _rightGradientRotation = serializedObject.FindProperty("_rightGradientRotation");

            _topGradient = serializedObject.FindProperty("_topGradient");
            _topTopColorGradient = serializedObject.FindProperty("_topTopColorGradient");
            _topGradientCenter = serializedObject.FindProperty("_topGradientCenter");
            _topGradientWidth = serializedObject.FindProperty("_topGradientWidth");
            _topGradientRevert = serializedObject.FindProperty("_topGradientRevert");
            _topGradientChangeDirection = serializedObject.FindProperty("_topGradientChangeDirection");
            _topGradientRotation = serializedObject.FindProperty("_topGradientRotation");

            _opacityLevel = serializedObject.FindProperty("_opacityLevel");
        }

        protected override void DrawColorsByNormals()
        {
            base.DrawColorsByNormals();

            CGFEditorUtilitiesClass.BuildColor("Front Color" + _colorType, "Color of the front normals.", _frontColor);
            CGFEditorUtilitiesClass.BuildColor("Right Color" + _colorType, "Color of the right normals.", _rightColor);
            CGFEditorUtilitiesClass.BuildColor("Top Color" + _colorType, "Color of the top normals.", _topColor);
        }

        protected override void DrawColorGradient()
        {
            
            DrawGradient("Front ", _frontGradient, _frontTopColorGradient, _frontGradientCenter, _frontGradientWidth, _frontGradientRevert, _frontGradientChangeDirection, _frontGradientRotation);
            GUILayout.Space(10);
            DrawGradient("Right ", _rightGradient, _rightTopColorGradient, _rightGradientCenter, _rightGradientWidth, _rightGradientRevert, _rightGradientChangeDirection, _rightGradientRotation);
            GUILayout.Space(10);
            DrawGradient("Top ", _topGradient, _topTopColorGradient, _topGradientCenter, _topGradientWidth, _topGradientRevert, _topGradientChangeDirection, _topGradientRotation);
            
        }

        protected override void DrawOpacityLevel()
        {
            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildFloatSlider("Opacity Level", "Overall opacity value.", _opacityLevel, 0, 1);
            EditorGUILayout.EndVertical();
        }
    }

}