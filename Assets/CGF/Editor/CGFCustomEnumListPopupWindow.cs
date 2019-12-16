///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 22/05/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Allows to search fields in a list from inspector with a popup.
///

using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Assets.CGF.Editor
{

	/// \english
    /// <summary>
    /// Allows to search fields in a list from inspector with a popup.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Permite buscar campos de una lista desde el inspector con un popup.
    /// </summary>
    /// \endspanish
    public class CGFCustomEnumListPopupWindow : EditorWindow
    {

        #region Public Variables

        #endregion


        #region Private Variables

		/// \english
		/// <summary>
		/// Selection background style in Uity Pro.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo del fondo de la selección en Unity Pro.
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
		/// Selection hightlight style.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Estilo del resaltado de la selección.
        /// </summary>
		/// \endspanish
        private const string s_HightLightBg_Light = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAIAAAD8GO2jAAAAQklEQVRIDe3SsQkAAAgDQXX/ETOMOMFXdm8dIhzpJPV581l+3T5AYYkkQgEMuCKJUAADrkgiFMCAK5IIBTDgipBoAc9YAtQLJ3kPAAAAAElFTkSuQmCC";

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
		/// Propertie to modify.
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
		/// Style of the search tool.
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
		/// Selected SingleEnumName.
		/// </summary>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// SingleEnumName seleccionada.
        /// </summary>
		/// \endspanish
        private SingleEnumName selectSingle = null;

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

                if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.MouseDown)
                {

                    serializedProperty.stringValue = single.name;

                    serializedProperty.serializedObject.ApplyModifiedProperties();

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
		/// <param name="serializedProperty">Propertie to modify.</param>
        /// <param name="namesList">Name list to show.</param>
		/// \endenglish
		/// \spanish
		/// <summary>
        /// Inicializa la ventana de editor.
        /// </summary>
        /// <param name="serializedProperty">Propiedad a modificar.</param>
        /// <param name="namesList">Lista de nombre a mostrar.</param>
		/// \endspanish
        public void Initialization(SerializedProperty serializedProperty, List<string> namesList)
        {

            enumNames.Clear();

            this.serializedProperty = serializedProperty;

            string[] names = namesList.ToArray();

            selectSingle = null;

            for (int i = 0; i < names.Length; i++)
            {

                string name = names[i];

                SingleEnumName single = new SingleEnumName
                {
                    name = name,
                    index = i
                };

                if (names[i] == serializedProperty.stringValue)
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

            isSelected = false;

            var t = typeof(EditorStyles);

            var property = t.GetProperty("toolbarSearchField", BindingFlags.NonPublic | BindingFlags.Static);

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

                    selectedBg.LoadImage(System.Convert.FromBase64String(s_SelectedBg_Pro));

                    hightLightBg.LoadImage(System.Convert.FromBase64String(s_HightLightBg_Pro));

                }
                else
                {

                    selectedBg.LoadImage(System.Convert.FromBase64String(s_SelectedBg_Light));

                    hightLightBg.LoadImage(System.Convert.FromBase64String(s_HightLightBg_Light));

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