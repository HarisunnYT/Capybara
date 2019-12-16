///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 21/12/2016
/// Author: Chloroplast Games
/// Web: http://www.chloroplastgames.com
/// Programmaers: Adan Baró Balboa, Miguel Reyes, Steven Mejía, David Cuenca
/// Description: Class with a utility and functionality set for the customize and build fast the inspector of scripts.
///


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.CGF.Editor
{
    /// \english
    /// <summary>
    /// Class with a utility and functionality set for the customize and build fast the inspector of scripts.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Clase con un conjunto de utilidades y funcionalidades para personalizar y construir rápido el inspector de los scripts.
    /// </summary>
    /// \endspanish
    [ExecuteInEditMode]
    public static class CGFEditorUtilitiesClass
    {
        #region Public Variables

        /// \english
        /// <summary>
        /// Available Backup structures.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Estructuras de Backup disponibles.
        /// </summary>
        public static List<CGFComponentBackup> backupList = new List<CGFComponentBackup>();

        /// \english
        /// <summary>
        /// Rendering modes.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de renderizado.
        /// </summary>
        /// \endspanish
        public enum RenderMode
        {

            Opaque,

            Transparent,

            Cutout,

            Background

        }

        /// \english
        /// <summary>
        /// Selected rendering mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de renderizado seleccionado.
        /// </summary>
        /// \endspanish
        public static RenderMode renderMode;

        /// \english
        /// <summary>
        /// Rendering mode names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de renderizado.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] renderModeNames = GetEnumNames(renderMode);

        /// \english
        /// <summary>
        /// Blending modes.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modos de fusión.
        /// </summary>
        /// \endspanish
        public enum BlendMode
        {

            Normal,

            Darken,

            Multiply,

            ColorBurn,

            LinearBurn,

            DarkerColor,

            Lighten,

            Screen,

            ColorDodge,

            LinearDodgeOrAdditive,

            LighterColor,

            Overlay,

            SoftLight,

            HardLight,

            VividLight,

            LinearLight,

            PinLight,

            HardMix,

            Difference,

            Exclusion,

            Subtract,

            Divide,

            Hue,

            Saturation,

            Color,

            Luminosity

        }

        /// \english
        /// <summary>
        /// Selected blending mode.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Modo de fusión seleccionado.
        /// </summary>
        /// \endspanish
        public static BlendMode blendMode;

        /// \english
        /// <summary>
        /// Blending mode names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los modos de fusión.
        /// </summary>
        /// \endspanish
        public static readonly GUIContent[] blendModeNames = GetEnumNames(blendMode);

        #endregion


        #region Private Variables

        /// \english
        /// <summary>
        /// Normal status texture for the Add button in the Reorderable List. Unity Personal Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado normal del botón de añadir un elemento en una lista. Versión Unity Personal.
        /// </summary>
        /// \endspanish
        private static GUIContent addButton = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_addelementlist_normal_personal.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Pressed status texture for the Add button in the Reorderable List. Unity Personal Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado presionado del botón de añadir un elemento en una lista. Versión Unity Personal.
        /// </summary>
        /// \endspanish
        private static GUIContent addPressedButton = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_addelementlist_pressed_personal.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Normal status texture for the Add button in the Reorderable List. Unity Professional Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado normal del botón de añadir un elemento en una lista. Versión Unity Professional.
        /// </summary>
        /// \endspanish
        private static GUIContent addButtonProfessional = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_addelementlist_normal_professional.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Pressed status texture for the Add button in the Reorderable List. Unity Professional Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado presionado del botón de añadir un elemento en una lista. Versión Unity Professional.
        /// </summary>
        /// \endspanish
        private static GUIContent addPressedButtonProfessional = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_addelementlist_pressed_professional.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Normal status texture for the Delete button in the Reorderable List. Unity Personal Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado normal del botón de eliminar un elemento en una lista. Versión Unity Personal.
        /// </summary>
        /// \endspanish
        private static GUIContent deleteButton = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_removeelementlist_normal_personal.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Pressed status texture for the Delete button in the Reorderable List. Unity Personal Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado presionado del botón de eliminar un elemento en una lista. Versión Unity Personal.
        /// </summary>
        /// \endspanish
        private static GUIContent deletePressedButton = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_removeelementlist_pressed_personal.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Normal status texture for the Delete button in the Reorderable List. Unity Professional Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado normal del botón de eliminar un elemento en una lista. Versión Unity Professional.
        /// </summary>
        /// \endspanish
        private static GUIContent deleteButtonProfessional = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_removeelementlist_normal_professional.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Pressed status texture for the Delete button in the Reorderable List. Unity Professional Edition.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Textura del estado presionado del botón de eliminar un elemento en una lista. Versión Unity Professional.
        /// </summary>
        /// \endspanish
        private static GUIContent deletePressedButtonProfessional = new GUIContent((Texture2D)AssetDatabase.LoadAssetAtPath("Assets/CGF/Editor default resources/Graphics/button_removeelementlist_pressed_professional.png", typeof(Texture2D)));

        /// \english
        /// <summary>
        /// Copied Component.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Componente copiado.
        /// </summary>
        /// \endspanish
        private static string copiedComponent;

        /// \english
        /// <summary>
        /// Unity Editor Skin.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Skin del Editor de Unity.
        /// </summary>
        /// \endspanish
        private static bool isPro = EditorGUIUtility.isProSkin;

        /// \english
        /// <summary>
        /// Backup List index.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Índice de la lista de backups disponibles.
        /// </summary>
        /// \endspanish
        private static int backupPopup;

        /// \english
        /// <summary>
        /// Axes List Update.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Actualización de la lista de Axes.
        /// </summary>
        /// \endspanish
        private static List<string> axes = new List<string>();

        #endregion


        #region Utility Methods


        #region Utilities

        /// \english
        /// <summary>
        /// Get the names of all elements of a enumeration.
        /// </summary>
        /// <param name="enumeration">Enumeration with the names.</param>
        /// <returns>Names of the elements of the enumeration.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Obtiene los nombres de los elementos de una enumeración.
        /// </summary>
        /// <param name="enumeration">Enumeración con los nombres.</param>
        /// <returns>Nombres de los elementos de la enumeración.</returns>
        /// \endspanish 
        private static GUIContent[] GetEnumNames(Enum enumeration)
        {

            string[] names = Enum.GetNames(enumeration.GetType());

            List<GUIContent> content = new List<GUIContent>(); ;

            for (int i = 0; i < names.Length; i++)
            {

                content.Add(new GUIContent(names[i]));

            }

            return content.ToArray();
        }

        /// \english
        /// <summary>
        /// Popup builder from a list.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="property">Properties to build.</param>
        /// <param name="elements">Popup elements.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una lista.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="description">Descripción del parámetro.</param>
        /// <param name="property">Propiedas a construir.</param>
        /// <param name="elements">Elementos del popup.</param>
        /// \endspanish 
        public static void BuildEnumListPopUpWindow(string name, string description, SerializedProperty property, List<string> elements)
        {

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var windowRect = EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel(BuildGUIContent(name, description));

            if (GUILayout.Button(property.stringValue, buttonStyle))
            {
                CGFCustomEnumListPopupWindow window = CGFCustomEnumListPopupWindow.CreateInstance<CGFCustomEnumListPopupWindow>();

                window.Initialization(property, elements);

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = windowRect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Popup builder from a enumeration.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="rect">Drawing position.</param>
        /// <param name="property">Properties of the enumeration to build.</param>
        /// <param name="elements">Popup elements.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una enumeración.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="description">Descripción del parámetro.</param>
        /// <param name="rect">Posición a dibujar.</param>
        /// <param name="property">Propiedades de la enumeración a construir.</param>
        /// <param name="elements">Elementos del popup.</param>
        /// \endspanish 
        public static void BuildEnumListPopUpWindow(string name, string description, Rect rect, SerializedProperty property, List<string> elements)
        {

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var lastRect = EditorGUI.PrefixLabel(rect, BuildGUIContent(name, description));

            if (GUI.Button(lastRect, property.stringValue, buttonStyle))
            {
                CGFCustomEnumListPopupWindow window = CGFCustomEnumListPopupWindow.CreateInstance<CGFCustomEnumListPopupWindow>();

                window.Initialization(property, elements);

                var windowRect = lastRect;

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = rect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

        }

        /// \english
        /// <summary>
        /// Popup builder from a enumeration.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="property">Properties of the enumeration to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una enumeración.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="description">Descripción del parámetro.</param>
        /// <param name="property">Propiedas del enumerador a construir.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, SerializedProperty property)
        {
            
            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var windowRect = EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel(BuildGUIContent(name, description));            

            string[] names = property.enumNames;

            int indexName = property.enumValueIndex;

            string strinValue = names[indexName];
            
            if (GUILayout.Button(new GUIContent(strinValue), buttonStyle))
            {
                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();                

                window.Initialization(property, CustomEnumPopType.Alphabet);

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height += 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }           

            EditorGUILayout.EndHorizontal();            

        }

        /// \english
        /// <summary>
        /// Popup builder from a enumeration.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="rect">Drawing position.</param>
        /// <param name="property">Properties of the enumeration to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una enumeración.
        /// </summary>
        /// <param name="name">Nombre de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="rect">Posición de dibujado.</param>
        /// <param name="property">Propiedades de la enumeración a construir.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, Rect rect, SerializedProperty property)
        {

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var lastRect = EditorGUI.PrefixLabel(rect, BuildGUIContent(name, description));

            string[] names = property.enumNames;

            int indexName = property.enumValueIndex;

            string strinValue = names[indexName];

            if (GUI.Button(lastRect, new GUIContent(strinValue), buttonStyle))
            {

                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();

                window.Initialization(property, CustomEnumPopType.Alphabet);

                var windowRect = lastRect;

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = rect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

        }

        /// \english
        /// <summary>
        /// Popup builder from a object enumeration.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="description">Property description.</param>
        /// <param name="obj">Enumeration object.</param>
        /// <param name="action">Action to be executed when exists a change in the selection.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con un objeto enumeración.
        /// </summary>
        /// <param name="name">Nombre de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="obj">Objeto enumeración.</param>
        /// <param name="action">Acción a realizar cuando hay un cambio de selección.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, object obj, Action<object> action)
        {

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var windowRect = EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel(BuildGUIContent(name, description));

            if (GUILayout.Button(obj.ToString(), buttonStyle))
            {

                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();

                window.Initialization(obj, CustomEnumPopType.Alphabet, action);

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height += 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));
            }

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Popup builder from a object enumeration.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="description">Property description.</param>
        /// <param name="rect">Drawing position</param>
        /// <param name="obj">Enumeration object.</param>
        /// <param name="action">Action to be executed when exists a change in the selection.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con un objeto enumeración.
        /// </summary>
        /// <param name="name">Nombre de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="rect">Posición de dibujado.</param>
        /// <param name="obj">Objeto enumeración.</param>
        /// <param name="action">Acción a realizar cuando hay un cambio de selección.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, Rect rect, object obj, Action<object> action)
        {

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var lastRect = EditorGUI.PrefixLabel(rect, BuildGUIContent(name, description));

            if (GUI.Button(lastRect, obj.ToString(), buttonStyle))
            {

                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();

                window.Initialization(obj, CustomEnumPopType.Alphabet, action);

                var windowRect = lastRect;

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = rect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

        }

        /// \english
        /// <summary>
        /// Popup builder from a list.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="property">Properties to build.</param>
        /// <param name="elements">Popup elements.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una lista.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="description">Descripción del parámetro.</param>
        /// <param name="property">Propiedas a construir.</param>
        /// <param name="elements">Elementos del popup.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish 
        public static void BuildEnumListPopUpWindow(string name, string description, SerializedProperty property, List<string> elements, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var windowRect = EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel(BuildGUIContent(name, description));

            if (GUILayout.Button(property.stringValue, buttonStyle))
            {
                CGFCustomEnumListPopupWindow window = CGFCustomEnumListPopupWindow.CreateInstance<CGFCustomEnumListPopupWindow>();

                window.Initialization(property, elements);

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = windowRect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Popup builder from a enumeration.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="rect">Drawing position.</param>
        /// <param name="property">Properties of the enumeration to build.</param>
        /// <param name="elements">Popup elements.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una enumeración.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="description">Descripción del parámetro.</param>
        /// <param name="rect">Posición a dibujar.</param>
        /// <param name="property">Propiedades de la enumeración a construir.</param>
        /// <param name="elements">Elementos del popup.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish 
        public static void BuildEnumListPopUpWindow(string name, string description, Rect rect, SerializedProperty property, List<string> elements, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var lastRect = EditorGUI.PrefixLabel(rect, BuildGUIContent(name, description));

            if (GUI.Button(lastRect, property.stringValue, buttonStyle))
            {
                CGFCustomEnumListPopupWindow window = CGFCustomEnumListPopupWindow.CreateInstance<CGFCustomEnumListPopupWindow>();

                window.Initialization(property, elements);

                var windowRect = lastRect;

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = rect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

        }

        /// \english
        /// <summary>
        /// Popup builder from a enumeration.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="property">Properties of the enumeration to build.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una enumeración.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="description">Descripción del parámetro.</param>
        /// <param name="property">Propiedas del enumerador a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var windowRect = EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel(BuildGUIContent(name, description));

            string[] names = property.enumNames;

            int indexName = property.enumValueIndex;

            string strinValue = names[indexName];

            if (GUILayout.Button(new GUIContent(strinValue), buttonStyle))
            {
                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();

                window.Initialization(property, CustomEnumPopType.Alphabet);

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height += 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Popup builder from a enumeration.
        /// </summary>
        /// <param name="name">Name description.</param>
        /// <param name="description">Property description.</param>
        /// <param name="rect">Drawing position.</param>
        /// <param name="property">Properties of the enumeration to build.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con una enumeración.
        /// </summary>
        /// <param name="name">Nombre de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="rect">Posición de dibujado.</param>
        /// <param name="property">Propiedades de la enumeración a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, Rect rect, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var lastRect = EditorGUI.PrefixLabel(rect, BuildGUIContent(name, description));

            string[] names = property.enumNames;

            int indexName = property.enumValueIndex;

            string strinValue = names[indexName];

            if (GUI.Button(lastRect, new GUIContent(strinValue), buttonStyle))
            {

                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();

                window.Initialization(property, CustomEnumPopType.Alphabet);

                var windowRect = lastRect;

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = rect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

        }

        /// \english
        /// <summary>
        /// Popup builder from a object enumeration.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="description">Property description.</param>
        /// <param name="obj">Enumeration object.</param>
        /// <param name="action">Action to be executed when exists a change in the selection.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con un objeto enumeración.
        /// </summary>
        /// <param name="name">Nombre de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="obj">Objeto enumeración.</param>
        /// <param name="action">Acción a realizar cuando hay un cambio de selección.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, object obj, Action<object> action, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var windowRect = EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PrefixLabel(BuildGUIContent(name, description));

            if (GUILayout.Button(obj.ToString(), buttonStyle))
            {

                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();

                window.Initialization(obj, CustomEnumPopType.Alphabet, action);

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height += 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));
            }

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Popup builder from a object enumeration.
        /// </summary>
        /// <param name="name">Property name.</param>
        /// <param name="description">Property description.</param>
        /// <param name="rect">Drawing position</param>
        /// <param name="obj">Enumeration object.</param>
        /// <param name="action">Action to be executed when exists a change in the selection.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un popup con un objeto enumeración.
        /// </summary>
        /// <param name="name">Nombre de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="rect">Posición de dibujado.</param>
        /// <param name="obj">Objeto enumeración.</param>
        /// <param name="action">Acción a realizar cuando hay un cambio de selección.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish 
        public static void BuildEnumPopUpWindow(string name, string description, Rect rect, object obj, Action<object> action, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            GUIStyle buttonStyle = new GUIStyle(EditorStyles.popup);

            buttonStyle.padding.top = 2;

            buttonStyle.padding.bottom = 2;

            var lastRect = EditorGUI.PrefixLabel(rect, BuildGUIContent(name, description));

            if (GUI.Button(lastRect, obj.ToString(), buttonStyle))
            {

                CGFCustomEnumPopupWindow window = ScriptableObject.CreateInstance<CGFCustomEnumPopupWindow>();

                window.Initialization(obj, CustomEnumPopType.Alphabet, action);

                var windowRect = lastRect;

                windowRect.position = GUIUtility.GUIToScreenPoint(windowRect.position);

                windowRect.height = rect.height + 1;

                window.ShowAsDropDown(windowRect, new Vector2(windowRect.width, 400));

            }

        }
        
        /// \english
        /// <summary>
        /// Gets the type a serialized property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Recoge el tipo de una propiedad serializable.
        /// </summary>
        /// <param name="property">Propiedad a recoger el tipo.</param>
        /// <returns>El tipo de una propiedad serializable.</returns>
        /// \endspanish 
        public static System.Type GetType(SerializedProperty property)
        {

            System.Type parentType = property.serializedObject.targetObject.GetType();

            System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);

            return fi.FieldType;

        }

        /// \english
        /// <summary>
        /// Parameter name and description.
        /// </summary>
        /// <param name="name"> Parameter name.</param>
        /// <param name="description"> Parameter description.</param>
        /// <return> GUIContent</return>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombre de un parámetro y su descripción.
        /// </summary>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="description">Descripción del parámetro.</param>
        /// <return> GUIContent</return>
        /// \endspanish
        public static GUIContent BuildGUIContent(string name, string description)
        {

            GUIContent currentContent = new GUIContent(name, description);

            return currentContent;

        }

        /// \english
        /// <summary>
        /// Documentation link and Component deleter tools builder.
        /// </summary>
        /// <param name="documentationURL"> Documentation URL.</param>
        /// <param name="serializedObject"> GameObject to which it is attached.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de las herramientas relacionadas con el componente.
        /// </summary>
        /// <param name="documentationURL">Dirección de la pagina a la que redireccionar al jugador.</param>
        /// <param name="serializedObject"> Gameobject que contiene el script.</param>
        /// \endspanish
        public static void BuildComponentTools(string documentationURL, SerializedObject serializedObject)
        {
            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Documentation", EditorStyles.miniLabel))
            {
                Application.OpenURL(documentationURL);
            }

            if (GUILayout.Button("Remove", EditorStyles.miniLabel))
            {
                var scripts = serializedObject.targetObjects;

                for (int i = scripts.Length - 1 ; i >= 0 ; i--)
                {
                    GameObject.DestroyImmediate(scripts[i]);
                }
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }

        /// \english
        /// <summary>
        /// List of components required from the target Script to work correctly.
        /// </summary>
        /// <param name="components"> Necessary components.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una lista de componentes necesarios para que el Script objetivo funcione correctamente.
        /// </summary>
        /// <param name="components"> Componentes necesarios para que es Script objetivo funcione correctamente.</param>
        /// \endspanish
        public static void BuildRequiredComponents(params Type[] components)
        {

            List<Type> necessaryComponents = components.ToList();

            List<Type> existingComponents = new List<Type>();

            List<string> textComponents = new List<string>();

            foreach (Component component in Selection.activeGameObject.GetComponents<Component>())
            {

                existingComponents.Add(component.GetType());
            }

            foreach (Type property in components)
            {
                if (existingComponents.Contains(property))
                {

                    necessaryComponents.Remove(property);
                }
                else
                {

                    textComponents.Add(property.Name);
                }
            }

            string componentsNeededText = String.Join(System.Environment.NewLine, textComponents.ToArray());

            if (necessaryComponents.Count > 0)
            {
                GUILayout.BeginHorizontal();

                EditorGUILayout.HelpBox(System.Environment.NewLine + "Required Components :" + System.Environment.NewLine + componentsNeededText + System.Environment.NewLine, MessageType.Warning, true);

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Add Components"))
                {
                    foreach (Type component in necessaryComponents)
                    {
                        if (!existingComponents.Contains(component))
                        {
                            Selection.activeGameObject.AddComponent(component);
                        }
                    }
                }

                GUILayout.FlexibleSpace();

                GUILayout.EndHorizontal();

                EditorGUILayout.Space();
            }
        }

        /// \english
        /// <summary>
        /// Enumeration of the available scenes in the project BuildSettings.
        /// </summary>
        /// <param name="enumerationName"> Name of the scene enumaration.</param>
        /// <param name="property"> String scene representation property to modify.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las escenas del proyecto incluidas en el BuildSettings.
        /// </summary>
        /// <param name="enumerationName"> Nombre de la propiedad.</param>
        /// <param name="property"> Propiedad de tipo string a modificar.</param>
        /// \endspanish
        public static void BuildSceneList(string enumerationName, SerializedProperty property)
        {
            List<string> _sceneNames = new List<string>();

            _sceneNames.Add("None");

            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(EditorBuildSettings.scenes[i].path);

                if (obj != null)
                {
                    _sceneNames.Add(obj.name);
                }
            }

            string[] sceneNames;

            sceneNames = _sceneNames.ToArray();

            int index = Array.IndexOf(sceneNames, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, sceneNames);

            if (GUI.changed)
            {
                if (index < 0 || index > sceneNames.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = sceneNames[index];
                }

                GUI.changed = false;
            }
        }

        /// \english
        /// <summary>
        /// Enumeration of the available scenes in the project BuildSettings with locking.
        /// </summary>
        /// <param name="enumerationName"> Name of the scene enumaration.</param>
        /// <param name="property"> String scene representation property to modify.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las escenas del proyecto incluidas en el BuildSettings con opción a bloqueo.
        /// </summary>
        /// <param name="enumerationName"> Nombre de la propiedad.</param>
        /// <param name="property"> Propiedad de tipo string a modificar.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildSceneList(string enumerationName, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            List<string> _sceneNames = new List<string>();

            _sceneNames.Add("None");

            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(EditorBuildSettings.scenes[i].path);

                if (obj != null)
                {
                    _sceneNames.Add(obj.name);
                }
            }

            string[] sceneNames;

            sceneNames = _sceneNames.ToArray();

            int index = Array.IndexOf(sceneNames, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, sceneNames);

            if (GUI.changed)
            {
                if (index < 0 || index > sceneNames.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = sceneNames[index];
                }

                GUI.changed = false;
            }

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Enumeration of the available scenes in the project BuildSettings with locking.
        /// </summary>
        /// <param name="enumerationName"> Name of the scene enumaration.</param>
        /// <param name="property"> String scene representation property to modify.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las escenas del proyecto incluidas en el BuildSettings con opción a bloqueo.
        /// </summary>
        /// <param name="enumerationName"> Nombre de la propiedad.</param>
        /// <param name="property"> Propiedad de tipo string a modificar.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildSceneList(string enumerationName, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }

            else
            {

                GUI.enabled = false;

            }

            List<string> _sceneNames = new List<string>();

            _sceneNames.Add("None");

            for (int i = 0; i < EditorBuildSettings.scenes.Length; ++i)
            {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(EditorBuildSettings.scenes[i].path);

                if (obj != null)
                {
                    _sceneNames.Add(obj.name);
                }
            }

            string[] sceneNames;

            sceneNames = _sceneNames.ToArray();

            int index = Array.IndexOf(sceneNames, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, sceneNames);

            if (GUI.changed)
            {
                if (index < 0 || index > sceneNames.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = sceneNames[index];
                }

                GUI.changed = false;
            }

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Enumeration of the available tags in the project Tags&Layers.
        /// </summary>
        /// <param name="enumerationName"> Tag eumeration name.</param>
        /// <param name="property"> String tag representation property to modify.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las tags del proyecto incluidas en Tags&Layers.
        /// </summary>
        /// <param name="propertyName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo string a modificar.</param>
        /// \endspanish
        public static void BuildTagList(string propertyName, SerializedProperty property)
        {

            property.stringValue = EditorGUILayout.TagField(propertyName, property.stringValue);

        }

        /// \english
        /// <summary>
        /// Enumeration of the available tags in the project Tags&Layers with locking.
        /// </summary>
        /// <param name="enumerationName"> Tag eumeration name.</param>
        /// <param name="property"> String tag representation property to modify.</param>
        /// <param name="enumLocker"></param>
        /// <param name="enumValues"></param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las tags del proyecto incluidas en Tags&Layers.
        /// </summary>
        /// <param name="propertyName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo string a modificar.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildTagList(string propertyName, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            property.stringValue = EditorGUILayout.TagField(propertyName, property.stringValue);

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Enumeration of the available tags in the project Tags&Layers with locking.
        /// </summary>
        /// <param name="enumerationName"> Tag eumeration name.</param>
        /// <param name="property"> String tag representation property to modify.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las tags del proyecto incluidas en Tags&Layers.
        /// </summary>
        /// <param name="propertyName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo string a modificar.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildTagList(string propertyName, SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            property.stringValue = EditorGUILayout.TagField(propertyName, property.stringValue);

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Enumeration of the available layers in the project Tags&Layers.
        /// </summary>
        /// <param name="propertyName"> Layer eumeration name.</param>
        /// <param name="property"> Int layer representation property to modify.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las Layers del proyecto incluidas en el Tags&Layers.
        /// </summary>
        /// <param name="propertyName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo int a modificar.</param>
        /// \endspanish
        public static void BuildLayerList(string propertyName, SerializedProperty property)
        {

            property.intValue = EditorGUILayout.LayerField(propertyName, property.intValue);
        }

        /// \english
        /// <summary>
        /// Enumeration of the available layers in the project Tags&Layers with locking.
        /// </summary>
        /// <param name="propertyName"> Layer eumeration name.</param>
        /// <param name="property"> Int layer representation property to modify.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las Layers del proyecto incluidas en el Tags&Layers.
        /// </summary>
        /// <param name="propertyName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo int a modificar.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildLayerList(string propertyName, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            property.intValue = EditorGUILayout.LayerField(propertyName, property.intValue);

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Enumeration of the available layers in the project Tags&Layers with locking.
        /// </summary>
        /// <param name="propertyName"> Layer eumeration name.</param>
        /// <param name="property"> Int layer representation property to modify.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las Layers del proyecto incluidas en el Tags&Layers.
        /// </summary>
        /// <param name="propertyName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo int a modificar.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildLayerList(string propertyName, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            property.intValue = EditorGUILayout.LayerField(propertyName, property.intValue);

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Enumeration of the available Sorting Layers in the project Tags&Layers.
        /// </summary>
        /// <param name="enumerationName"> Sorting Layers eumeration name.</param>
        /// <param name="property"> Int Sorting Layer representation property to modify.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las Sorting Layers del proyecto incluidas en el Tags&Layers.
        /// </summary>
        /// <param name="enumerationName">Nombre de la enumeración de Sorting Layers.</param>
        /// <param name="property">Propiedad de tipo string a modificar.</param>
        /// \endspanish
        public static void BuildSortingLayers(string enumerationName, SerializedProperty property)
        {

            Type internalEditorUtilityType = typeof(InternalEditorUtility);

            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);

            string[] _sortingLayerList;

            _sortingLayerList = (string[])sortingLayersProperty.GetValue(null, new object[0]);

            int index = Array.IndexOf(_sortingLayerList, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, _sortingLayerList);

            if (GUI.changed)
            {
                if (index < 0 || index > _sortingLayerList.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = _sortingLayerList[index];
                }

                GUI.changed = false;
            }
        }

        /// \english
        /// <summary>
        /// Enumeration of the available Sorting Layers in the project Tags&Layers with locking.
        /// </summary>
        /// <param name="enumerationName"> Sorting Layers eumeration name.</param>
        /// <param name="property"> Int Sorting Layer representation property to modify.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las Sorting Layers del proyecto incluidas en el Tags&Layers.
        /// </summary>
        /// <param name="propertyName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo string a modificar.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildSortingLayers(string enumerationName, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            Type internalEditorUtilityType = typeof(InternalEditorUtility);

            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);

            string[] _sortingLayerList;

            _sortingLayerList = (string[])sortingLayersProperty.GetValue(null, new object[0]);

            int index = Array.IndexOf(_sortingLayerList, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, _sortingLayerList);

            if (GUI.changed)
            {
                if (index < 0 || index > _sortingLayerList.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = _sortingLayerList[index];
                }

                GUI.changed = false;
            }

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Enumeration of the available Sorting Layers in the project Tags&Layers with locking.
        /// </summary>
        /// <param name="enumerationName"> Sorting Layers eumeration name.</param>
        /// <param name="property"> Int Sorting Layer representation property to modify.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una enumeración con todas las Sorting Layers del proyecto incluidas en el Tags&Layers.
        /// </summary>
        /// <param name="enumerationName">Nombre de la enumeración de tags.</param>
        /// <param name="property">Propiedad de tipo string a modificar.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildSortingLayers(string enumerationName, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }

            else
            {

                GUI.enabled = false;

            }

            Type internalEditorUtilityType = typeof(InternalEditorUtility);

            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);

            string[] _sortingLayerList;

            _sortingLayerList = (string[])sortingLayersProperty.GetValue(null, new object[0]);

            int index = Array.IndexOf(_sortingLayerList, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, _sortingLayerList);

            if (GUI.changed)
            {
                if (index < 0 || index > _sortingLayerList.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = _sortingLayerList[index];
                }

                GUI.changed = false;
            }

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Enumeration of the Input list.
        /// </summary>
        /// <param name="enumerationName"> Enumeration name.</param>
        /// <param name="property"> Representation property to modify.</param>
        /// \english
        /// \spanish
        /// <summary>
        /// Constructor de la lista de Inputs que devuelve un string.
        /// </summary>
        /// <param name="enumerationName"> Nombre de la enumeración.</param>
        /// <param name="property"> Nombre de la propiedad.</param>
        /// \spanish
        public static void BuildInputList(string enumerationName, SerializedProperty property)
        {
            string[] Languages = axes.ToArray();

            int index = Array.IndexOf(Languages, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, Languages);

            if (GUI.changed)
            {

                if (index < 0 || index >= Languages.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = Languages[index];
                }

                GUI.changed = false;
            }

        }

        /// \english
        /// <summary>
        /// Enumeration of the Input list with locking.
        /// </summary>
        /// <param name="enumerationName"> Enumeration name.</param>
        /// <param name="property"> Representation property to modify.</param>
        /// <param name="enumLocker"> Enumeration property that can lock.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \english
        /// \spanish
        /// <summary>
        /// Constructor de la lista de Inputs que devuelve un string con opción a bloqueo.
        /// </summary>
        /// <param name="enumerationName"> Nombre de la enumeración.</param>
        /// <param name="property"> Nombre de la propiedad.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \spanish
        public static void BuildInputList(string enumerationName, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            string[] _inputList = axes.ToArray();

            int index = Array.IndexOf(_inputList, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, _inputList);

            if (GUI.changed)
            {
                if (index < 0 || index >= _inputList.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = _inputList[index];
                }

                GUI.changed = false;
            }

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Enumeration of the Input list with locking.
        /// </summary>
        /// <param name="enumerationName"> Enumeration name.</param>
        /// <param name="property"> Representation property to modify.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \english
        /// \spanish
        /// <summary>
        /// Constructor de la lista de Inputs que devuelve un string con opción a bloqueo.
        /// </summary>
        /// <param name="enumerationName"> Nombre de la enumeración.</param>
        /// <param name="property"> Nombre de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \spanish
        public static void BuildInputList(string enumerationName, SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            string[] _inputList = axes.ToArray();

            int index = Array.IndexOf(_inputList, property.stringValue);

            index = EditorGUILayout.Popup(enumerationName, index, _inputList);

            if (GUI.changed)
            {
                if (index < 0 || index >= _inputList.Length)
                {
                    property.stringValue = string.Empty;
                }
                else
                {
                    property.stringValue = _inputList[index];
                }

                GUI.changed = false;
            }

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Returns the Axes List.
        /// </summary>
        /// <returns> List of Axes</returns>
        /// \english
        /// \spanish
        /// <summary>
        /// Devuelve la lista de Axes.
        /// </summary>
        /// <returns> Lista de Axes</returns>
        /// \spanish
        public static List<string> ReadInputs()
        {
            var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];

            SerializedObject obj = new SerializedObject(inputManager);

            List<string> _list = new List<string>();

            SerializedProperty axisArray = obj.FindProperty("m_Axes");

            for (int i = 0; i < axisArray.arraySize; ++i)
            {
                var axis = axisArray.GetArrayElementAtIndex(i);

                string name = axis.FindPropertyRelative("m_Name").stringValue;

                _list.Add(name);
            }

            axes = _list;

            return _list;
        }

        /// \english
        /// <summary>
        /// Component data manager.
        /// </summary>
        /// <typeparam name="T"> Component Type.</typeparam>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Permite gestionar los datos del componente objetivo.
        /// </summary>
        /// <typeparam name="T">Clase del componente objetivo.</typeparam>
        /// \endspanish
        public static void ManageComponentValues<T>() where T : Component
        {

            GUILayout.BeginHorizontal("Toolbar");

            GUILayout.Space(10.0f);

            GUILayout.Label("Copied: " + (copiedComponent != null ? copiedComponent : "Nothing"));

            GUILayout.FlexibleSpace();

            if (GUILayout.RepeatButton(new GUIContent("Copy", "Copy component values"), "toolbarButton"))
            {

                ComponentUtility.CopyComponent(Selection.activeGameObject.GetComponent<T>());

                copiedComponent = typeof(T).ToString();

                string[] splitString = copiedComponent.Split(new string[] { ".", }, StringSplitOptions.None);

                int c = splitString.Length;

                copiedComponent = splitString[c - 1];

            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Paste", "Paste component values"), "toolbarButton"))
            {

                ComponentUtility.PasteComponentValues(Selection.activeGameObject.GetComponent<T>());

            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Paste as New", "Paste the copied component as new."), "toolbarButton"))
            {

                ComponentUtility.PasteComponentAsNew(Selection.activeGameObject);

            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

        }

        /// \english
        /// <summary>
        /// Component Backup manager.
        /// </summary>
        /// <typeparam name="T"> Component type.</typeparam>
        /// <param name="serializedObject"> GameObject to which it is attached.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Método de guardado de componentes.
        /// </summary>
        /// <typeparam name="T"> Tipo del componente a guardar.</typeparam>
        /// <param name="serializedObject"> GameObject al que esta unido el componente.</param>
        /// \endspanish
        public static void BackUpComponentValues<T>(SerializedObject serializedObject)
        {
            LoadBackUpList();

            CGFComponentBackup componentBackup = new CGFComponentBackup();

            List<string> backupNames = new List<string>();

            foreach (CGFComponentBackup backup in backupList)
            {
                if (backup != null)
                {
                    backupNames.Add(backup.iD);
                }
            }

            if (backupNames.Count < 1)
            {
                backupNames.Add("None");
            }
            else
            {
                if (backupNames.Exists(name => name == "None"))
                {
                    backupNames.RemoveAt(0);
                }
            }

            GUILayout.BeginHorizontal("Toolbar");

            GUILayout.Space(10);

            GUILayout.Label("Backups: ", GUILayout.ExpandWidth(false));

            backupPopup = EditorGUILayout.Popup(backupPopup, backupNames.ToArray());

            GUILayout.Space(10);

            string backupDate;

            if (backupList.Exists(backup => backup.iD == backupNames[backupPopup]))
            {
                backupDate = backupList.Find(backup => backup.iD == backupNames[backupPopup]).date ?? "Nothing";

                GUILayout.Label(backupDate);
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("Backup", "Copy component values to a backup file."), "toolbarButton"))
            {
                componentBackup.information = JsonUtility.ToJson(serializedObject.targetObject);

                componentBackup.date = DateTime.Now.ToString();

                componentBackup.iD = serializedObject.targetObject.GetType().Name.ToString() + "  " + backupList.Count.ToString();

                componentBackup.t = typeof(T);

                if (backupList.Contains(componentBackup) == false)
                {
                    backupList.Insert(0, componentBackup);
                }

                SaveBackUpList();
            }

            GUILayout.Space(5);

            componentBackup = backupList.Find(backup => backup.iD == backupNames[backupPopup]);

            if (GUILayout.Button(new GUIContent("Restore", "Paste component values"), "toolbarButton"))
            {
                if (componentBackup != null)
                {
                    if (componentBackup.t == typeof(T))
                    {
                        JsonUtility.FromJsonOverwrite(componentBackup.information, serializedObject.targetObject);
                    }
                }
            }

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Remove", "Remove selected backup"), "toolbarButton"))
            {
                backupList.Remove(componentBackup);

                SaveBackUpList();

                if (backupPopup != 0)
                {
                    backupPopup -= 1;
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(10);
        }

        /// \english
        /// <summary>
        /// Save the Backup list information to an external file.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Guarda la información de la lista de backups en un archivo externo.
        /// </summary>
        /// \endspanish
        public static void SaveBackUpList()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/CGF/Editor default resources/backupInfo.dat", FileMode.OpenOrCreate);
            bf.Serialize(file, backupList);
            file.Close();
        }

        /// \english
        /// <summary>
        /// Load the Backup list information from an external file.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Carga la información de la lista de backups de un archivo externo.
        /// </summary>
        /// \endspanish
        public static void LoadBackUpList()
        {
            if (File.Exists(Application.dataPath + "/CGF/Editor default resources/backupInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.dataPath + "/CGF/Editor default resources/backupInfo.dat", FileMode.Open);
                backupList = (List<CGFComponentBackup>)bf.Deserialize(file);
                file.Close();
            }
        }

        #endregion


        #region Value Types

        #region Int

        /// \english
        /// <summary>
        /// Integer value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor entero.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// \endspanish
        public static void BuildInt(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Integer value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildInt(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Integer value builder with bool locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildInt(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        public static void BuildInt(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Integer value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildInt(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Integer value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Boolean que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildInt(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {
                GUI.enabled = true;

            }
            else
            {
                GUI.enabled = false;
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive integer value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor entero positivo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// \endspanish
        public static void BuildIntPositive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.intValue < 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive integer value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor entero positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildIntPositive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.intValue < 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive integer value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor entero positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="boolLocker"> Boolean que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildIntPositive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {
                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.intValue < 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Positive integer value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor entero positivo con sus correspondietes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildIntPositive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.intValue < 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive integer value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor entero positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildIntPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            if (property.intValue < 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive integer value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor entero positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildIntPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }


            EditorGUILayout.BeginHorizontal();

            if (property.intValue < 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative integer value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero negativo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// \endspanish
        public static void BuildIntNegative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.intValue > 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative integer value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildIntNegative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.intValue > 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative integer value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildIntNegative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.intValue > 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative integer value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero negativo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildIntNegative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.intValue > 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative integer value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero negativo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildIntNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.intValue > 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative integer value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero negativo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildIntNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.intValue > 0)
            {

                property.intValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Integer value builder within a slider.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero comprimido en una barra de valores.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="maxSliderValue"> Valor máximo de la barra de valores.</param>
        /// <param name="minSliderValue"> Valor mínimo de la barra de valores.</param>
        /// \endspanish
        public static void BuildIntSlider(string propertyName, string propertyDescription, SerializedProperty property, int minSliderValue, int maxSliderValue)
        {

            EditorGUILayout.BeginHorizontal();

            property.intValue = EditorGUILayout.IntSlider(BuildGUIContent(propertyName, propertyDescription), property.intValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Integer value builder within a slider with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="minSliderValue"> Slider maximum value.</param>
        /// <param name="maxSliderValue"> Slider minimum value.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero comprimido en una barra de valores con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="minSliderValue"> Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue"> Valor máximo del de la barra de valores.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildIntSlider(string propertyName, string propertyDescription, SerializedProperty property, int minSliderValue, int maxSliderValue, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.intValue = EditorGUILayout.IntSlider(BuildGUIContent(propertyName, propertyDescription), property.intValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Integer value builder within a slider with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="minSliderValue"> Slider maximum value.</param>
        /// <param name="maxSliderValue"> Slider minimum value.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero comprimido en una barra de valores con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="minSliderValue"> Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue"> Valor máximo del de la barra de valores.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildIntSlider(string propertyName, string propertyDescription, SerializedProperty property, int minSliderValue, int maxSliderValue, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }

            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.intValue = EditorGUILayout.IntSlider(BuildGUIContent(propertyName, propertyDescription), property.intValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Integer value builder within a slider with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="minSliderValue"> Slider maximum value.</param>
        /// <param name="maxSliderValue"> Slider minimum value.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero comprimido en una barra de valores con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="minSliderValue"> Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue"> Valor máximo del de la barra de valores.</param>
        /// \endspanish
        public static void BuildIntSlider(string propertyName, string propertyDescription, SerializedProperty property, string units, int minSliderValue, int maxSliderValue)
        {

            EditorGUILayout.BeginHorizontal();

            property.intValue = EditorGUILayout.IntSlider(BuildGUIContent(propertyName, propertyDescription), property.intValue, minSliderValue, maxSliderValue);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Integer value builder within a slider with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minSliderValue"> Slider maximum value.</param>
        /// <param name="maxSliderValue"> Slider minimum value.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero comprimido en una barra de valores con opción a bloqueo y con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="minSliderValue"> Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue"> Valor máximo del de la barra de valores.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildIntSlider(string propertyName, string propertyDescription, SerializedProperty property, string units, int minSliderValue, int maxSliderValue, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.intValue = EditorGUILayout.IntSlider(BuildGUIContent(propertyName, propertyDescription), property.intValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Integer value builder within a slider with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minSliderValue"> Slider maximum value.</param>
        /// <param name="maxSliderValue"> Slider minimum value.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor entero comprimido en una barra de valores con opción a bloqueo y con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="minSliderValue"> Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue"> Valor máximo del de la barra de valores.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildIntSlider(string propertyName, string propertyDescription, SerializedProperty property, string units, int minSliderValue, int maxSliderValue, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.intValue = EditorGUILayout.IntSlider(BuildGUIContent(propertyName, propertyDescription), property.intValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Float

        /// \english
        /// <summary>
        /// Float value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor float.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildFloat(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            var floarValue = EditorGUILayout.FloatField(new GUIContent(propertyName, propertyDescription), property.floatValue);

            if (EditorGUI.EndChangeCheck())
            {
                property.floatValue = floarValue;
            }

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor float con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloat(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor float con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker">Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloat(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildFloat(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor float con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloat(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor float con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker">Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloat(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive float value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float positivo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildFloatPositive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue < 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive float value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatPositive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue < 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive float value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker">Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatPositive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue < 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive float value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float positivo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildFloatPositive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue < 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive float value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue < 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive float value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }


            EditorGUILayout.BeginHorizontal();

            if (property.floatValue < 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative float value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float negativo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildFloatNegative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue > 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative float value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatNegative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue > 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative float value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatNegative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue > 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative float value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float negativo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildFloatNegative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue > 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative float value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float negativo con sus correspondientes unidades y con opción a bloqueo;
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue > 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative float value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float negativo con sus correspondientes unidades y con opción a bloqueo;
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.floatValue > 0)
            {

                property.floatValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas grande o igual existente.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildFloatCeil(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Ceil(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas grande o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatCeil(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Ceil(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas grande o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatCeil(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Ceil(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas grande o igual existente con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildFloatCeil(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Ceil(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas grande o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatCeil(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Ceil(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas grande o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatCeil(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Ceil(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas pequeño o igual existente.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildFloatFloor(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Floor(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas pequeño o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatFloor(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Floor(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas pequeño o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatFloor(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Floor(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas pequeño o igual existente con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildFloatFloor(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Floor(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas pequeño o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatFloor(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Floor(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas pequeño o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatFloor(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Floor(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas cercano.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildFloatRounded(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Round(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas cercano con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatRounded(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Round(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas cercano con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatRounded(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Round(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildFloatRounded(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Round(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas cercano con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatRounded(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Round(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float aproximado al entero mas cercano con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatRounded(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Mathf.Round(property.floatValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder within a slider.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float comprimido en una barra de valores.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// \endspanish
        public static void BuildFloatSlider(string propertyName, string propertyDescription, SerializedProperty property, float minSliderValue, float maxSliderValue)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = EditorGUILayout.Slider(BuildGUIContent(propertyName, propertyDescription), property.floatValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with locking within a slider.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        ///  Constructor de un valor float comprimido en una barra de valores con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatSlider(string propertyName, string propertyDescription, SerializedProperty property, float minSliderValue, float maxSliderValue, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = EditorGUILayout.Slider(BuildGUIContent(propertyName, propertyDescription), property.floatValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with locking within a slider.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        ///  Constructor de un valor float comprimido en una barra de valores con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatSlider(string propertyName, string propertyDescription, SerializedProperty property, float minSliderValue, float maxSliderValue, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = EditorGUILayout.Slider(BuildGUIContent(propertyName, propertyDescription), property.floatValue, minSliderValue, maxSliderValue);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units within a slider.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float comprimido en una barra de valores con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// \endspanish
        public static void BuildFloatSlider(string propertyName, string propertyDescription, SerializedProperty property, string units, float minSliderValue, float maxSliderValue)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = EditorGUILayout.Slider(BuildGUIContent(propertyName, propertyDescription), property.floatValue, minSliderValue, maxSliderValue);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Float value builder with units and loking within a slider
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float comprimido en una barra de valores con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildFloatSlider(string propertyName, string propertyDescription, SerializedProperty property, string units, float minSliderValue, float maxSliderValue, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = EditorGUILayout.Slider(BuildGUIContent(propertyName, propertyDescription), property.floatValue, minSliderValue, maxSliderValue);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Float value builder with units and loking within a slider
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor float comprimido en una barra de valores con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildFloatSlider(string propertyName, string propertyDescription, SerializedProperty property, string units, float minSliderValue, float maxSliderValue, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.floatValue = EditorGUILayout.Slider(BuildGUIContent(propertyName, propertyDescription), property.floatValue, minSliderValue, maxSliderValue);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Builds a margin between two float values within an slider.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// <param name="minLimitValue"> Margin minimum value.</param>
        /// <param name="maxLimitValue"> Margin maximum value.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de dos valores float que actuan como márgenes para la selección de un entero en un rango aleatorio. 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del rango seleccionado.</param>
        /// <param name="maxSliderValue">Valor máximo del rango seleccionado.</param>
        /// <param name="minLimitValue">Valor mínimo de la barra de valores.</param>
        /// <param name="maxLimitValue">Valor máximo de la bara de valores.</param>
        /// \endspanish
        public static void BuildSliderRanged(string propertyName, string propertyDescription, SerializedProperty minSliderValue, SerializedProperty maxSliderValue, float minLimitValue, float maxLimitValue)
        {

            float minSlider = minSliderValue.floatValue;

            float maxSlider = maxSliderValue.floatValue;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.MinMaxSlider(BuildGUIContent(propertyName, propertyDescription), ref minSlider, ref maxSlider, minLimitValue, maxLimitValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            minSliderValue.floatValue = EditorGUILayout.FloatField("Min", minSlider);

            maxSliderValue.floatValue = EditorGUILayout.FloatField("Max", maxSlider);

            if (maxSliderValue.floatValue <= minSliderValue.floatValue)
            {

                maxSliderValue.floatValue = minSliderValue.floatValue;

            }
            else if (minSliderValue.floatValue >= maxSliderValue.floatValue)
            {

                minSliderValue.floatValue = maxSliderValue.floatValue;

            }

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Builds a margin between two float values within an slider with locking. 
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// <param name="minLimitValue"> Margin minimum value.</param>
        /// <param name="maxLimitValue"> Margin maximum value.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de dos valores float que actuan como márgenes para la selección de un entero en un rango aleatorio con opción a bloqueo. 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="minLimitValue">Valor mínimo límite de la barra de valores</param>
        /// <param name="maxLimitValue">Valor máximo límite de la barra de valores.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildSliderRanged(string propertyName, string propertyDescription, SerializedProperty minSliderValue, SerializedProperty maxSliderValue, float minLimitValue, float maxLimitValue, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            float minSlider = minSliderValue.floatValue;

            float maxSlider = maxSliderValue.floatValue;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.MinMaxSlider(BuildGUIContent(propertyName, propertyDescription), ref minSlider, ref maxSlider, minLimitValue, maxLimitValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            minSliderValue.floatValue = EditorGUILayout.FloatField("Min", minSlider);

            maxSliderValue.floatValue = EditorGUILayout.FloatField("Max", maxSlider);

            if (maxSliderValue.floatValue <= minSliderValue.floatValue)
            {

                maxSliderValue.floatValue = minSliderValue.floatValue;

            }
            else if (minSliderValue.floatValue >= maxSliderValue.floatValue)
            {

                minSliderValue.floatValue = maxSliderValue.floatValue;

            }

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Builds a margin between two float values within an slider with locking. 
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// <param name="minLimitValue"> Margin minimum value.</param>
        /// <param name="maxLimitValue"> Margin maximum value.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de dos valores float que actuan como márgenes para la selección de un entero en un rango aleatorio con opción a bloqueo. 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="minLimitValue">Valor mínimo límite de la barra de valores</param>
        /// <param name="maxLimitValue">Valor máximo límite de la barra de valores.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildSliderRanged(string propertyName, string propertyDescription, SerializedProperty minSliderValue, SerializedProperty maxSliderValue, float minLimitValue, float maxLimitValue, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            float minSlider = minSliderValue.floatValue;

            float maxSlider = maxSliderValue.floatValue;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.MinMaxSlider(BuildGUIContent(propertyName, propertyDescription), ref minSlider, ref maxSlider, minLimitValue, maxLimitValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            minSliderValue.floatValue = EditorGUILayout.FloatField("Min", minSlider);

            maxSliderValue.floatValue = EditorGUILayout.FloatField("Max", maxSlider);

            if (maxSliderValue.floatValue <= minSliderValue.floatValue)
            {

                maxSliderValue.floatValue = minSliderValue.floatValue;

            }
            else if (minSliderValue.floatValue >= maxSliderValue.floatValue)
            {

                minSliderValue.floatValue = maxSliderValue.floatValue;

            }

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Builds a margin between two float values within an slider with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minLimitValue"> Margin minimum value.</param>
        /// <param name="maxLimitValue"> Margin maximum value.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de dos valores float que actuan como márgenes para la selección de un entero en un rango aleatorio con sus correspondientes unidades. 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="minLimitValue">Valor mínimo límite de la barra de valores</param>
        /// <param name="maxLimitValue">Valor máximo límite de la barra de valores.</param>
        /// \endspanish
        public static void BuildSliderRanged(string propertyName, string propertyDescription, SerializedProperty minSliderValue, SerializedProperty maxSliderValue, string units, float minLimitValue, float maxLimitValue)
        {

            float minSlider = minSliderValue.floatValue;

            float maxSlider = maxSliderValue.floatValue;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.MinMaxSlider(BuildGUIContent(propertyName, propertyDescription), ref minSlider, ref maxSlider, minLimitValue, maxLimitValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            minSliderValue.floatValue = EditorGUILayout.FloatField("Min", minSlider);

            maxSliderValue.floatValue = EditorGUILayout.FloatField("Max", maxSlider);

            if (maxSliderValue.floatValue <= minSliderValue.floatValue)
            {

                maxSliderValue.floatValue = minSliderValue.floatValue;

            }
            else if (minSliderValue.floatValue >= maxSliderValue.floatValue)
            {

                minSliderValue.floatValue = maxSliderValue.floatValue;

            }

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Builds a margin between two float values within an slider with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minLimitValue"> Margin minimum value.</param>
        /// <param name="maxLimitValue"> Margin maximum value.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de dos valores float que actuan como márgenes para la selección de un entero en un rango aleatorio con sus correspondientes unidades y con opción a bloqueo. 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="minLimitValue">Valor mínimo límite de la barra de valores</param>
        /// <param name="maxLimitValue">Valor máximo límite de la barra de valores.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildSliderRanged(string propertyName, string propertyDescription, SerializedProperty minSliderValue, SerializedProperty maxSliderValue, string units, float minLimitValue, float maxLimitValue, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            float minSlider = minSliderValue.floatValue;

            float maxSlider = maxSliderValue.floatValue;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.MinMaxSlider(BuildGUIContent(propertyName, propertyDescription), ref minSlider, ref maxSlider, minLimitValue, maxLimitValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            minSliderValue.floatValue = EditorGUILayout.FloatField("Min", minSlider);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            maxSliderValue.floatValue = EditorGUILayout.FloatField("Max", maxSlider);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (maxSliderValue.floatValue <= minSliderValue.floatValue)
            {

                maxSliderValue.floatValue = minSliderValue.floatValue;

            }
            else if (minSliderValue.floatValue >= maxSliderValue.floatValue)
            {

                minSliderValue.floatValue = maxSliderValue.floatValue;

            }

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Builds a margin between two float values within an slider with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="minSliderValue"> Slider minimum value.</param>
        /// <param name="maxSliderValue"> Slider maximum value.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="minLimitValue"> Margin minimum value.</param>
        /// <param name="maxLimitValue"> Margin maximum value.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de dos valores float que actuan como márgenes para la selección de un entero en un rango aleatorio con sus correspondientes unidades y con opción a bloqueo. 
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="minSliderValue">Valor mínimo del de la barra de valores.</param>
        /// <param name="maxSliderValue">Valor máximo del de la barra de valores.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="minLimitValue">Valor mínimo límite de la barra de valores</param>
        /// <param name="maxLimitValue">Valor máximo límite de la barra de valores.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildSliderRanged(string propertyName, string propertyDescription, SerializedProperty minSliderValue, SerializedProperty maxSliderValue, string units, float minLimitValue, float maxLimitValue, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            float minSlider = minSliderValue.floatValue;

            float maxSlider = maxSliderValue.floatValue;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.MinMaxSlider(BuildGUIContent(propertyName, propertyDescription), ref minSlider, ref maxSlider, minLimitValue, maxLimitValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            minSliderValue.floatValue = EditorGUILayout.FloatField("Min", minSlider);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            maxSliderValue.floatValue = EditorGUILayout.FloatField("Max", maxSlider);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            if (maxSliderValue.floatValue <= minSliderValue.floatValue)
            {

                maxSliderValue.floatValue = minSliderValue.floatValue;

            }
            else if (minSliderValue.floatValue >= maxSliderValue.floatValue)
            {

                minSliderValue.floatValue = maxSliderValue.floatValue;

            }

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Double

        /// \english
        /// <summary>
        /// Double value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor double.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildDouble(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor double con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDouble(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor double con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDouble(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildDouble(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor double con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDouble(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>

        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor double con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDouble(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive double value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double positivo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildDoublePositive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue < 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive double value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoublePositive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue < 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive double value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>

        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoublePositive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue < 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive double value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double positivo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildDoublePositive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue < 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive double value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoublePositive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue < 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive double value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoublePositive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue < 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative double value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double negativo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildDoubleNegative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue > 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative double value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleNegative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue > 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative double value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>

        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleNegative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue > 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative double value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double negativo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleNegative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue > 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative double value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double negativo con sus correspondientes unidades y con opción a bloqueo;
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue > 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative double value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double negativo con sus correspondientes unidades y con opción a bloqueo;
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }


            EditorGUILayout.BeginHorizontal();

            if (property.doubleValue > 0)
            {

                property.doubleValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas grande o igual existente.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildDoubleCeil(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Ceiling(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas grande o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleCeil(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Ceiling(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas grande o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleCeil(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Ceiling(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas grande o igual existente con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleCeil(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Ceiling(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas grande o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleCeil(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Ceiling(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking representing the smallest integer that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas grande o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleCeil(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Ceiling(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas pequeño o igual existente.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildDoubleFloor(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Floor(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas pequeño o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleFloor(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Floor(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas pequeño o igual existente con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleFloor(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Floor(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas pequeño o igual existente con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleFloor(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Floor(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas pequeño o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleFloor(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Floor(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking representing the largest integer that is less than or equal to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas pequeño o igual existente con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleFloor(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Floor(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas cercano.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildDoubleRounded(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Round(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas cercano con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleRounded(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Round(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas cercano con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleRounded(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Round(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildDoubleRounded(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Round(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas cercano con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish       
        public static void BuildDoubleRounded(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Round(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Double value builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor double aproximado al entero mas cercano con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units">Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish       
        public static void BuildDoubleRounded(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            property.doubleValue = Math.Round(property.doubleValue);

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Long

        /// \english
        /// <summary>
        /// Long value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor long.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// \endspanish
        public static void BuildLong(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Long value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildLong(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Long value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildLong(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Long value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        /// Constructor de un valor long con opción a bloqueo.
        public static void BuildLong(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Long value builder with locking and units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildLong(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Long value builder with locking and units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildLong(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive long value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor long positivo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// \endspanish
        public static void BuildLongPositive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.longValue < 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive long value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor long positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildLongPositive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue < 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Positive long value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor long positivo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildLongPositive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue < 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive long value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor long positivo con sus correspondietes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildLongPositive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.longValue < 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive long value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor long positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildLongPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue < 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive long value builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Contructor de un valor long positivo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildLongPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue < 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative long value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long negativo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// \endspanish
        public static void BuildLongNegative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.longValue > 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative long value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildLongNegative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue > 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative long value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long negativo con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildLongNegative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue > 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative long value builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long negativo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildLongNegative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            if (property.longValue > 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative long value builder with units and locking..
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long negativo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker"> Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues"> Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildLongNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue > 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative long value builder with units and locking..
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property tooltip.</param>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un valor long negativo con sus correspondientes unidades y con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName"> Nombre de la propiedad.</param>
        /// <param name="propertyDescription"> Descripción de la propiedad.</param>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildLongNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            if (property.longValue > 0)
            {

                property.longValue = 0;

            }

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Boolean

        /// \english
        /// <summary>
        /// Boolean value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una variable tipo boolean.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildBoolean(bool floatConverter, string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            property.floatValue = Convert.ToSingle(EditorGUILayout.Toggle(new GUIContent(propertyName, propertyDescription), Convert.ToBoolean(property.floatValue)));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Boolean value builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una variable tipo boolean.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildBoolean(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Boolean value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una variable tipo boolean con opcion a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildBoolean(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Boolean value builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una variable tipo boolean con opcion a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildBoolean(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Vector2

        /// \english
        /// <summary>
        /// Vector2 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector2(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Vector2 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector2(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Vector2 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector2(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Vector2 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector2(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Vector2 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector2(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Vector2 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector2(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Positive Vector2 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 positiva.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector2Positive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive Vector2 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 positiva con opción de bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Positive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector2 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 positiva con opción de bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Positive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector2 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 positiva con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Positive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Positive Vector2 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 positiva con opción de bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Positive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector2 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 positiva con opción de bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Positive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative Vector2 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 negativa.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector2Negative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative Vector2 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 negativa con opción de bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Negative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative Vector2 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 negativa con opción de bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Negative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Negative Vector2 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 negativa con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Negative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative Vector2 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 negativa con opción de bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Negative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector2 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector2 negativa con opción de bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector2Negative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector2 vector = property.vector2Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }

            property.vector2Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Vector3

        /// \english
        /// <summary>
        /// Vector3 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector3(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector3(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector3Rounded(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Rounded(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Rounded(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with units representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Rounded(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Rounded(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector3 structure builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Rounded(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positiva.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector3Positive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positiva con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Positive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positiva con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Positive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positiva con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Positive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positiva con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Positive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positiva con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Positive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positivo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector3RoundedPositive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positivo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedPositive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positivo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedPositive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with units representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positivo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedPositive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positivo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Positive Vector3 structure builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 positivo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedPositive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativa.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector3Negative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativa con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Negative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativa con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Negative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativa con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Negative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {
            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativa con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Negative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativa con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3Negative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector3RoundedNegative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedNegative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedNegative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with units representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedNegative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector3 structure builder with units and locking representing the nearest integer or to the specified value.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector3 negativo con opción a bloqueo aproximado al entero mas cercano con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector3RoundedNegative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector3 vector = property.vector3Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }

            vector.Set(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector.z));

            property.vector3Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Vector4

        /// \english
        /// <summary>
        /// Vector4 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector4(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Vector4 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector4(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector4 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector4(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector4 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector4(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Vector4 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector4(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Vector4 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector4(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector4 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 positiva.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector4Positive(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }
            else if (vector.w < 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Positive Vector4 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 positiva con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Positive(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }
            else if (vector.w < 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector4 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 positiva con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Positive(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }
            else if (vector.w < 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Positive Vector4 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 positiva con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Positive(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }
            else if (vector.w < 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Positive Vector4 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 positiva con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Positive(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }
            else if (vector.w < 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Positive Vector4 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 positiva con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Positive(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x < 0)
            {

                vector.x = 0;

            }
            else if (vector.y < 0)
            {

                vector.y = 0;

            }
            else if (vector.z < 0)
            {

                vector.z = 0;

            }
            else if (vector.w < 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector4 structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 negativa.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildVector4Negative(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }
            else if (vector.w > 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Negative Vector4 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 negativa con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Negative(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }
            else if (vector.w > 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector4 structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 negativa con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Negative(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }
            else if (vector.w > 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector4 structure builder with units.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 negativa con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Negative(string propertyName, string propertyDescription, SerializedProperty property, string units)
        {
            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }
            else if (vector.w > 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Negative Vector4 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 negativa con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Negative(string propertyName, string propertyDescription, SerializedProperty property, string units, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }
            else if (vector.w > 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Negative Vector4 structure builder with units and locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="units"> Property units.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura de valores Vector4 negativa con opción a bloqueo con sus correspondientes unidades.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="units"> Unidades de la propiedad.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildVector4Negative(string propertyName, string propertyDescription, SerializedProperty property, string units, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            Vector4 vector = property.vector4Value;

            if (vector.x > 0)
            {

                vector.x = 0;

            }
            else if (vector.y > 0)
            {

                vector.y = 0;

            }
            else if (vector.z > 0)
            {

                vector.z = 0;

            }
            else if (vector.w > 0)
            {

                vector.w = 0;

            }

            property.vector4Value = vector;

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            GUILayout.Label(units, GUILayout.ExpandWidth(false));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Color

        /// \english
        /// <summary>
        /// Color structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura Color.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildColor(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Color structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura Color con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildColor(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Color structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la estructura Color con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildColor(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Enum

        /// \english
        /// <summary>
        /// Enumeration builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Enumeración.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildEnum(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, BuildGUIContent(propertyName, propertyDescription));

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Enumeration builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Enumeración con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildEnum(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, BuildGUIContent(propertyName, propertyDescription));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Enumeration builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Enumeración con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildEnum(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, BuildGUIContent(propertyName, propertyDescription));

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Text

        /// \english
        /// <summary>
        /// Text builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de Texto.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildText(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Text builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de Texto con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildText(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Text builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de Texto con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildText(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Rect

        /// \english
        /// <summary>
        /// Rect structure builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una estructura Rect.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildRect(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Rect structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una estructura Rect con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildRect(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Rect structure builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una estructura Rect con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildRect(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Animation Curve

        /// \english
        /// <summary>
        /// Animation Curve builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description.</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Curva de Animación.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildAnimationCurve(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Animation Curve builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Curva de Animación con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildAnimationCurve(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Animation Curve builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Curva de Animación con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildAnimationCurve(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Animation Curve builder.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description.</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="color"> Curve color.</param>
        /// <param name="curveRange"> Curve range.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Curva de Animación.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="color">Color de la curva.</param>
        /// <param name="curveRange"> Rango de la curva.</param>
        /// \endspanish
        public static void BuildAnimationCurve(string propertyName, string propertyDescription, SerializedProperty property, Color color, Rect curveRange)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Animation Curve builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="curveRange"> Curve range.</param>
        /// <param name="color"> Curve color.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Curva de Animación con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="curveRange"> Rango de la curva.</param>
        /// <param name="color"> Color de la curva.</param> 
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildAnimationCurve(string propertyName, string propertyDescription, SerializedProperty property, Color color, Rect curveRange, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// Animation Curve builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// <param name="color"> Curve color.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Curva de Animación con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// <param name="color"> Color de la curva.</param>
        /// \endspanish
        public static void BuildAnimationCurve(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker, Color color, Rect curveRange)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

        }

        #endregion

        #region Object

        /// \english
        /// <summary>
        /// Object builder.
        /// </summary>
        /// <typeparam name="T"> Object Type.</typeparam>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un Objeto de cualquier tipo.
        /// </summary>
        /// <typeparam name="T">Tipo del Objeto a construir.</typeparam>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildObject<T>(string propertyName, string propertyDescription, SerializedProperty property, GUILayoutOption options)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), options);

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Object builder.
        /// </summary>
        /// <typeparam name="T"> Object Type.</typeparam>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un Objeto de cualquier tipo.
        /// </summary>
        /// <typeparam name="T">Tipo del Objeto a construir.</typeparam>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildObject<T>(string propertyName, string propertyDescription, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();
        }

        /// \english
        /// <summary>
        /// Object builder with locking.
        /// </summary>
        /// <typeparam name="T"> Object Type</typeparam>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un Objeto de cualquier tipo con opción a bloqueo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// \endspanish
        public static void BuildObject<T>(string propertyName, string propertyDescription, SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Object builder with locking.
        /// </summary>
        /// <typeparam name="T"> Object Type</typeparam>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="property"> Property to build.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un Objeto de cualquier tipo con opción a bloqueo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// \endspanish
        public static void BuildObject<T>(string propertyName, string propertyDescription, SerializedProperty property, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(property, new GUIContent(propertyName, propertyDescription), true);

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        #endregion

        #region Texture

        /// \english
        /// <summary>
        /// Textur property builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="tooltip"></param>
        /// <param name="texture"></param>
        /// <param name="tiling"></param>
        /// <param name="offset"></param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una propiedad de tipo textura.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="tooltip"></param>
        /// <param name="texture"></param>
        /// <param name="tiling"></param>
        /// <param name="offset"></param>
        /// \endspanish
        public static void BuildTexture(string label, string tooltip, SerializedProperty texture, SerializedProperty tiling, SerializedProperty offset)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(new GUIContent(label, tooltip));

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            EditorGUIUtility.labelWidth = 55.0f;

            GUILayout.Space(20);

            EditorGUILayout.PrefixLabel("Tiling");

            EditorGUILayout.PropertyField(tiling, GUIContent.none);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(20);

            EditorGUILayout.PrefixLabel("Offset");

            EditorGUILayout.PropertyField(offset, GUIContent.none);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            texture.objectReferenceValue = EditorGUILayout.ObjectField(texture.objectReferenceValue as Texture, typeof(Texture), true, GUILayout.Height(65), GUILayout.Width(65)) as Texture;

            EditorGUIUtility.labelWidth = 0;

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Textur property builder.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="tooltip"></param>
        /// <param name="texture"></param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una propiedad de tipo textura.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="tooltip"></param>
        /// <param name="texture"></param>
        /// \endspanish
        public static void BuildTexture(string label, string tooltip, SerializedProperty texture)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(new GUIContent(label, tooltip));

            EditorGUILayout.Space();

            EditorGUIUtility.labelWidth = 55.0f;

            texture.objectReferenceValue = EditorGUILayout.ObjectField(texture.objectReferenceValue as Texture, typeof(Texture), true, GUILayout.Height(65), GUILayout.Width(65)) as Texture;

            EditorGUIUtility.labelWidth = 0;

            EditorGUILayout.EndHorizontal();

        }

        #endregion

        /// \english
        /// <summary>
        /// Enable or disable a keyword according to its parameters.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="enable">Property float value used to enable o disable the keyword.</param>
        /// <param name="mode">Defines the default behavior of the keyword. If true, the keyword is enabled by default. If false, the keyword is disabled by default.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="enable">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="mode">Define el comportamiento por defecto de la keyword. Si es true, la keyword esá activada por defecto. Si es false, la keyword esá desactivada por defecto.</param>
        /// \endspanish 
        public static void SetKeyword(SerializedProperty material, SerializedProperty property, bool mode)
        {

            if (mode)
            {

                SetKeywordInternal(material, property, property.boolValue, "_ON");

            }
            else
            {

                SetKeywordInternal(material, property, !property.boolValue, "_OFF");

            }

        }

        /// \english
        /// <summary>
        /// Enable or disable a keyword.
        /// </summary>
        /// <param name="property">Property that manage the keyword status.</param>
        /// <param name="on">Property float value used to enable o disable the keyword.</param>
        /// <param name="defaultKeywordSuffix">Keyword suffix.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Activa o desactiva una keyword de acuerdo con sus parámetros.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el estado de la keyword.</param>
        /// <param name="on">El valor de la propiedad usado para activar o desactivar la keyword.</param>
        /// <param name="defaultKeywordSuffix">Sufijo dela keyword.</param>
        /// \endspanish 
        private static void SetKeywordInternal(SerializedProperty material, SerializedProperty property, bool on, string defaultKeywordSuffix)
        {

            string keyword = property.name.ToUpperInvariant() + defaultKeywordSuffix;

            Material target = material.objectReferenceValue as Material;

            if (target != null)
            {

                if (on)
                {

                    target.EnableKeyword(keyword);

                }
                else
                {

                    target.DisableKeyword(keyword);

                }

            }

        }

        /// \english
        /// <summary>
        /// Rendering mode enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the rendering mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de slección de modo de renderizado.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de renderizado.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildRenderModeEnum(SerializedProperty property, SerializedProperty materialProperty)
        {

            RenderMode renderMode = (RenderMode)property.floatValue;

            Material material = materialProperty.objectReferenceValue as Material;

            EditorGUI.BeginChangeCheck();

            renderMode = (RenderMode)EditorGUILayout.Popup(new GUIContent("Render Mode", "Rendering mode."), (int)renderMode, renderModeNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)renderMode;

                SetRenderMode(material, renderMode);

            }

        }

        /// \english
        /// <summary>
        /// Blending mode enumeration builder.
        /// </summary>
        /// <param name="property">Property that manages the blending mode.</param>
        /// <param name="materialEditor">Editor of material.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de slección de modo de fusión.
        /// </summary>
        /// <param name="property">Propiedad que gestiona el modo de fusión.</param>
        /// <param name="materialEditor">Editor del material.</param>
        /// \endspanish
        public static void BuildBlendModeEnum(SerializedProperty property, SerializedProperty materialProperty)
        {

            BlendMode blendMode = (BlendMode)property.floatValue;

            Material material = materialProperty.objectReferenceValue as Material;

            EditorGUI.BeginChangeCheck();

            blendMode = (BlendMode)EditorGUILayout.Popup(new GUIContent("Blend Mode", "Blending mode."), (int)blendMode, blendModeNames);

            if (EditorGUI.EndChangeCheck())
            {

                property.floatValue = (float)blendMode;

                SetBlendMode(material, blendMode);

                EditorUtility.SetDirty(material);

            }

        }

        /// \english
        /// <summary>
        /// Set the rendering mode of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="renderMode">Render modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de renderizado del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="renderMode">Enumeracion de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetRenderMode(Material material, RenderMode renderMode)
        {

            switch (renderMode)
            {

                case RenderMode.Opaque:

                    material.SetOverrideTag("RenderType", "Opaque");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.renderQueue = -1;

                    break;

                case RenderMode.Transparent:

                    material.SetOverrideTag("RenderType", "Transparent");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    material.SetInt("_ZWrite", 0);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

                    break;

                case RenderMode.Cutout:

                    material.SetOverrideTag("RenderType", "TransparentCutout");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 1);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);

                    material.EnableKeyword("_ALPHATEST_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

                    break;

                case RenderMode.Background:

                    material.SetOverrideTag("RenderType", "Opaque");

                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                    material.SetInt("_ZWrite", 0);

                    material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);

                    material.DisableKeyword("_ALPHATEST_ON");

                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Background;

                    break;

            }

        }


        /// \english
        /// <summary>
        /// Set the blending mode of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="blendMode">Blend modes enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el modo de fusión del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="blendMode">Blend de todos los modos de renderización.</param>
        /// \endspanish 
        public static void SetBlendMode(Material material, BlendMode blendMode)
        {

            switch (blendMode)
            {

                case BlendMode.Normal:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    break;

                case BlendMode.Darken:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_DARKEN");

                    break;

                case BlendMode.Multiply:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_MULTIPLY");

                    break;

                case BlendMode.ColorBurn:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_COLORBURN");

                    break;

                case BlendMode.LinearBurn:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LINEARBURN");

                    break;

                case BlendMode.DarkerColor:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_DARKERCOLOR");

                    break;

                case BlendMode.Lighten:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LIGHTEN");

                    break;

                case BlendMode.Screen:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SCREEN");

                    break;

                case BlendMode.ColorDodge:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_COLORDODGE");

                    break;

                case BlendMode.LinearDodgeOrAdditive:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LINEARDODGE");

                    break;

                case BlendMode.LighterColor:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LIGHTERCOLOR");

                    break;

                case BlendMode.Overlay:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_OVERLAY");

                    break;

                case BlendMode.SoftLight:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SOFTLIGHT");

                    break;

                case BlendMode.HardLight:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_HARDLIGHT");

                    break;

                case BlendMode.VividLight:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_VIVIDLIGHT");

                    break;

                case BlendMode.LinearLight:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LINEARLIGHT");

                    break;

                case BlendMode.PinLight:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_PINLIGHT");

                    break;

                case BlendMode.HardMix:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_HARDMIX");

                    break;

                case BlendMode.Difference:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_DIFFERENCE");

                    break;

                case BlendMode.Exclusion:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_EXCLUSION");

                    break;

                case BlendMode.Subtract:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SUBTRACT");

                    break;

                case BlendMode.Divide:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_DIVIDE");

                    break;

                case BlendMode.Hue:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_HUE");

                    break;

                case BlendMode.Saturation:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_SATURATION");

                    break;

                case BlendMode.Color:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_COLOR");

                    break;

                case BlendMode.Luminosity:

                    material.DisableKeyword("_BLENDMODE_DARKEN");
                    material.DisableKeyword("_BLENDMODE_MULTIPLY");
                    material.DisableKeyword("_BLENDMODE_COLORBURN");
                    material.DisableKeyword("_BLENDMODE_LINEARBURN");
                    material.DisableKeyword("_BLENDMODE_DARKERCOLOR");
                    material.DisableKeyword("_BLENDMODE_LIGHTEN");
                    material.DisableKeyword("_BLENDMODE_SCREEN");
                    material.DisableKeyword("_BLENDMODE_COLORDODGE");
                    material.DisableKeyword("_BLENDMODE_LINEARDODGE");
                    material.DisableKeyword("_BLENDMODE_LIGHTERCOLOR");
                    material.DisableKeyword("_BLENDMODE_OVERLAY");
                    material.DisableKeyword("_BLENDMODE_SOFTLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDLIGHT");
                    material.DisableKeyword("_BLENDMODE_VIVIDLIGHT");
                    material.DisableKeyword("_BLENDMODE_LINEARLIGHT");
                    material.DisableKeyword("_BLENDMODE_PINLIGHT");
                    material.DisableKeyword("_BLENDMODE_HARDMIX");
                    material.DisableKeyword("_BLENDMODE_DIFFERENCE");
                    material.DisableKeyword("_BLENDMODE_EXCLUSION");
                    material.DisableKeyword("_BLENDMODE_SUBTRACT");
                    material.DisableKeyword("_BLENDMODE_DIVIDE");
                    material.DisableKeyword("_BLENDMODE_HUE");
                    material.DisableKeyword("_BLENDMODE_SATURATION");
                    material.DisableKeyword("_BLENDMODE_COLOR");
                    material.DisableKeyword("_BLENDMODE_LUMINOSITY");

                    material.EnableKeyword("_BLENDMODE_LUMINOSITY");

                    break;

            }

        }

        #region Fold Out

        /// \english
        /// <summary>
        /// Fold Out builder.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        /// <param name="propertyDescription">Property description-</param>
        /// <param name="isUnfold">Is unfold?</param>
        /// <returns> Fold out status.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un Fold Out de la Propiedad asignada.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="isUnfold">¿Está desplegado?</param>
        /// \endspanish
        public static bool BuildContentFoldOut(string propertyName, string propertyDescription, bool isUnfold)
        {
            isUnfold = EditorGUILayout.Foldout(isUnfold, BuildGUIContent(propertyName, propertyDescription));

            return isUnfold;
        }

        /// \english
        /// <summary>
        /// Fold Out builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="isUnfold">Is unfold?</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// <returns> Fold Out status.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un Fold Out de la Propiedad asignada con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// /// <param name="isUnfold">¿Está desplegado?</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// <returns> Booleana que indica si el FoldOut se debe mostrar o no.</returns>
        /// \endspanish
        public static bool BuildContentFoldOut(string propertyName, string propertyDescription, bool isUnfold, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {

                    GUI.enabled = false;
                }
            }

            isUnfold = EditorGUILayout.Foldout(isUnfold, BuildGUIContent(propertyName, propertyDescription));

            GUI.enabled = true;

            return isUnfold;
        }

        /// \english
        /// <summary>
        /// Fold Out builder with locking.
        /// </summary>
        /// <param name="propertyName"> Property name.</param>
        /// <param name="propertyDescription"> Property description-</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// <param name="isUnfold">Is unfold?</param>
        /// <returns> Fold Out status.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un Fold Out de la Propiedad asignada con opción a bloqueo.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad.</param>
        /// <param name="propertyDescription">Descripción de la propiedad.</param>
        /// <param name="isUnfold">¿Está desplegado?</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// <returns> Booleana que indica si el FoldOut se debe mostrar o no.</returns>
        /// \endspanish
        public static bool BuildContentFoldOut(string propertyName, string propertyDescription, bool isUnfold, bool boolLocker)
        {
            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            isUnfold = EditorGUILayout.Foldout(isUnfold, BuildGUIContent(propertyName, propertyDescription));

            GUI.enabled = true;

            return isUnfold;
        }

        #endregion

        #region List

        /// \english
        /// <summary>
        /// Update, Add and Remove list buttons builder.
        /// </summary>
        /// <param name="property"> Porperty to build.</param>
        /// <param name="newListSize"> New list size.</param>
        /// <returns> New list size.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de los botones de Actualizar, Añadir y Eliminar elementos de la lista.
        /// </summary>
        /// <param name="property"> Propiedad a construir.</param>
        /// <param name="newListSize"> Número de elementos de la lista.</param>
        /// <returns> Número de elementos de la lista.</returns>
        /// \endspanish
        public static int BuildListButtons(SerializedProperty property, int newListSize)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(BuildGUIContent("Elements", "Number of list elements"), GUILayout.MaxWidth(80));

            newListSize = Mathf.Max(0, EditorGUILayout.IntField(newListSize, GUILayout.MaxWidth(80)));

            if (GUILayout.Button("Update"))
            {

                property.arraySize = newListSize;

            }

            if (GUILayout.Button("Add"))
            {

                property.arraySize++;

                property.serializedObject.ApplyModifiedProperties();

            }

            if (GUILayout.Button("Remove"))
            {

                property.arraySize--;

                property.serializedObject.ApplyModifiedProperties();

            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUI.enabled = true;

            return newListSize;

        }

        /// \english
        /// <summary>
        /// Update, Add and Remove list buttons builder with locking.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="newListSize"> New list size.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// <returns> New list size.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de los botones de Actualizar, Añadir y Eliminar elementos de la lista con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="newListSize">Número de elementos de la lista.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// <returns>Número de elementos de la lista.</returns>
        /// \endspanish
        public static int BuildListButtons(SerializedProperty property, int newListSize, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(BuildGUIContent("Elements", "Number of list elements"), GUILayout.MaxWidth(80));

            newListSize = Mathf.Max(0, EditorGUILayout.IntField(newListSize, GUILayout.MaxWidth(80)));

            if (GUILayout.Button("Update"))
            {

                property.arraySize = newListSize;

            }

            if (GUILayout.Button("Add"))
            {

                property.arraySize++;

                property.serializedObject.ApplyModifiedProperties();

            }

            if (GUILayout.Button("Remove"))
            {

                property.arraySize--;

                property.serializedObject.ApplyModifiedProperties();

            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUI.enabled = true;

            return newListSize;

        }

        /// \english
        /// <summary>
        /// Update, Add and Remove list buttons builder with locking.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="newListSize"> New list size.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// <returns> New list size.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de los botones de Actualizar, Añadir y Eliminar elementos de la lista con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="newListSize">Número de elementos de la lista.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// <returns>Número de elementos de la lista.</returns>
        /// \endspanish
        public static int BuildListButtons(SerializedProperty property, int newListSize, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(BuildGUIContent("Elements", "Number of list elements"), GUILayout.MaxWidth(80));

            newListSize = Mathf.Max(0, EditorGUILayout.IntField(newListSize, GUILayout.MaxWidth(80)));

            if (GUILayout.Button("Update"))
            {

                property.arraySize = newListSize;

            }

            if (GUILayout.Button("Add"))
            {

                property.arraySize++;

                property.serializedObject.ApplyModifiedProperties();

            }

            if (GUILayout.Button("Remove"))
            {

                property.arraySize--;

                property.serializedObject.ApplyModifiedProperties();

            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUI.enabled = true;

            return newListSize;

        }

        /// \english
        /// <summary>
        /// Value Reorderable List builder.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="list"> Reorderable List to build.</param>
        /// <param name="listName"> List name.</param>
        /// <returns> Updated Reorderable List.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Lista Reorderable de valores.
        /// </summary>
        /// <param name="property">Propiedad que contiene la lista.</param>
        /// <param name="list">Lista a construir.</param>
        /// <param name="listName">Nombre de la lista a construir.</param>
        /// <returns> Variable Lista actualizada.</returns>
        /// \endspanish
        public static ReorderableList BuildListValue(SerializedProperty property, ReorderableList list, string listName)
        {

            list.drawHeaderCallback = (Rect rect) =>
            {

                EditorGUI.LabelField(rect, listName + " (" + property.arraySize + ")");

            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {

                GUIStyle style = new GUIStyle(GUI.skin.label);

                GUIStyle style2 = new GUIStyle(GUI.skin.label);

                style.alignment = TextAnchor.MiddleLeft;

                style2.alignment = TextAnchor.MiddleLeft;

                rect.y += 2;

                if (property.GetArrayElementAtIndex(index) != null)
                {

                    Rect position = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                    EditorGUI.PropertyField(position, property.GetArrayElementAtIndex(index), BuildGUIContent("Element " + index, "List Element "));

                }

                property.serializedObject.ApplyModifiedProperties();

            };

            list.DoLayoutList();

            return list;

        }

        /// \english
        /// <summary>
        /// Value Reorderable List builder with locking.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="list"> Reorderable List to build.</param>
        /// <param name="listName"> List name.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// <returns> Updated Reorderable List.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Lista Reorderable de valores con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad que contiene la lista.</param>
        /// <param name="list">Lista que construir.</param>
        /// <param name="listName">Nombre de la lista a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// <returns> Variable Lista actualizada</returns>
        /// \endspanish
        public static ReorderableList BuildListValue(SerializedProperty property, ReorderableList list, string listName, SerializedProperty enumLocker, params int[] enumValues)
        {

            foreach (int value in enumValues)
            {

                if (enumLocker.enumValueIndex == value)
                {

                    GUI.enabled = true;

                    break;

                }
                else
                {

                    GUI.enabled = false;

                }

            }

            list.drawHeaderCallback = (Rect rect) =>
            {

                EditorGUI.LabelField(rect, listName + " (" + property.arraySize + ")");

            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {

                GUIStyle style = new GUIStyle(GUI.skin.label);

                GUIStyle style2 = new GUIStyle(GUI.skin.label);

                style.alignment = TextAnchor.MiddleLeft;

                style2.alignment = TextAnchor.MiddleLeft;

                rect.y += 2;

                if (property.GetArrayElementAtIndex(index) != null)
                {

                    Rect position = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                    EditorGUI.PropertyField(position, property.GetArrayElementAtIndex(index), BuildGUIContent("Element " + index, "List Element "));

                }

                property.serializedObject.ApplyModifiedProperties();

            };

            list.DoLayoutList();

            GUI.enabled = true;

            return list;

        }

        /// \english
        /// <summary>
        /// Value Reorderable List builder with locking.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="list"> Reorderable List to build.</param>
        /// <param name="listName"> List name.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// <returns> Updated Reorderable List.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Lista Reorderable de valores con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad que contiene la lista.</param>
        /// <param name="list">Lista que construir.</param>
        /// <param name="listName">Nombre de la lista a construir.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// <returns> Variable Lista actualizada</returns>
        /// \endspanish
        public static ReorderableList BuildListValue(SerializedProperty property, ReorderableList list, string listName, bool boolLocker)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            list.drawHeaderCallback = (Rect rect) =>
            {

                EditorGUI.LabelField(rect, listName + " (" + property.arraySize + ")");

            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {

                GUIStyle style = new GUIStyle(GUI.skin.label);

                GUIStyle style2 = new GUIStyle(GUI.skin.label);

                style.alignment = TextAnchor.MiddleLeft;

                style2.alignment = TextAnchor.MiddleLeft;

                rect.y += 2;

                if (property.GetArrayElementAtIndex(index) != null)
                {

                    Rect position = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);

                    EditorGUI.PropertyField(position, property.GetArrayElementAtIndex(index), BuildGUIContent("Element " + index, "List Element "));

                }

                property.serializedObject.ApplyModifiedProperties();

            };

            list.DoLayoutList();

            GUI.enabled = true;

            return list;

        }

        /// \english
        /// <summary>
        /// Custom Class Reorderable List builder.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="list"> Reorderable List to build.</param>
        /// <param name="listName"> List name.</param>
        /// <param name="vertical"> How to show subproperties.</param>
        /// <param name="propertySpace"> Space for each Custom Class property.</param>
        /// <param name="properties"> Custom Class property.</param>
        /// <returns> Updated Reorderable List.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Lista Reorderable con múltiples subpropiedades.
        /// </summary>
        /// <param name="property">Propiedad que contiene la lista.</param>
        /// <param name="list">Lista que construir.</param>
        /// <param name="listName">Nombre de la lista a construir.</param>
        /// <param name="vertical">Forma de las propiedades de la clase de la lista.</param>
        /// <param name="propertySpace">Espacio para cada propiedad de la lista</param>
        /// <param name="properties">Propiedades de la clase de la Lista a construir.</param>
        /// <returns> Lista actualizada.</returns>
        /// \endspanish
        public static ReorderableList BuildListCustom(SerializedProperty property, ReorderableList list, string listName, bool vertical, int[] propertySpace, params string[] properties)
        {           

            list.drawHeaderCallback = (Rect rect) =>
            {

                EditorGUI.LabelField(rect, listName + " (" + property.arraySize + ")");

            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);

                GUIStyle style2 = new GUIStyle(GUI.skin.label);

                style.alignment = TextAnchor.MiddleRight;

                style.stretchWidth = true;

                style2.alignment = TextAnchor.MiddleRight;

                style2.stretchWidth = true;

                rect.y += 2;

                if (property.arraySize > index)
                {

                    var element = list.serializedProperty.GetArrayElementAtIndex(index);

                    float propertyXPosition = rect.x;

                    if (vertical)
                    {
                        EditorGUI.LabelField(rect, listName);
                    }

                    int length = 0;

                    bool expanded = false;

                    int propertiesLength = 0;

                    for (int i = 0; i < properties.Length; i++)
                    {                        

                        if (properties[i] != null)
                        {

                            if (vertical)
                            {

                                float arrayMultiplier = 0;

                                if (expanded)
                                {
                                    arrayMultiplier = (EditorGUIUtility.singleLineHeight + 5) * length;
                                }

                                Rect position = new Rect(rect.x + 20, rect.y + (EditorGUIUtility.singleLineHeight + 3) * (i + 1) + arrayMultiplier, rect.width - 20, EditorGUIUtility.singleLineHeight);
                               
                                SerializedProperty propertyFind = element.FindPropertyRelative(properties[i]);

                                EditorGUI.PropertyField(position, propertyFind, true);
                                
                                if (propertyFind.isArray)
                                {

                                    expanded = propertyFind.isExpanded;

                                    length = propertyFind.arraySize;

                                    if (expanded)
                                    {

                                        propertiesLength += length;

                                    }
                                    
                                }
                                else
                                {

                                    expanded = false;

                                    length = 0;

                                }

                                propertiesLength++;

                            }
                            else
                            {

                                float propertyWidth = ((rect.width - 50) / 12) * propertySpace[i];

                                float elementWidth = propertyWidth / 2;

                                Rect labelPosition = new Rect(propertyXPosition, rect.y, elementWidth, EditorGUIUtility.singleLineHeight);

                                Rect propertyPosition = new Rect(propertyXPosition + elementWidth, rect.y, elementWidth, EditorGUIUtility.singleLineHeight);

                                EditorGUI.LabelField(labelPosition, properties[i].ToString(), style);

                                EditorGUI.PropertyField(propertyPosition, element.FindPropertyRelative(properties[i]), true);

                                propertyXPosition += propertyWidth;

                            }

                        }

                    }

                    if (vertical)
                    {

                        list.elementHeight = (EditorGUIUtility.singleLineHeight + 5) * (propertiesLength + 1);

                    }                    

                    Rect addButtonPosition = new Rect(rect.width - 9, rect.y, addButton.image.width, addButton.image.height);

                    Rect removeButtonPosition = new Rect(rect.width + 16, rect.y, deleteButton.image.width, deleteButton.image.height);

                    if (isPro)
                    {

                        if (CGFGUIHelper.IconButton(addButtonPosition, addButtonProfessional, addPressedButtonProfessional))
                        {

                            Add(index, property);
                        }

                        EditorGUI.LabelField(addButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);

                        if (CGFGUIHelper.IconButton(removeButtonPosition, deleteButtonProfessional, deletePressedButtonProfessional))
                        {

                            Delete(index, property);

                        }

                        EditorGUI.LabelField(removeButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);
                    }
                    else
                    {

                        if (CGFGUIHelper.IconButton(addButtonPosition, addButton, addPressedButton))
                        {

                            Add(index, property);

                        }

                        EditorGUI.LabelField(addButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);

                        if (CGFGUIHelper.IconButton(removeButtonPosition, deleteButton, deletePressedButton))
                        {

                            Delete(index, property);
                        }

                        EditorGUI.LabelField(removeButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);
                    }


                }

                property.serializedObject.ApplyModifiedProperties();
                

            };

            list.DoLayoutList();

            GUI.enabled = true;

            return list;

        }

        /// \english
        /// <summary>
        /// Custom Class Reorderable List builder with locking.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="list"> Reorderable List to build.</param>
        /// <param name="listName"> List name.</param>
        /// <param name="vertical"> How to show subproperties.</param>
        /// <param name="enumLocker"> Enumeration that locks the property.</param>
        /// <param name="enumValues"> Values to unlock the property.</param>
        /// <param name="propertySpace"> Space for each Custom Class property.</param>
        /// <param name="properties"> Custom Class property.</param>
        /// <returns> Updated Reorderable List.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Lista Reorderable con múltiples subpropiedades y con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad que contiene la lista.</param>
        /// <param name="list">Lista que construir.</param>
        /// <param name="listName">Nombre de la lista a construir.</param>
        /// <param name="vertical">Forma de las propiedades de la clase de la lista.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad.</param>
        /// <param name="enumValues">Valores para mostrar la propiedad.</param>
        /// <param name="propertySpace">Espacio para cada propiedad de la lista</param>
        /// <param name="properties">Propiedades de la clase de la Lista a construir.</param>
        /// <returns> Lista actualizada.</returns>
        /// \endspanish
        public static ReorderableList BuildListCustom(SerializedProperty property, ReorderableList list, string listName, bool vertical, SerializedProperty enumLocker, int[] enumValues, int[] propertySpace, params string[] properties)
        {

            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;

                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            list.drawHeaderCallback = (Rect rect) =>
            {

                EditorGUI.LabelField(rect, listName + " (" + property.arraySize + ")");
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);

                GUIStyle style2 = new GUIStyle(GUI.skin.label);

                style.alignment = TextAnchor.MiddleCenter;

                style.stretchWidth = true;

                style2.alignment = TextAnchor.MiddleCenter;

                style2.stretchWidth = true;

                rect.y += 2;

                if (property.arraySize > index)
                {

                    var element = list.serializedProperty.GetArrayElementAtIndex(index);

                    float propertyXPosition = rect.x;

                    if (vertical)
                    {
                        EditorGUI.LabelField(rect, listName);
                    }

                    int length = 0;

                    int propertiesLength = 0;

                    bool expanded = false;

                    for (int i = 0; i < properties.Length; i++)
                    {

                        propertiesLength++;

                        if (properties[i] != null)
                        {

                            if (vertical)
                            {

                                float arrayMultiplier = 0;

                                if (expanded)
                                {
                                    arrayMultiplier = (EditorGUIUtility.singleLineHeight + 5) * length;
                                }

                                Rect position = new Rect(rect.x + 20, rect.y + (EditorGUIUtility.singleLineHeight + 3) * (i + 1) + arrayMultiplier, rect.width - 20, EditorGUIUtility.singleLineHeight);

                                SerializedProperty propertyFind = element.FindPropertyRelative(properties[i]);

                                EditorGUI.PropertyField(position, propertyFind, true);

                                if (propertyFind.isArray)
                                {

                                    expanded = propertyFind.isExpanded;

                                    length = propertyFind.arraySize;

                                    if (expanded)
                                    {

                                        propertiesLength += length;

                                    }

                                }
                                else
                                {

                                    expanded = false;

                                    length = 0;

                                }

                            }
                            else
                            {

                                float propertyWidth = ((rect.width - 50) / 12) * propertySpace[i];

                                float elementWidth = propertyWidth / 2;

                                Rect labelPosition = new Rect(propertyXPosition, rect.y, elementWidth, EditorGUIUtility.singleLineHeight);

                                Rect propertyPosition = new Rect(propertyXPosition + elementWidth, rect.y, elementWidth, EditorGUIUtility.singleLineHeight);

                                EditorGUI.LabelField(labelPosition, properties[i].ToString(), style);

                                EditorGUI.PropertyField(propertyPosition, element.FindPropertyRelative(properties[i]), true);

                                propertyXPosition += propertyWidth;

                            }

                        }

                    }

                    if (vertical)
                    {

                        list.elementHeight = (EditorGUIUtility.singleLineHeight + 5) * (propertiesLength + 1);

                    }

                    Rect addButtonPosition = new Rect(rect.width - 9, rect.y, addButton.image.width, addButton.image.height);

                    Rect removeButtonPosition = new Rect(rect.width + 16, rect.y, deleteButton.image.width, deleteButton.image.height);

                    if (isPro)
                    {

                        if (CGFGUIHelper.IconButton(addButtonPosition, addButtonProfessional, addPressedButtonProfessional))
                        {

                            Add(index, property);
                        }

                        EditorGUI.LabelField(addButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);

                        if (CGFGUIHelper.IconButton(removeButtonPosition, deleteButtonProfessional, deleteButtonProfessional))
                        {

                            Delete(index, property);

                        }

                        EditorGUI.LabelField(removeButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);
                    }
                    else
                    {

                        if (CGFGUIHelper.IconButton(addButtonPosition, addButton, addPressedButton))
                        {

                            Add(index, property);

                        }

                        EditorGUI.LabelField(addButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);

                        if (CGFGUIHelper.IconButton(removeButtonPosition, deleteButton, deletePressedButton))
                        {

                            Delete(index, property);
                        }

                        EditorGUI.LabelField(removeButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);
                    }


                }

                property.serializedObject.ApplyModifiedProperties();
            };

            if (vertical)
            {
                list.elementHeightCallback = (index) =>
                {

                    return ((EditorGUIUtility.singleLineHeight + 3) * (properties.Length + 1));

                };
            }

            list.DoLayoutList();

            GUI.enabled = true;

            return list;
        }

        /// \english
        /// <summary>
        /// Custom Class Reorderable List builder with locking.
        /// </summary>
        /// <param name="property"> Property to build.</param>
        /// <param name="list"> Reorderable List to build.</param>
        /// <param name="listName"> List name.</param>
        /// <param name="vertical"> How to show subproperties.</param>
        /// <param name="boolLocker"> Boolean that locks the property.</param>
        /// <param name="propertySpace"> Space for each Custom Class property.</param>
        /// <param name="properties"> Custom Class property.</param>
        /// <returns> Updated Reorderable List.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una Lista Reorderable con múltiples subpropiedades y con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad que contiene la lista.</param>
        /// <param name="list">Lista que construir.</param>
        /// <param name="listName">Nombre de la lista a construir.</param>
        /// <param name="vertical">Forma de las propiedades de la clase de la lista.</param>
        /// <param name="boolLocker"> Booleano que bloquea la propiedad.</param>
        /// <param name="propertySpace">Espacio para cada propiedad de la lista</param>
        /// <param name="properties">Propiedades de la clase de la Lista a construir.</param>
        /// <returns> Lista actualizada.</returns>
        /// \endspanish
        public static ReorderableList BuildListCustom(SerializedProperty property, ReorderableList list, string listName, bool vertical, bool boolLocker, int[] propertySpace, params string[] properties)
        {

            if (boolLocker)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            list.drawHeaderCallback = (Rect rect) =>
            {

                EditorGUI.LabelField(rect, listName + " (" + property.arraySize + ")");
            };

            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                GUIStyle style = new GUIStyle(GUI.skin.label);

                GUIStyle style2 = new GUIStyle(GUI.skin.label);

                style.alignment = TextAnchor.MiddleCenter;

                style.stretchWidth = true;

                style2.alignment = TextAnchor.MiddleCenter;

                style2.stretchWidth = true;

                rect.y += 2;

                if (property.arraySize > index)
                {

                    var element = list.serializedProperty.GetArrayElementAtIndex(index);

                    float propertyXPosition = rect.x;

                    if (vertical)
                    {
                        EditorGUI.LabelField(rect, listName);
                    }

                    int length = 0;

                    int propertiesLength = 0;

                    bool expanded = false;

                    for (int i = 0; i < properties.Length; i++)
                    {

                        propertiesLength++;

                        if (properties[i] != null)
                        {

                            if (vertical)
                            {

                                float arrayMultiplier = 0;

                                if (expanded)
                                {
                                    arrayMultiplier = (EditorGUIUtility.singleLineHeight + 5) * length;
                                }

                                Rect position = new Rect(rect.x + 20, rect.y + (EditorGUIUtility.singleLineHeight + 3) * (i + 1) + arrayMultiplier, rect.width - 20, EditorGUIUtility.singleLineHeight);

                                SerializedProperty propertyFind = element.FindPropertyRelative(properties[i]);

                                EditorGUI.PropertyField(position, propertyFind, true);

                                if (propertyFind.isArray)
                                {

                                    expanded = propertyFind.isExpanded;

                                    length = propertyFind.arraySize;

                                    if (expanded)
                                    {

                                        propertiesLength += length;

                                    }

                                }
                                else
                                {

                                    expanded = false;

                                    length = 0;

                                }

                            }
                            else
                            {

                                float propertyWidth = ((rect.width - 50) / 12) * propertySpace[i];

                                float elementWidth = propertyWidth / 2;

                                Rect labelPosition = new Rect(propertyXPosition, rect.y, elementWidth, EditorGUIUtility.singleLineHeight);

                                Rect propertyPosition = new Rect(propertyXPosition + elementWidth, rect.y, elementWidth, EditorGUIUtility.singleLineHeight);

                                EditorGUI.LabelField(labelPosition, properties[i].ToString(), style);

                                EditorGUI.PropertyField(propertyPosition, element.FindPropertyRelative(properties[i]), true);

                                propertyXPosition += propertyWidth;

                            }

                        }

                    }

                    if (vertical)
                    {

                        list.elementHeight = (EditorGUIUtility.singleLineHeight + 5) * (propertiesLength + 1);

                    }

                    Rect addButtonPosition = new Rect(rect.width - 9, rect.y, addButton.image.width, addButton.image.height);

                    Rect removeButtonPosition = new Rect(rect.width + 16, rect.y, deleteButton.image.width, deleteButton.image.height);

                    if (isPro)
                    {

                        if (CGFGUIHelper.IconButton(addButtonPosition, addButtonProfessional, addPressedButtonProfessional))
                        {

                            Add(index, property);
                        }

                        EditorGUI.LabelField(addButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);

                        if (CGFGUIHelper.IconButton(removeButtonPosition, deleteButtonProfessional, deleteButtonProfessional))
                        {

                            Delete(index, property);

                        }

                        EditorGUI.LabelField(removeButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);
                    }
                    else
                    {

                        if (CGFGUIHelper.IconButton(addButtonPosition, addButton, addPressedButton))
                        {

                            Add(index, property);

                        }

                        EditorGUI.LabelField(addButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);

                        if (CGFGUIHelper.IconButton(removeButtonPosition, deleteButton, deletePressedButton))
                        {

                            Delete(index, property);
                        }

                        EditorGUI.LabelField(removeButtonPosition, CGFGUIHelper._temporalIconContentProfessional, style2);
                    }


                }

                property.serializedObject.ApplyModifiedProperties();
            };

            if (vertical)
            {
                list.elementHeightCallback = (index) =>
                {

                    return ((EditorGUIUtility.singleLineHeight + 3) * (properties.Length + 1));

                };
            }

            list.DoLayoutList();

            GUI.enabled = true;

            return list;
        }

        /// \english
        /// <summary>
        /// Utility button to clone the selected element in the ReorderableList.
        /// </summary>
        /// <param name="index"> Element position in the Reorderable List.</param>
        /// <param name="property"> Property that contains the element.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Botón extra para clonar un elemento concreto de la lista.
        /// </summary>
        /// <param name="index">Posición del elemento a clonar.</param>
        /// <param name="property">Propiedad que contiene el elemento.</param>
        /// \endspanish
        private static void Add(int index, SerializedProperty property)
        {
            if (property != null)
            {
                property.InsertArrayElementAtIndex(index);

                property.serializedObject.ApplyModifiedProperties();
            }
        }

        /// \english
        /// <summary>
        /// Utility button to remove the selected element in the ReorderableList
        /// </summary>
        /// <param name="index"> Element position in the Reorderable List.</param>
        /// <param name="property"> Property that contains the element.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Elimina el elemento de la lista seleccionado.
        /// </summary>
        /// <param name="index">Posición del elemento a elminar.</param>
        /// <param name="property">Propiedad que contiene el elemento.</param>
        /// \endspanish
        private static void Delete(int index, SerializedProperty property)
        {
            if (property != null)
            {
                property.DeleteArrayElementAtIndex(index);

                property.serializedObject.ApplyModifiedProperties();
            }
        }

        #endregion

        #region Property

        /// \english
        /// <summary>
        /// Generic property builder.
        /// </summary>
        /// <param name="property">Property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una propiedad genérica.
        /// </summary>
        /// <param name="property">Propiedad a construir.</param>
        /// \endspanish
        public static void BuildProperty(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property, true);
        }

        /// \english
        /// <summary>
        /// Generic property builder with locking.
        /// </summary>
        /// <param name="property">Property to build.</param>
        /// <param name="enumLocker">Enum that locks the property.</param>
        /// <param name="enumValues">Enum value to unlock the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una propiedad genérica con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="enumLocker">Enumeración que bloquea la propiedad</param>
        /// <param name="enumValues">Valor de la enumaración que desbloquea la propiedad.</param>
        /// \endspanish
        public static void BuildProperty(SerializedProperty property, SerializedProperty enumLocker, params int[] enumValues)
        {
            foreach (int value in enumValues)
            {
                if (enumLocker.enumValueIndex == value)
                {
                    GUI.enabled = true;
                    break;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUILayout.PropertyField(property, true);

            GUI.enabled = true;
        }

        /// \english
        /// <summary>
        /// Generic property builder with locking.
        /// </summary>
        /// <param name="property">Property to build.</param>
        /// <param name="boolLocker">Bool that locks the property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de una propiedad genérica con opción a bloqueo.
        /// </summary>
        /// <param name="property">Propiedad a construir.</param>
        /// <param name="boolLocker">Booleana que bloquea la propiedad</param>
        /// \endspanish
        public static void BuildProperty(SerializedProperty property, bool boolLocker)
        {
            if (boolLocker)
            {
                GUI.enabled = true;
            }
            else
            {
                GUI.enabled = false;
            }

            EditorGUILayout.PropertyField(property, true);

            GUI.enabled = true;
        }



        #endregion 

        #region Axis

        #endregion

        #endregion


        #endregion
    }


    /// \english
    /// <summary>
    /// Set of functionalities to create buttons in the Editor UI.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Funcionalidades para crear botones en la UI del editor.
    /// </summary>
    /// \endspanish
    [ExecuteInEditMode]
    public class CGFGUIHelper
    {
        #region Public Variables

        /// \english
        /// <summary>
        /// GuiContent Unity Personal Button.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Botón GuiContent Unity Personal.
        /// </summary>
        /// \endspanish
        public static GUIContent _temporalIconContent = new GUIContent();

        /// \english
        /// <summary>
        /// GuiContent Unity Professional Button.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Botón GuiContent Unity Professional.
        /// </summary>
        /// \endspanish
        public static GUIContent _temporalIconContentProfessional = new GUIContent();

        #endregion


        #region Private Variables

        /// \english
        /// <summary>
        /// Rectanle area to build the separator.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Área rectangulo visible del botón a construir.
        /// </summary>
        /// \endspanish
        private static Func<Rect> VisibleRect;

        /// \english
        /// <summary>
        /// GUI Style.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Estilo de GUI local.
        /// </summary>
        /// \endspanish
        private static GUIStyle _temporalStyle = new GUIStyle();

        /// \english
        /// <summary>
        /// Integer button identifier.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Valor entero identificador del icono del botón a construir.
        /// </summary>
        /// \endspanish
        private static readonly int _iconButtonHint = "_ReorderableIconButton_".GetHashCode();

        /// \english
        /// <summary>
        /// Separator space color.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Color del espacio separador.
        /// </summary>
        /// \endspanish
        private static readonly Color _separatorColor = EditorGUIUtility.isProSkin ? new Color(0.11f, 0.11f, 0.11f) : new Color(0.5f, 0.5f, 0.5f);

        /// \english
        /// <summary>
        /// Separator GUI Style.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Estilo de GUI local para el separador.
        /// </summary>
        /// \endspanish
        private static readonly GUIStyle _separatorStyle = new GUIStyle();

        #endregion


        #region Utility Methods

        /// \english
        /// <summary>
        /// Draws the selected texture
        /// </summary>
        /// <param name="position"> Texture position.</param>
        /// <param name="texture"> Texture to draw.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Dibuja la textura seleccionada.
        /// </summary>
        /// <param name="position">Posición donde dibujar la textura.</param>
        /// <param name="texture">Textura a dibujar.</param>
        /// \endspanish
        public static void DrawTexture(Rect position, Texture2D texture)
        {

            if (Event.current.type != EventType.Repaint)
            {

                return;

            }

            _temporalStyle.normal.background = texture;

            _temporalStyle.Draw(position, GUIContent.none, false, false, false, false);

        }

        /// \english
        /// <summary>
        /// Create an UI Button icon.
        /// </summary>
        /// <param name="position"> Button position.</param>
        /// <param name="visible"> Is the icon visible?.</param>
        /// <param name="iconNormal"> Normal status button image.</param>
        /// <param name="iconActive"> Pressed status button image.</param>
        /// <returns> Result boolean.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Crea el Icono del Botón seleccionado.
        /// </summary>
        /// <param name="position">Posición donde crear el botón.</param>
        /// <param name="visible">Es el botón visible?.</param>
        /// <param name="iconNormal">Icono para el estado normal del botón.</param>
        /// <param name="iconActive">Icono para el estado activo del botón.</param>
        /// <returns>Booleana de resultado.</returns>
        /// \endspanish
        public static bool IconButton(Rect position, bool visible, GUIContent iconNormal, GUIContent iconActive)
        {

            int controlID = GUIUtility.GetControlID(_iconButtonHint, FocusType.Passive);

            bool result = false;

            position.height += 1;

            switch (Event.current.GetTypeForControl(controlID))
            {

                case EventType.MouseDown:

                    if (GUI.enabled && Event.current.button != 1 && position.Contains(Event.current.mousePosition))
                    {

                        GUIUtility.hotControl = controlID;

                        GUIUtility.keyboardControl = 0;

                        Event.current.Use();

                    }

                    break;

                case EventType.MouseDrag:

                    if (GUIUtility.hotControl == controlID)
                    {

                        Event.current.Use();

                    }

                    break;

                case EventType.MouseUp:

                    if (GUIUtility.hotControl == controlID)
                    {

                        GUIUtility.hotControl = 0;

                        result = position.Contains(Event.current.mousePosition);

                        Event.current.Use();

                    }

                    break;

                case EventType.Repaint:

                    if (visible)
                    {

                        bool isActive = GUIUtility.hotControl == controlID &&
                                        position.Contains(Event.current.mousePosition);

                        _temporalIconContentProfessional = isActive ? iconActive : iconNormal;

                        position.height -= 1;

                    }

                    break;

            }

            return result;

        }

        /// \english
        /// <summary>
        /// Create an UI Button icon.
        /// </summary>
        /// <param name="position"> Button position.</param>
        /// <param name="visible"> Is the icon visible?.</param>
        /// <param name="iconNormal"> Normal status button image.</param>
        /// <param name="iconActive"> Pressed status button image.</param>
        /// <returns> Result boolean.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Crea el Icono del Botón seleccionado.
        /// </summary>
        /// <param name="position">Posición donde crear el botón.</param>
        /// <param name="visible">Es el botón visible?.</param>
        /// <param name="iconNormal">Icono para el estado normal del botón.</param>
        /// <param name="iconActive">Icono para el estado activo del botón.</param>
        /// <returns>Booleana de resultado.</returns>
        /// \endspanish
        public static bool IconButton2(Rect position, bool visible, GUIContent iconNormal, GUIContent iconActive)
        {

            int controlID = GUIUtility.GetControlID(_iconButtonHint, FocusType.Passive);

            bool result = false;

            position.height += 1;

            switch (Event.current.GetTypeForControl(controlID))
            {

                case EventType.MouseDown:

                    if (GUI.enabled && Event.current.button != 1 && position.Contains(Event.current.mousePosition))
                    {

                        GUIUtility.hotControl = controlID;

                        GUIUtility.keyboardControl = 0;

                        Event.current.Use();

                    }

                    break;

                case EventType.MouseDrag:

                    if (GUIUtility.hotControl == controlID)
                    {

                        Event.current.Use();

                    }

                    break;

                case EventType.MouseUp:

                    if (GUIUtility.hotControl == controlID)
                    {

                        GUIUtility.hotControl = 0;

                        result = position.Contains(Event.current.mousePosition);

                        Event.current.Use();

                    }

                    break;

                case EventType.Repaint:

                    if (visible)
                    {

                        bool isActive = GUIUtility.hotControl == controlID && position.Contains(Event.current.mousePosition);

                        _temporalIconContent = isActive ? iconActive : iconNormal;

                        position.height -= 1;

                    }

                    break;

            }

            return result;

        }

        /// \english
        /// <summary>
        /// Create an UI Button.
        /// </summary>
        /// <param name="position"> Button position.</param>
        /// <param name="iconNormal"> Normal status button image.</param>
        /// <param name="iconActive"> Pressed status button image.</param>
        /// <returns> Boolean result.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Crea el Icono del Botón seleccionado.
        /// </summary>
        /// <param name="position">Posición donde crear el botón.</param>
        /// <param name="iconNormal">Icono para el estado normal del botón.</param>
        /// <param name="iconActive">Icono para el estado activo del botón.</param>
        /// <returns>Booleana de resultado.</returns>
        /// \endspanish
        public static bool IconButton(Rect position, GUIContent iconNormal, GUIContent iconActive)
        {

            return IconButton(position, true, iconNormal, iconActive);

        }

        /// \english
        /// <summary>
        /// Create an UI Button.
        /// </summary>
        /// <param name="position"> Button position.</param>
        /// <param name="iconNormal"> Normal status button image.</param>
        /// <param name="iconActive"> Pressed status button image.</param>
        /// <returns> Boolean result.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Crea el Icono del Botón seleccionado.
        /// </summary>
        /// <param name="position">Posición donde crear el botón.</param>
        /// <param name="iconNormal">Icono para el estado normal del botón.</param>
        /// <param name="iconActive">Icono para el estado activo del botón.</param>
        /// <returns> Booleana de resultado.</returns>
        /// \endspanish
        public static bool IconButton2(Rect position, GUIContent iconNormal, GUIContent iconActive)
        {

            return IconButton2(position, true, iconNormal, iconActive);

        }

        /// \english
        /// <summary>
        /// Creates a separator.
        /// </summary>
        /// <param name="position"> Separator position.</param>
        /// <param name="color"> Separator Color.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Crea un separador.
        /// </summary>
        /// <param name="position">Posición donde crear el separador.</param>
        /// <param name="color">Color del separador.</param>
        /// \endspanish
        public static void Separator(Rect position, Color color)
        {

            if (Event.current.type == EventType.Repaint)
            {

                Color restoreColor = GUI.color;

                GUI.color = color;

                _separatorStyle.Draw(position, false, false, false, false);

                GUI.color = restoreColor;

            }

        }

        /// \english
        /// <summary>
        /// Creates a separator.
        /// </summary>
        /// <param name="position"> Separator position.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Crea un separador.
        /// </summary>
        /// <param name="position">Posición donde crear el separador.</param>
        /// \endspanish
        public static void Separator(Rect position)
        {

            Separator(position, _separatorColor);

        }

        #endregion
    }

    /// \english
    /// <summary>
    /// Manages the component copy-paste.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Gestión del copiado y pegado de componentes.
    /// </summary>
    /// \endspanish
    [ExecuteInEditMode]
    public class CGFComponentCopier : UnityEditor.Editor
    {
        #region Private Variables

        /// \english
        /// <summary>
        /// Copied Component.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// /// <summary>
        /// Componente copiado.
        /// </summary>
        /// \endspanish
        private static Component copiedComponent;

        #endregion


        #region Utility Methods

        /// \english
        /// <summary>
        /// Adds to the Gameobject a new component with the copied values.
        /// </summary>
        /// <typeparam name="T"> New compoennt type.</typeparam>
        /// \endenglish
        /// \spanish
        /// /// <summary>
        /// Añade al gameobject el comoponente copiado como si fuera un nuevo comoponente.
        /// </summary>
        /// <typeparam name="T">Tipo del componente a pegar como nuevo componente.</typeparam>
        /// \endspanish
        public static void PasteAsNew<T>() where T : Component
        {

            copiedComponent = Selection.activeGameObject.GetComponent<T>();

            foreach (var targetGameObject in Selection.gameObjects)
            {

                if (!targetGameObject || copiedComponent == null) continue;
                {

                    if (!copiedComponent) continue;

                    ComponentUtility.CopyComponent(copiedComponent);

                    ComponentUtility.PasteComponentAsNew(targetGameObject);

                }

            }

        }

        /// \english
        /// <summary>
        /// Paste the copied component values.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// /// <summary>
        /// Pega los valores del componente copiado.
        /// </summary>
        /// \endspanish
        public static void Paste()
        {

            foreach (var targetGameObject in Selection.gameObjects)
            {

                if (!targetGameObject || copiedComponent == null) continue;
                {

                    if (!copiedComponent) continue;

                    ComponentUtility.PasteComponentValues(copiedComponent);

                }

            }

        }

        #endregion

    }


    /// \english
    /// <summary>
    /// Manages the Component Backup Information.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Gestión de la copia de seguridad de componentes.
    /// </summary>
    /// \endspanish
    [System.Serializable]
    public class CGFComponentBackup
    {
        #region Public Variables

        /// \english
        /// <summary>
        /// Backup Identification.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombre de referencia.
        /// </summary>
        /// \endspanish
        public string iD;

        /// \english
        /// <summary>
        /// Backup information.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Copia de seguridad del componente.
        /// </summary>
        /// \endspanish
        public string information;

        /// \english
        /// <summary>
        /// Backup date.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Hora y fecha de la copia de seguridad del componente.
        /// </summary>
        /// \endspanish
        public string date;

        /// \english
        /// <summary>
        /// Backup component type.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Tipo del componente de la copia de seguridad.
        /// </summary>
        /// \endspanish
        public Type t;

        #endregion
    }

}
 