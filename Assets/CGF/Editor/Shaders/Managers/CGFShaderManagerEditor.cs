///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 09/05/2018
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Editor of shader managers.
///

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using CGF.Systems.Shaders.Managers;
using Assets.CGF.Editor;

namespace CGF.Editor.Shaders.Managers
{

    /// \english
    /// <summary>
    /// Editor of shader managers.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Editor de los gestores de shaders.
    /// </summary>
    /// \endspanish
    public abstract class CGFShaderManagerEditor<T> : UnityEditor.Editor where T : CGFShaderManager
    {

        SerializedProperty _useAnimator;

        SerializedProperty _automatic;

        SerializedProperty _getAllMaterials;

        SerializedProperty _materials;

        int _materialsIndex;

        ReorderableList _materialsReordenable;

        protected virtual void OnEnable()
        {

            _useAnimator = serializedObject.FindProperty("_useAnimator");

            _automatic = serializedObject.FindProperty("_automatic");

            _getAllMaterials = serializedObject.FindProperty("_getAllMaterials");

            _materials = serializedObject.FindProperty("_materials");

            _materialsReordenable = new ReorderableList(serializedObject, _materials, true, true, true, true);

        }

        public override void OnInspectorGUI()
        {

            serializedObject.Update();

            CGFEditorUtilitiesClass.BuildComponentTools("http://chloroplastgames.com/cg-framework-user-manual/", serializedObject);

            CGFEditorUtilitiesClass.ManageComponentValues<T>();

            CGFEditorUtilitiesClass.BackUpComponentValues<T>(serializedObject);

            EditorGUILayout.BeginVertical();

            DrawOptions();

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();

            DrawContent();

            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();

        }

        protected virtual void DrawOptions()
        {

            CGFEditorUtilitiesClass.BuildBoolean("Use Animator", "Description", _useAnimator);

            if (GUILayout.Button("Get all materials in scene"))
            {

                CGFShaderManager myClass = serializedObject.targetObject as CGFShaderManager;

                myClass.GetAllMaterials();

            }

            if (GUILayout.Button("Clear Materials"))
            {

                CGFShaderManager myClass = serializedObject.targetObject as CGFShaderManager;

                myClass.ClearMaterials();

            }

            ReorderableList rl = CGFEditorUtilitiesClass.BuildListCustom(_materials, _materialsReordenable, "Materials", true, new int[] { 2 }, "renderer");

            CGFShaderManager manager = serializedObject.targetObject as CGFShaderManager;

            manager.GetAllProperties();

            CGFEditorUtilitiesClass.BuildBoolean("Automatic", "Enable o disable the automatic start.", _automatic);

            CGFEditorUtilitiesClass.BuildBoolean("Get All Materials", "Get all material with this property.", _getAllMaterials);

        }

        protected virtual void DrawContent()
        {

        }

        protected virtual void DrawEnableButton(string label, string description, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(new GUIContent(label, description), EditorStyles.boldLabel);

            GUILayout.FlexibleSpace();

            EditorGUILayout.PropertyField(property, GUIContent.none, GUILayout.Width(10));

            EditorGUIUtility.labelWidth = 0.1f;

            EditorGUILayout.LabelField("Enable");

            EditorGUIUtility.labelWidth = 0;

            EditorGUILayout.EndHorizontal();

        }

        protected virtual void DrawFog(string fogDescription, string colorType, string endName, SerializedProperty fog, SerializedProperty fogColor, SerializedProperty fogStartPosition, SerializedProperty fogEnd, SerializedProperty fogDensity, SerializedProperty alpha, SerializedProperty localFog)
        {

            DrawEnableButton(colorType + " Fog", fogDescription, fog);

            EditorGUI.BeginDisabledGroup(fog.boolValue == false);

            CGFEditorUtilitiesClass.BuildColor(colorType + " Fog Color" + " (RGB)", "Color of the fog.", fogColor);

            CGFEditorUtilitiesClass.BuildFloat(colorType + " Fog Start Position ", "Start point of the fog.", fogStartPosition);

            CGFEditorUtilitiesClass.BuildFloat(endName, "End point of the fog.", fogEnd);

            CGFEditorUtilitiesClass.BuildFloatSlider(colorType + " Fog Density", "Level of fog in relation the source color.", fogDensity, 0, 1);

            CGFEditorUtilitiesClass.BuildBoolean("Use Alpha", "If enabled fog doesn't affect the transparent parts of the source color.", alpha);

            CGFEditorUtilitiesClass.BuildBoolean("Local " + colorType + " Fog", "If enabled the fog is created based on the center of the world.", localFog);

            EditorGUI.EndDisabledGroup();

        }

        protected virtual void DrawFog(string fogDescription, string colorType, string endName, SerializedProperty fog, SerializedProperty fogColor, SerializedProperty fogStartPosition, SerializedProperty fogEnd, SerializedProperty fogDensity, SerializedProperty alpha, SerializedProperty worldDistanceFog, SerializedProperty worldFogPosition)
        {

            DrawEnableButton("Distance Fog", fogDescription, fog);

            EditorGUI.BeginDisabledGroup(fog.boolValue == false);

            CGFEditorUtilitiesClass.BuildColor("Distance Fog Color" + " (RGB)", "Color of the fog.", fogColor);

            CGFEditorUtilitiesClass.BuildFloat("Distance Fog Start Position", "Start point of the fog.", fogStartPosition);

            CGFEditorUtilitiesClass.BuildFloat(endName, "Length of the fog.", fogEnd);

            CGFEditorUtilitiesClass.BuildFloatSlider("Distance Fog Density", "Level of fog in relation the source color.", fogDensity, 0, 1);

            CGFEditorUtilitiesClass.BuildBoolean("Use Alpha", "If enabled fog doesn't affect the transparent parts of the source color.", alpha);

            CGFEditorUtilitiesClass.BuildBoolean("World Distance Fog", "If enabled the fog is created based on the center of the world.", worldDistanceFog);

            EditorGUI.BeginDisabledGroup(worldDistanceFog.boolValue == false);

            CGFEditorUtilitiesClass.BuildVector3("World Distance Fog Position", "World position of the distance fog.", worldFogPosition);

            EditorGUI.EndDisabledGroup();

            EditorGUI.EndDisabledGroup();

        }

    }

}