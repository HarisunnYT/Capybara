///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of CGFSimulatedLightManager.
///

using UnityEditor;
using CGF.Systems.Shaders.Managers;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Editor of CGFSimulatedLightManager.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor de CGFSimulatedLightManager.
    /// </summary>
    /// \endspanish
    [CustomEditor(typeof(CGFSimulatedLightManager))]
    [CanEditMultipleObjects]
    public class CGFSimulatedLightManagerEditor : CGFShaderManagerEditor<CGFSimulatedLightManager>
    {

        SerializedProperty _simulatedLight;

        SerializedProperty _simulatedLightRampTexture;

        SerializedProperty _simulatedLightRampTextureTiling;

        SerializedProperty _simulatedLightRampTextureOffset;

        SerializedProperty _simulatedLightLevel;

        SerializedProperty _simulatedLightPosition;

        SerializedProperty _simulatedLightDistance;

        SerializedProperty _gradientRamp;

        SerializedProperty _centerColor;

        SerializedProperty _useExternalColor;

        SerializedProperty _externalColor;

        SerializedProperty _additiveSimulatedLight;

        SerializedProperty _additiveSimulatedLightLevel;

        SerializedProperty _posterize;

        SerializedProperty _steps;

        protected override void OnEnable()
        {

            base.OnEnable();

            _simulatedLight = serializedObject.FindProperty("_simulatedLight");

            _simulatedLightRampTexture = serializedObject.FindProperty("_simulatedLightRampTexture");

            _simulatedLightRampTextureTiling = serializedObject.FindProperty("_simulatedLightRampTextureTiling");

            _simulatedLightRampTextureOffset = serializedObject.FindProperty("_simulatedLightRampTextureOffset");

            _simulatedLightLevel = serializedObject.FindProperty("_simulatedLightLevel");

            _simulatedLightPosition = serializedObject.FindProperty("_simulatedLightPosition");

            _simulatedLightDistance = serializedObject.FindProperty("_simulatedLightDistance");

            _gradientRamp = serializedObject.FindProperty("_gradientRamp");

            _centerColor = serializedObject.FindProperty("_centerColor");

            _useExternalColor = serializedObject.FindProperty("_useExternalColor");

            _externalColor = serializedObject.FindProperty("_externalColor");

            _additiveSimulatedLight = serializedObject.FindProperty("_additiveSimulatedLight");

            _additiveSimulatedLightLevel = serializedObject.FindProperty("_additiveSimulatedLightLevel");

            _posterize = serializedObject.FindProperty("_posterize");

            _steps = serializedObject.FindProperty("_steps");

        }

        private void OnSceneGUI()
        {
            CGFEditorUtilitiesExtendedClass.DrawSphereHandle(_simulatedLight, _simulatedLightPosition, _simulatedLightDistance, _centerColor, true, true, this);
        }

        protected override void DrawContent()
        {
            
            EditorGUILayout.BeginVertical();

            DrawEnableButton("Simulated Light", "Simulated Light.", _simulatedLight);

            EditorGUI.BeginDisabledGroup(_simulatedLight.boolValue == false);

            EditorGUI.BeginDisabledGroup(_gradientRamp.boolValue == true);

            CGFEditorUtilitiesClass.BuildTexture("Simulated Light Ramp Texture (RGB)", "Color ramp of the simulated light based on a texture. The top part of the texture is the center of the simulated light and the bottom part is the external part of the simulated light.", _simulatedLightRampTexture, _simulatedLightRampTextureTiling, _simulatedLightRampTextureOffset);

            EditorGUI.EndDisabledGroup();

            CGFEditorUtilitiesClass.BuildFloatSlider("Simulated Light Level", "Level of simulated light in relation the source color.", _simulatedLightLevel, 0, 1);

            CGFEditorUtilitiesClass.BuildVector3("Simulated Light Position", "World position of the simulated light.", _simulatedLightPosition);

            CGFEditorUtilitiesClass.BuildFloat("Simulated Light Distance", "Simulated light circunference diameter.", _simulatedLightDistance);

            CGFEditorUtilitiesClass.BuildBoolean("Gradient Ramp", "If enabled uses a gradient ramp between two colors instead a ramp texture.", _gradientRamp);

            EditorGUI.BeginDisabledGroup(_gradientRamp.boolValue == false);

            CGFEditorUtilitiesClass.BuildColor("Center Color (RGB)", "Color of the center of the simulated light if gradient ramp is enabled.", _centerColor);

            CGFEditorUtilitiesClass.BuildBoolean("Use External Color", "If enabled uses a color for the external part of the light instead de source color.", _useExternalColor);

            EditorGUI.BeginDisabledGroup(_useExternalColor.boolValue == false);

            CGFEditorUtilitiesClass.BuildColor("External Color (RGB)", "Color of the expernal part of the simulated light if gradient ramp is enabled.", _externalColor);

            EditorGUI.EndDisabledGroup();

            EditorGUI.EndDisabledGroup();

            CGFEditorUtilitiesClass.BuildBoolean("Additive Simulated Light", "If enabled adds the simulated light color to the source color.", _additiveSimulatedLight);

            EditorGUI.BeginDisabledGroup(_additiveSimulatedLight.boolValue == false);

            CGFEditorUtilitiesClass.BuildFloatSlider("Additive Simulated Light Level", "Level of simulated light addition in relation the source color.", _additiveSimulatedLightLevel, 0, 1);

            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(_gradientRamp.boolValue == false);

            CGFEditorUtilitiesClass.BuildBoolean("Posterize", "If enabled converts the ramp texture or the gradient ramp to multiple regions of fewer tones.", _posterize);

            EditorGUI.BeginDisabledGroup(_posterize.boolValue == false);

            CGFEditorUtilitiesClass.BuildFloatSlider("Steps", "Color steps of the gradient ramp between center color and external color.", _steps, 2, 20);

            EditorGUI.EndDisabledGroup();

            EditorGUI.EndDisabledGroup();

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndVertical();

        }

    }

}
