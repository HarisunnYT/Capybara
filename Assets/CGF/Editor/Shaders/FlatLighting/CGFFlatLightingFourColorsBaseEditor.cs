///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Base of editors of the behaviors FlatLightingFourColors.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Base of editors of the behaviors FlatLightingFourColors.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base del editor de los comportamientos FlatLightingFourColors.
    /// </summary>
    /// \endspanish
    public abstract class CGFFlatLightingFourColorsBaseEditor<T> : CGFFlatLightingColorBaseEditor<T> where T : Component
    {
        SerializedProperty _rimColor;

        SerializedProperty _rimTexture;
        SerializedProperty _rimTextureTiling;
        SerializedProperty _rimTextureOffset;

        SerializedProperty _rimTextureLevel;

        SerializedProperty _rimGradient;
        SerializedProperty _rimTopColorGradient;
        SerializedProperty _rimGradientCenter;
        SerializedProperty _rimGradientWidth;
        SerializedProperty _rimGradientRevert;
        SerializedProperty _rimGradientChangeDirection;
        SerializedProperty _rimGradientRotation;

        protected override void OnEnable()
        {
            base.OnEnable();

            _rimColor = serializedObject.FindProperty("_rimColor");

            _rimTexture = serializedObject.FindProperty("_rimTexture");
            _rimTextureTiling = serializedObject.FindProperty("_rimTextureTiling");
            _rimTextureOffset = serializedObject.FindProperty("_rimTextureOffset");

            _rimTextureLevel = serializedObject.FindProperty("_rimTextureLevel");

            _rimGradient = serializedObject.FindProperty("_rimGradient");
            _rimTopColorGradient = serializedObject.FindProperty("_rimTopColorGradient");
            _rimGradientCenter = serializedObject.FindProperty("_rimGradientCenter");
            _rimGradientWidth = serializedObject.FindProperty("_rimGradientWidth");
            _rimGradientRevert = serializedObject.FindProperty("_rimGradientRevert");
            _rimGradientChangeDirection = serializedObject.FindProperty("_rimGradientChangeDirection");
            _rimGradientRotation = serializedObject.FindProperty("_rimGradientRotation");

            _colorsByNormals = "4 Colors By Normals";

        }

        protected override void DrawColorsByNormals()
        {
            base.DrawColorsByNormals();

            CGFEditorUtilitiesClass.BuildColor("Rim Color" + _colorType, "Color of the rim normals.", _rimColor);

            EditorGUILayout.EndVertical();

        }

        protected override void DrawTextures()
        {
            base.DrawTextures();

            CGFEditorUtilitiesClass.BuildTexture("Rim Texture" + _colorType, "Texture of the top normals.", _rimTexture, _rimTextureTiling, _rimTextureOffset);

        }

        protected override void DrawTextureLevel()
        {
            base.DrawTextureLevel();

            CGFEditorUtilitiesClass.BuildFloatSlider("Rim Texture Level", "Level of rim texture in relation the source color.", _rimTextureLevel, 0, 1);
   
        }

        protected override void DrawColorGradient()
        {
            base.DrawColorGradient();

            GUILayout.Space(10);
            DrawGradient("Rim ", _rimGradient, _rimTopColorGradient, _rimGradientCenter, _rimGradientWidth, _rimGradientRevert, _rimGradientChangeDirection, _rimGradientRotation);

        }
    }

}