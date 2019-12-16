///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Base of editors of the behaviors FlatLightingSixColors.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Base of editors of the behaviors FlatLightingSixColors.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base del editor de los comportamientos FlatLightingSixColors.
    /// </summary>
    /// \endspanish
    public class CGFFlatLightingSixColorsBaseEditor<T> : CGFFlatLightingColorBaseEditor<T> where T : Component
    {

        SerializedProperty _backColor;

        SerializedProperty _backTexture;
        SerializedProperty _backTextureTiling;
        SerializedProperty _backTextureOffset;

        SerializedProperty _backTextLevel;

        SerializedProperty _backGradient;
        SerializedProperty _backTopColorGradient;
        SerializedProperty _backGradientCenter;
        SerializedProperty _backGradientWidth;
        SerializedProperty _backGradientRevert;
        SerializedProperty _backGradientChangeDirection;
        SerializedProperty _backGradientRotation;


        SerializedProperty _leftColor;

        SerializedProperty _leftTexture;
        SerializedProperty _leftTextureTiling;
        SerializedProperty _leftTextureOffset;

        SerializedProperty _leftTextLevel;

        SerializedProperty _leftGradient;
        SerializedProperty _leftTopColorGradient;
        SerializedProperty _leftGradientCenter;
        SerializedProperty _leftGradientWidth;
        SerializedProperty _leftGradientRevert;
        SerializedProperty _leftGradientChangeDirection;
        SerializedProperty _leftGradientRotation;


        SerializedProperty _bottomColor;

        SerializedProperty _bottomTexture;
        SerializedProperty _bottomTextureTiling;
        SerializedProperty _bottomTextureOffset;

        SerializedProperty _bottomTextLevel;

        SerializedProperty _bottomGradient;
        SerializedProperty _bottomTopColorGradient;
        SerializedProperty _bottomGradientCenter;
        SerializedProperty _bottomGradientWidth;
        SerializedProperty _bottomGradientRevert;
        SerializedProperty _bottomGradientChangeDirection;
        SerializedProperty _bottomGradientRotation;

        protected override void OnEnable()
        {
            base.OnEnable();

            _backColor = serializedObject.FindProperty("_backColor");

            _backTexture = serializedObject.FindProperty("_backTexture");
            _backTextureTiling = serializedObject.FindProperty("_backTextureTiling");
            _backTextureOffset = serializedObject.FindProperty("_backTextureOffset");

            _backTextLevel = serializedObject.FindProperty("_backTextLevel");

            _backGradient = serializedObject.FindProperty("_backGradient");
            _backTopColorGradient = serializedObject.FindProperty("_backTopColorGradient");
            _backGradientCenter = serializedObject.FindProperty("_backGradientCenter");
            _backGradientWidth = serializedObject.FindProperty("_backGradientWidth");
            _backGradientRevert = serializedObject.FindProperty("_backGradientRevert");
            _backGradientChangeDirection = serializedObject.FindProperty("_backGradientChangeDirection");
            _backGradientRotation = serializedObject.FindProperty("_backGradientRotation");


            _leftColor = serializedObject.FindProperty("_leftColor");

            _leftTexture = serializedObject.FindProperty("_leftTexture");
            _leftTextureTiling = serializedObject.FindProperty("_leftTextureTiling");
            _leftTextureOffset = serializedObject.FindProperty("_leftTextureOffset");

            _leftTextLevel = serializedObject.FindProperty("_leftTextLevel");

            _leftGradient = serializedObject.FindProperty("_leftGradient");
            _leftTopColorGradient = serializedObject.FindProperty("_leftTopColorGradient");
            _leftGradientCenter = serializedObject.FindProperty("_leftGradientCenter");
            _leftGradientWidth = serializedObject.FindProperty("_leftGradientWidth");
            _leftGradientRevert = serializedObject.FindProperty("_leftGradientRevert");
            _leftGradientChangeDirection = serializedObject.FindProperty("_leftGradientChangeDirection");
            _leftGradientRotation = serializedObject.FindProperty("_leftGradientRotation");



            _bottomColor = serializedObject.FindProperty("_bottomColor");

            _bottomTexture = serializedObject.FindProperty("_bottomTexture");
            _bottomTextureTiling = serializedObject.FindProperty("_bottomTextureTiling");
            _bottomTextureOffset = serializedObject.FindProperty("_bottomTextureOffset");

            _bottomTextLevel = serializedObject.FindProperty("_bottomTextLevel");

            _bottomGradient = serializedObject.FindProperty("_bottomGradient");
            _bottomTopColorGradient = serializedObject.FindProperty("_bottomTopColorGradient");
            _bottomGradientCenter = serializedObject.FindProperty("_bottomGradientCenter");
            _bottomGradientWidth = serializedObject.FindProperty("_bottomGradientWidth");
            _bottomGradientRevert = serializedObject.FindProperty("_bottomGradientRevert");
            _bottomGradientChangeDirection = serializedObject.FindProperty("_bottomGradientChangeDirection");
            _bottomGradientRotation = serializedObject.FindProperty("_bottomGradientRotation");

            _colorsByNormals = "6 Colors By Normals";
        }

        protected override void DrawColorsByNormals()
        {
            base.DrawColorsByNormals();

            CGFEditorUtilitiesClass.BuildColor("Back Color " + _colorType, "Color of the back normals.", _backColor);
            CGFEditorUtilitiesClass.BuildColor("Left Color " + _colorType, "Color of the left normals.", _leftColor);
            CGFEditorUtilitiesClass.BuildColor("Bottom Color " + _colorType, "Color of the bottom normals.", _bottomColor);

            EditorGUILayout.EndVertical();
        }

        protected override void DrawTextures()
        {
            base.DrawTextures();

            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildTexture("Back Texture " + _colorType, "Texture of the back normals.", _backTexture, _backTextureTiling, _backTextureOffset);
            CGFEditorUtilitiesClass.BuildTexture("Left Texture " + _colorType, "Texture of the left normals.", _leftTexture, _leftTextureTiling, _leftTextureOffset);
            CGFEditorUtilitiesClass.BuildTexture("Bottom Texture " + _colorType, "Texture of the bottom normals.", _bottomTexture, _bottomTextureTiling, _bottomTextureOffset);
            EditorGUILayout.EndVertical();
        }

        protected override void DrawTextureLevel()
        {
            base.DrawTextureLevel();

            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildFloatSlider("Back Texture Level", "Level of back texture in relation the source color.", _backTextLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Left Texture Level", "Level of left texture in relation the source color.", _leftTextLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Bottom Texture Level", "Level of bottom texture in relation the source color.", _bottomTextLevel, 0, 1);
            EditorGUILayout.EndVertical();
        }

        protected override void DrawColorGradient()
        {
            base.DrawColorGradient();

            GUILayout.Space(10);

            DrawGradient("Back ", _backGradient, _backTopColorGradient, _backGradientCenter, _backGradientWidth, _backGradientRevert, _backGradientChangeDirection, _backGradientRotation);

            GUILayout.Space(10);

            DrawGradient("Left ", _leftGradient, _leftTopColorGradient, _leftGradientCenter, _leftGradientWidth, _leftGradientRevert, _leftGradientChangeDirection, _leftGradientRotation);

            GUILayout.Space(10);

            DrawGradient("Bottom ", _bottomGradient, _bottomTopColorGradient, _bottomGradientCenter, _bottomGradientWidth, _bottomGradientRevert, _bottomGradientChangeDirection, _bottomGradientRotation);
            
        }
    }

}