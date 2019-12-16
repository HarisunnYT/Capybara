///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Base of editors of the behaviors FlatLightingSixLights.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Base of editors of the behaviors 
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base del editor de los comportamientos FlatLightingSixLights.
    /// </summary>
    /// \endspanish
    public class CGFFlatLightingSixLightsBaseEditor<T> : CGFFlatLightingLightBaseEditor<T> where T : Component
    {

        SerializedProperty _leftTexture;
        SerializedProperty _leftTextureTiling;
        SerializedProperty _leftTextureOffset;

        SerializedProperty _backTexture;
        SerializedProperty _backTextureTiling;
        SerializedProperty _backTextureOffset;

        SerializedProperty _bottomTexture;
        SerializedProperty _bottomTextureTiling;
        SerializedProperty _bottomTextureOffset;

        SerializedProperty _leftTextLevel;
        SerializedProperty _backTextLevel;
        SerializedProperty _bottomTextLevel;

        SerializedProperty _backLightLevel;
        SerializedProperty _leftLightLevel;
        SerializedProperty _bottomLightLevel;

        SerializedProperty _backOpacityLevel;
        SerializedProperty _leftOpacityLevel;
        SerializedProperty _bottomOpacityLevel;

        protected override void OnEnable()
        {
            base.OnEnable();

            _leftTexture = serializedObject.FindProperty("_leftTexture");
            _leftTextureTiling = serializedObject.FindProperty("_leftTextureTiling");
            _leftTextureOffset = serializedObject.FindProperty("_leftTextureOffset");

            _backTexture = serializedObject.FindProperty("_backTexture");
            _backTextureTiling = serializedObject.FindProperty("_backTextureTiling");
            _backTextureOffset = serializedObject.FindProperty("_backTextureOffset");

            _bottomTexture = serializedObject.FindProperty("_bottomTexture");
            _bottomTextureTiling = serializedObject.FindProperty("_bottomTextureTiling");
            _bottomTextureOffset = serializedObject.FindProperty("_bottomTextureOffset");

            _leftTextLevel = serializedObject.FindProperty("_leftTextLevel");
            _backTextLevel = serializedObject.FindProperty("_backTextLevel");
            _bottomTextLevel = serializedObject.FindProperty("_bottomTextLevel");


            _backLightLevel = serializedObject.FindProperty("_backLightLevel");
            _leftLightLevel = serializedObject.FindProperty("_leftLightLevel");
            _bottomLightLevel = serializedObject.FindProperty("_bottomLightLevel");

            _backOpacityLevel = serializedObject.FindProperty("_backOpacityLevel");
            _leftOpacityLevel = serializedObject.FindProperty("_leftOpacityLevel");
            _bottomOpacityLevel = serializedObject.FindProperty("_bottomOpacityLevel");

            _colorsByNormals = "6 Colors By Normals";
        }

        protected override void DrawTextures()
        {
            
            base.DrawTextures();
            EditorGUILayout.BeginVertical();
            CGFEditorUtilitiesClass.BuildTexture("Back Texture" + _colorType, "Texture of the back normals.", _backTexture, _backTextureTiling, _backTextureOffset);
            CGFEditorUtilitiesClass.BuildTexture("Left Texture" + _colorType, "Texture of the left normals.", _leftTexture, _leftTextureTiling, _leftTextureOffset);
            CGFEditorUtilitiesClass.BuildTexture("Bottom Texture" + _colorType, "Texture of the bottom normals.", _bottomTexture, _bottomTextureTiling, _bottomTextureOffset);
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
        protected override void DrawColorLightLevel()
        {
            base.DrawColorLightLevel();

            CGFEditorUtilitiesClass.BuildFloatSlider("Back Light Level", "Brightness of the light of the back normals.", _backLightLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Left Light Level", "Brightness of the light of the left normals.", _leftLightLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Bottom Light Level", "Brightness of the light of the bottom normals.", _bottomLightLevel, 0, 1);
        }

        protected override void DrawColorOpacityLevel()
        {
            base.DrawColorOpacityLevel();

            CGFEditorUtilitiesClass.BuildFloatSlider("Back Opacity Level", "Opacity of the color of the back normals.", _backOpacityLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Left Opacity Level", "Opacity of the color of the left normals.", _leftOpacityLevel, 0, 1);
            CGFEditorUtilitiesClass.BuildFloatSlider("Bottom Opacity Level", "Opacity of the color of the bottom normals.", _bottomOpacityLevel, 0, 1);
        }
    }

}