///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 22/05/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Permite buscar campos de una enumeración en el inspector con un popup.
///

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Local Namespace
namespace Assets.CGF.Editor
{

	/// \english
    /// <summary>
    /// Class that stores the enumeration data.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Clase que almacena datos de la enumeración.
    /// </summary>
    /// \endspanish
    public class SingleEnumName
    {

		/// \english
		/// <summary>
		/// Name.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Nombre.
        /// </summary>
		/// \endspanish
        public string name;

		/// \english
		/// <summary>
		/// Index.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Índice.
        /// </summary>
		/// \endspanish
        public int index;

		/// \english
		/// <summary>
		/// Value.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Valor.
        /// </summary>
		/// \endspanish
        public int value;

		/// \english
		/// <summary>
		/// If is selected or unselected.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Si esta seleccionado o no.
        /// </summary>
		/// \endspanish
        public bool isSelect;

    }

	/// \english
    /// <summary>
    /// Enumeration to define the sort method.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Enumeración para definir el método de ordenado.
    /// </summary>
    /// \endspanish
    public enum CustomEnumPopType
    {

		/// \english
		/// <summary>
		/// Alphabetically sorting.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Ordenado alfabéticamente.
        /// </summary>
		/// \endspanish
        Alphabet = 0,

		/// \english
		/// <summary>
		/// Origin enumeration sorting.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Ordenado como en el enumerador de origen.
        /// </summary>
		/// \endspanish
        Enum = 1,

    }

	/// \english
    /// <summary>
    /// Allows to search fields in a enumeration from inspector with a popup.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Permite buscar campos de una enumeración en el inspector con un popup.
    /// </summary>
    /// \endspanish
    public class CGFCustomEnumPopupWindow : EditorWindow
    {

        #region Public Variables

        #endregion


        #region Private Variables

		/// \english
		/// <summary>
		/// Window position.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Posición de la ventana.
        /// </summary>
		/// \endspanish
        private Vector2 windowPosition;

		/// \english
		/// <summary>
		/// Action to be executed when exists a selection.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Acción a realizar cuando hay una selección.
        /// </summary>
		/// \endspanish
        private Action<object> action;

		/// \english
		/// <summary>
		/// Property to modify.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Propiedad a modificar.
        /// </summary>
		/// \endspanish
        private SerializedProperty serializedProperty;

		/// \english
		/// <summary>
		/// Object to modify.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Objeto a modificar.
        /// </summary>
		/// \endspanish
        private object obj;

		/// \english
		/// <summary>
		/// SingleEnumName list.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Lista de SingleEnumName.
        /// </summary>
		/// \endspanish
        private List<SingleEnumName> enumNames = new List<SingleEnumName>();

		/// \english
		/// <summary>
		/// Text style.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo del texto.
        /// </summary>
		/// \endspanish
        private GUIStyle textStyle;

		/// \english
		/// <summary>
		/// Background style of the selected property.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo del fondo de la propiedad seleccionada.
        /// </summary>
		/// \endspanish
        private GUIStyle selectedBackgroundStyle;

		/// \english
		/// <summary>
		/// Background style of the unselected property.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo del fondo de la propiedad no seleccionada.
        /// </summary>
		/// \endspanish
        private GUIStyle normalBackgroundStyle;

		/// \english
		/// <summary>
		/// Checks if the styles has been initialized.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Comprobación de si los estilos se han inicializado.
        /// </summary>
		/// \endspanish
        private bool isInitedStype = false;

		/// \english
		/// <summary>
		/// Checks if the windows is selected.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Comprobación de si la ventana está seleccionada.
        /// </summary>
		/// \endspanish
        private bool isSelected = false;

		/// \english
		/// <summary>
		/// Estilo de la herramienta de búsqueda.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo de la herramienta de búsqueda.
        /// </summary>
		/// \endspanish
        private GUIStyle searchToobar;

		/// \english
		/// <summary>
		/// Search text.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Texto de la búsqueda.
        /// </summary>
		/// \endspanish
        private string searchText = string.Empty;

		/// \english
		/// <summary>
		/// Sorting method.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Método de ordenación.
        /// </summary>
		/// \endspanish
        private CustomEnumPopType type;

		/// \english
		/// <summary>
		/// Checks if the values have been showing.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Comprueba si se están monstrando los valores.
        /// </summary>
		/// \endspanish
        private bool isShowValue = false;

		/// \english
		/// <summary>
		/// Selected SingleEnumName.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// SingleEnumName seleccionada.
        /// </summary>
		/// \endspanish
        private SingleEnumName selectSingle = null;

		/// \english
		/// <summary>
		/// 
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo fondo de la selección Unity Pro.
        /// </summary>
		/// \endspanish
        private const string s_SelectedBg_Pro = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQklEQVRIDe3SsQkAAAgDQXWN7L+nOMFXdm8dIhzpJPV581l+3T5AYYkkQgEMuCKJUAADrkgiFMCAK5IIBTDgipBoAWXpAJEoZnl3AAAAAElFTkSuQmCC";

		/// \english
		/// <summary>
		/// Selection hightlight style in Uity Pro.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo del del resaltado de la selección en Unity Pro.
        /// </summary>
		/// \endspanish
        private const string s_HightLightBg_Pro = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQklEQVRIDe3SsQkAAAgDQXXD7L+MOMFXdm8dIhzpJPV581l+3T5AYYkkQgEMuCKJUAADrkgiFMCAK5IIBTDgipBoARFdATMHrayuAAAAAElFTkSuQmCC";
        
		/// \english
		/// <summary>
		/// Selection background style.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo del fondo de la selección.
        /// </summary>
		/// \endspanish
        private const string s_SelectedBg_Light = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQUlEQVRIDe3SsQkAAAgDQXV/yMriBF/ZvXWIcKST1OfNZ/l1+wCFJZIIBTDgiiRCAQy4IolQAAOuSCIUwIArQqIF36EB7diYDg8AAAAASUVORK5CYII=";
        
		/// \english
		/// <summary>
		/// Hightlight style.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo resaltado.
        /// </summary>
		/// \endspanish
        private const string s_HightLightBg_Light = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQklEQVRIDe3SsQkAAAgDQXX/ETOMOMFXdm8dIhzpJPV581l+3T5AYYkkQgEMuCKJUAADrkgiFMCAK5IIBTDgipBoAc9YAtQLJ3kPAAAAAElFTkSuQmCC";

        #endregion


        #region Main Methods

        void OnGUI()
        {

            InitializationTextStyle();

            GUILayout.BeginHorizontal(EditorStyles.toolbar);

            GUI.backgroundColor = new Color(1f, 1f, 1f, 0.5f);

            GUI.SetNextControlName("Search");

            searchText = EditorGUILayout.TextField("", searchText, searchToobar, GUILayout.MinWidth(95));

            EditorGUI.FocusTextInControl("Search");

            if (this.type == CustomEnumPopType.Alphabet)
            {

                if (GUILayout.Button("E", EditorStyles.toolbarButton, GUILayout.Width(16)))
                {

                    enumNames.Sort((e1, e2) => { return e1.value.CompareTo(e2.value); });

                    int realIndex;

                    if (null == selectSingle)
                    {

                        realIndex = 0;

                    }
                    else
                    {

                        realIndex = enumNames.IndexOf(selectSingle);

                    }

                    windowPosition.y = 16 * realIndex;

                    this.type = CustomEnumPopType.Enum;
                }

            }
            else if (this.type == CustomEnumPopType.Enum)
            {

                if (GUILayout.Button("A", EditorStyles.toolbarButton, GUILayout.Width(16)))
                {

                    enumNames.Sort((e1, e2) => { return e1.name.CompareTo(e2.name); });

                    int realIndex;

                    if (null == selectSingle)
                    {

                        realIndex = 0;

                    }
                    else
                    {

                        realIndex = enumNames.IndexOf(selectSingle);

                    }

                    windowPosition.y = 16 * realIndex;

                    this.type = CustomEnumPopType.Alphabet;

                }

            }

            isShowValue = GUILayout.Toggle(isShowValue, "V", EditorStyles.toolbarButton, GUILayout.Width(16));

            GUILayout.EndHorizontal();

            GUI.backgroundColor = Color.white;

            windowPosition = EditorGUILayout.BeginScrollView(windowPosition);

            for (int i = 0; i < enumNames.Count; i++)
            {

                SingleEnumName single = enumNames[i];

                if (!string.IsNullOrEmpty(searchText) && !single.name.ToLower().Contains(searchText))
                {

                    continue;

                }

                Rect rect;

                if (single.isSelect)
                {

                    rect = EditorGUILayout.BeginHorizontal(selectedBackgroundStyle);

                }
                else
                {

                    rect = EditorGUILayout.BeginHorizontal(normalBackgroundStyle);

                }

                GUILayout.Label(single.name, textStyle);

                GUILayout.FlexibleSpace();

                if (isShowValue)
                {

                    GUILayout.Label(single.value.ToString(), textStyle);

                }

                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {

                    if (serializedProperty != null)
                    {

                        serializedProperty.enumValueIndex = single.index;

                        serializedProperty.serializedObject.ApplyModifiedProperties();

                    }
                    else if (obj != null)
                    {

                        obj = Enum.Parse(CGFEditorUtilitiesClass.GetType(serializedProperty), single.name);

                        action.Invoke(obj);

                    }

                    isSelected = true;
                }

                EditorGUILayout.EndHorizontal();

            }

            EditorGUILayout.EndScrollView();

            if (isSelected)
            {

                isSelected = false;

                Close();

            }

        }

        void Update()
        {

            Repaint();

        }

        #endregion


        #region Utility Methods

		/// \english
		/// <summary>
		/// Initializes the editor window.
		/// </summary>
		/// <param name="serializedProperty">Property to modify.</param>
        /// <param name="type">Sorting method.</param>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Inicializa la ventana de editor.
        /// </summary>
		/// <param name="serializedProperty">Propiedad a modificar.</param>
        /// <param name="type">Método de ordenación.</param>
		/// \endspanish
        public void Initialization(SerializedProperty serializedProperty, CustomEnumPopType type)
        {

            enumNames.Clear();

            this.serializedProperty = serializedProperty;

            this.type = type;

            isShowValue = false;

            string[] names = serializedProperty.enumNames;

            List<string> namesList = new List<string>();

            foreach (var m_name in names)
            {

                namesList.Add(m_name);

            }

            namesList.Sort((n1, n2) => { return ((int)System.Enum.Parse(CGFEditorUtilitiesClass.GetType(serializedProperty), n1)).CompareTo((int)System.Enum.Parse(CGFEditorUtilitiesClass.GetType(serializedProperty), n2)); });

            names = namesList.ToArray();

            selectSingle = null;

            for (int i = 0; i < names.Length; i++)
            {

                string name = names[i];

                SingleEnumName single = new SingleEnumName();

                single.name = name;

                single.index = i;

                single.value = (int)System.Enum.Parse(CGFEditorUtilitiesClass.GetType(serializedProperty), name);

                if (i == serializedProperty.enumValueIndex)
                {

                    single.isSelect = true;

                    selectSingle = single;

                }
                else
                {

                    single.isSelect = false;

                }

                enumNames.Add(single);

            }

            if (this.type == CustomEnumPopType.Alphabet)
            {

                enumNames.Sort((e1, e2) => { return e1.name.CompareTo(e2.name); });

            }
            else
            {

                enumNames.Sort((e1, e2) => { return e1.value.CompareTo(e2.value); });

            }

            int realIndex;

            if (null == selectSingle)
            {

                realIndex = 0;

            }
            else
            {

                realIndex = enumNames.IndexOf(selectSingle);

            }

            windowPosition.y = 16 * realIndex;

            isSelected = false;

            var t = typeof(EditorStyles);

            var property = t.GetProperty("toolbarSearchField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            searchToobar = property.GetValue(null, null) as GUIStyle;

            searchText = string.Empty;

        }

		/// \english
		/// <summary>
		/// Initializes the editor window.
		/// </summary>
		/// <param name="serializedProperty">Property to modify.</param>
        /// <param name="type">Sorting method.</param>
		/// <param name="action">Action to be executed when exists a selection.</param>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Inicializa la ventana de editor.
        /// </summary>
		/// <param name="serializedProperty">Propiedad a modificar.</param>
        /// <param name="type">Método de ordenación.</param>
		/// <param name="action">Acción a realizar cuando haya una selección.</param>
		/// \endspanish
        public void Initialization(object obj, CustomEnumPopType type, Action<object> action)
        {

            enumNames.Clear();

            this.action = action;

            this.obj = obj;

            this.type = type;

            isShowValue = false;

            string[] names = Enum.GetNames(CGFEditorUtilitiesClass.GetType(serializedProperty));

            List<string> namesList = new List<string>();

            foreach (var m_name in names)
            {

                namesList.Add(m_name);

            }

            namesList.Sort((n1, n2) => { return ((int)Enum.Parse(CGFEditorUtilitiesClass.GetType(serializedProperty), n1)).CompareTo((int)Enum.Parse(CGFEditorUtilitiesClass.GetType(serializedProperty), n2)); });

            names = namesList.ToArray();

            selectSingle = null;

            for (int i = 0; i < names.Length; i++)
            {

                string name = names[i];

                SingleEnumName single = new SingleEnumName();

                single.name = name;

                single.index = i;

                single.value = (int)System.Enum.Parse(CGFEditorUtilitiesClass.GetType(serializedProperty), name);

                if (serializedProperty != null)
                {

                    if (i == serializedProperty.enumValueIndex)
                    {

                        single.isSelect = true;

                        selectSingle = single;

                    }
                    else
                    {

                        single.isSelect = false;

                    }

                }
                else if (obj != null)
                {

                    if (i == Array.IndexOf(Enum.GetValues(CGFEditorUtilitiesClass.GetType(serializedProperty)), obj))
                    {

                        single.isSelect = true;

                        selectSingle = single;

                    }
                    else
                    {

                        single.isSelect = false;

                    }

                }

                enumNames.Add(single);

            }

            if (this.type == CustomEnumPopType.Alphabet)
            {

                enumNames.Sort((e1, e2) => { return e1.name.CompareTo(e2.name); });

            }
            else
            {

                enumNames.Sort((e1, e2) => { return e1.value.CompareTo(e2.value); });

            }

            int realIndex;

            if (null == selectSingle)
            {

                realIndex = 0;

            }
            else
            {

                realIndex = enumNames.IndexOf(selectSingle);

            }

            windowPosition.y = 16 * realIndex;

            isSelected = false;

            var t = typeof(EditorStyles);

            var property = t.GetProperty("toolbarSearchField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            searchToobar = property.GetValue(null, null) as GUIStyle;

            searchText = string.Empty;

        }

		/// \english
		/// <summary>
		/// Initializes the styles of the window text.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Inicializa los estilos del texto de la ventana.
        /// </summary>
		/// \endspanish
        void InitializationTextStyle()
        {

            if (!isInitedStype)
            {

                textStyle = new GUIStyle(EditorStyles.label)
                {
                    fixedHeight = 16,

                    alignment = TextAnchor.MiddleLeft
                };

                Texture2D selectedBg = new Texture2D(32, 32, TextureFormat.RGB24, false);

                Texture2D hightLightBg = new Texture2D(32, 32, TextureFormat.RGB24, false);

                if (EditorGUIUtility.isProSkin)
                {

                    selectedBg.LoadImage(Convert.FromBase64String(s_SelectedBg_Pro));

                    hightLightBg.LoadImage(Convert.FromBase64String(s_HightLightBg_Pro));

                }
                else
                {

                    selectedBg.LoadImage(Convert.FromBase64String(s_SelectedBg_Light));

                    hightLightBg.LoadImage(Convert.FromBase64String(s_HightLightBg_Light));

                }

                selectedBackgroundStyle = new GUIStyle();

                selectedBackgroundStyle.normal.background = selectedBg;

                normalBackgroundStyle = new GUIStyle();

                normalBackgroundStyle.hover.background = hightLightBg;

                isInitedStype = true;

            }

        }

        #endregion


        #region Utility Events

        #endregion

    }

}