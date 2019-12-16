///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Base of editors of the behaviors FlatLightingFourLights.
///

using UnityEditor;
using UnityEngine;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.FlatLighting
{

    /// \english
    /// <summary>
    /// Base of editors of the behaviors FlatLightingFourLights.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Base del editor de los comportamientos FlatLightingFourLights.
    /// </summary>
    /// \endspanish
    public class CGFFlatLightingFourLightsBaseEditor<T> : CGFFlatLightingLightBaseEditor<T> where T : Component
    {
        SerializedProperty _rimLightLevel;

        SerializedProperty _rimOpacityLevel;

        SerializedProperty _rimTexture;
        SerializedProperty _rimTextureTiling;
        SerializedProperty _rimTextureOffset;

        SerializedProperty _rimTextureLevel;

        protected override void OnEnable()
        {
            base.OnEnable();

            _rimLightLevel = serializedObject.FindProperty("_rimLightLevel");

            _rimOpacityLevel = serializedObject.FindProperty("_rimOpacityLevel");

            _rimTexture = serializedObject.FindProperty("_rimTexture");
            _rimTextureTiling = serializedObject.FindProperty("_rimTextureTiling");
            _rimTextureOffset = serializedObject.FindProperty("_rimTextureOffset");

            _rimTextureLevel = serializedObject.FindProperty("_rimTextureLevel");

            _colorsByNormals = "4 Lights By Normals";
        }

        protected override void DrawTextures()
        {
            base.DrawTextures();

            CGFEditorUtilitiesClass.BuildTexture("Rim Texture" + _colorType, "Texture of the rim normals.", _rimTexture, _rimTextureTiling, _rimTextureOffset);

        }

        protected override void DrawTextureLevel()
        {
            base.DrawTextureLevel();

            CGFEditorUtilitiesClass.BuildFloatSlider("Rim Texture Level", "Level of rim texture in relation the source color.", _rimTextureLevel, 0, 1);

        }

        protected override void DrawColorLightLevel()
        {
            base.DrawColorLightLevel();

            CGFEditorUtilitiesClass.BuildFloatSlider("Rim Light Level", "Brightness of the light of the rim normals.", _rimLightLevel, 0, 1);
        }

        protected override void DrawColorOpacityLevel()
        {
            base.DrawColorOpacityLevel();

            CGFEditorUtilitiesClass.BuildFloatSlider("Rim Opacity Level", "Opacity of the color of the rim normals.", _rimOpacityLevel, 0, 1);
        }

    }

}