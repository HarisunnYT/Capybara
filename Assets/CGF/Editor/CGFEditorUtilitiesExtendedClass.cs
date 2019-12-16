///
/// INFORMATION
/// 
/// Project: Chloroplast Games Framework
/// Game: Chloroplast Games Framework
/// Date: 19/03/2018
/// Author: Chloroplast Games
/// Website: http://www.chloroplastgames.com
/// Programmers: David Cuenca
/// Description: Class that extends the utility and functionality of CGFEditorUtilitiesClass.
///

using System;
using UnityEditor;
using UnityEngine;

namespace Assets.CGF.Editor
{

    /// \english
    /// <summary>
    /// Class that extends the utility and functionality of CGFEditorUtilitiesClass.
    /// </summary>
    /// \endenglish
    /// \spanish
    /// <summary>
    /// Clase que extiende las utilidades y funcionalidades de CGFEditorUtilitiesClass.
    /// </summary>
    /// \endspanish
    public static class CGFEditorUtilitiesExtendedClass
    {

        #region Public Variables

        #endregion


        #region Private Variables

        /// \english
        /// <summary>
        /// Blending factors.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Factores de fusión.
        /// </summary>
        /// \endspanish
        public enum BlendFactor
        {

            Zero,

            One,

            DstColor,

            SrcColor,

            OneMinusDstColor,

            SrcAlpha,

            OneMinusSrcColor,

            DstAlpha,

            OneMinusDstAlpha,

            SrcAlphaSaturate,

            OneMinusSrcAlpha,

        }

        /// \english
        /// <summary>
        /// Selected blending factor.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Factor de fusión seleccionado.
        /// </summary>
        /// \endspanish
        private static BlendFactor blendEnumFactor;

        /// \english
        /// <summary>
        /// Blending factor names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los factores de fusión.
        /// </summary>
        /// \endspanish
        private static readonly GUIContent[] blendFactorNames = GetEnumNames(blendEnumFactor);

        /// \english
        /// <summary>
        /// Blending type.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Tipos de fusión.
        /// </summary>
        /// \endspanish
        public enum BlendType
        {

            Custom,

            AlphaBlend,

            Premultiplied,

            Additive,

            SoftAdditive,

            Multiplicative,

            DoubleMultiplicative,

            ParticleAdditive,

            ParticleBlend

        }

        /// \english
        /// <summary>
        /// Selected blending type.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Tipo de fusión seleccionado.
        /// </summary>
        /// \endspanish
        private static BlendType blendEnumType;

        /// \english
        /// <summary>
        /// Blending factor names.
        /// </summary>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Nombres de los factores de fusión.
        /// </summary>
        /// \endspanish
        private static readonly GUIContent[] blendTypeNames = GetEnumNames(blendEnumType);

        #endregion


        #region Main Methods

        #endregion


        #region Utility Methods

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

            GUIContent[] content = new GUIContent[names.Length]; ;

            for (int i = 0; i < names.Length; i++)
            {

                content[i] = new GUIContent(names[i]);

            }

            return content;

        }

        /// \english
        /// <summary>
        /// Blending type enumeration builder.
        /// </summary>
        /// <param name="propertyBlendType">Property that manages the blending type.</param>
        /// <param name="propertyBlendSource">Property that manages the blending source.</param>
        /// <param name="propertyBlendDestination">Property that manages the blending destination.</param>
        /// <param name="myMaterial">Material in use.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de la enumeración de selección de tipo de fusión.
        /// </summary>
        /// <param name="propertyBlendType">Propiedad que gestiona el tipo de fusión.</param>
        /// <param name="propertyBlendSource">Propiedad que gestiona la fuente de la fusión.</param>
        /// <param name="propertyBlendDestination">Propiedad que gestiona el destinación de la fusión.</param>
        /// <param name="myMaterial">Material en uso.</param>
        /// \endspanish
        public static void BuildBlendTypeEnum(SerializedProperty propertyBlendType, SerializedProperty propertyBlendSource, SerializedProperty propertyBlendDestination, SerializedProperty myMaterial)
        {

            BlendType blendType = (BlendType)propertyBlendType.floatValue;

            BlendFactor blendFactorSource = (BlendFactor)propertyBlendSource.floatValue;

            BlendFactor blendFactorDestination = (BlendFactor)propertyBlendDestination.floatValue;

            EditorGUI.BeginChangeCheck();

            blendType = (BlendType)EditorGUILayout.Popup(new GUIContent("Blend Type", "Blending type."), (int)blendType, blendTypeNames);

            if (blendType == 0)
            {

                GUI.enabled = true;
            }
            else
            {

                GUI.enabled = false;

            }

            blendFactorSource = (BlendFactor)EditorGUILayout.Popup(new GUIContent("Source Factor", "Blending source factor."), (int)blendFactorSource, blendFactorNames);

            blendFactorDestination = (BlendFactor)EditorGUILayout.Popup(new GUIContent("Destination factor", "Blending destination factor."), (int)blendFactorDestination, blendFactorNames);

            if (EditorGUI.EndChangeCheck())
            {

                propertyBlendType.floatValue = (float)blendType;

                propertyBlendSource.floatValue = (float)blendFactorSource;

                propertyBlendDestination.floatValue = (float)blendFactorDestination;

                SetBlendType(myMaterial.objectReferenceValue as Material, blendType, blendFactorSource, blendFactorDestination, propertyBlendSource, propertyBlendDestination);

            }

            EditorGUI.showMixedValue = false;

            GUI.enabled = true;

        }

        /// \english
        /// <summary>
        /// UV scroll function builder.
        /// </summary>
        /// <param name="uvScroll">UV scroll property.</param>
        /// <param name="flipUVHorizontal">Flip UV horizontal property.</param>
        /// <param name="flipUVVertical">Flip UV vertical property.</param>
        /// <param name="uvScrollAnimation">UV scroll animation property.</param>
        /// <param name="uvScrollSpeed">UV scroll speed property.</param>
        /// <param name="scrollByTexel">Scroll by texel property.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// UV scroll constructor.
        /// </summary>
        /// <param name="uvScroll">Propiedad UV scroll.</param>
        /// <param name="flipUVHorizontal">Propiedad voltear UV horizontalmente.</param>
        /// <param name="flipUVVertical">Propiedad voltear UV verticalmente.</param>
        /// <param name="uvScrollAnimation">Propiedad animación de desplazamiento de las UV.</param>
        /// <param name="uvScrollSpeed">Propiedad velocidad de desplazamiento de las UV.</param>
        /// <param name="scrollByTexel">Propiedad desplazamiento por texel.</param>
        /// \endspanish
        internal static void BuildUVScroll(SerializedProperty uvScroll, SerializedProperty flipUVHorizontal, SerializedProperty flipUVVertical, SerializedProperty uvScrollAnimation, SerializedProperty uvScrollSpeed, SerializedProperty scrollByTexel)
        {
            DrawHeaderWithKeyword("UV Scroll", "Description", uvScroll);

            EditorGUI.BeginDisabledGroup(uvScroll.boolValue == false);

            CGFEditorUtilitiesClass.BuildBoolean("Flip UV Horizontal", "Description", flipUVHorizontal);

            CGFEditorUtilitiesClass.BuildBoolean("Flip UV Vertical", "Description", flipUVVertical);

            CGFEditorUtilitiesClass.BuildBoolean("UV Scroll Animation", "Description", uvScrollAnimation);

            CGFEditorUtilitiesClass.BuildVector2("UV Scroll Speed", "Description", uvScrollSpeed);

            CGFEditorUtilitiesClass.BuildBoolean("Scroll By Textel", "Description", scrollByTexel);

            EditorGUI.EndDisabledGroup();
        }

        /// \english
        /// <summary>
        /// Header builder with float toggle to manage a keyword.
        /// </summary>
        /// <param name="text">Header text.</param>
        /// <param name="description">Header description.</param>
        /// <param name="property">Toggle float property to build.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor de un título con un toggle float para gestionar una keyword.
        /// </summary>
        /// <param name="text">Texto del título</param>
        /// <param name="description">Descripción del título.</param>
        /// <param name="property">Propiedad de tipo toggle float a construir.</param>
        /// \endspanish
        public static void DrawHeaderWithKeyword(string text, string description, SerializedProperty property)
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(new GUIContent(text, description), EditorStyles.boldLabel);

            GUILayout.FlexibleSpace();

            EditorGUILayout.PropertyField(property, GUIContent.none, GUILayout.Width(10));

            EditorGUIUtility.labelWidth = 0.1f;

            EditorGUILayout.LabelField("Enable");

            EditorGUIUtility.labelWidth = 0;

            EditorGUILayout.EndHorizontal();

        }

        /// \english
        /// <summary>
        /// Set the blending type of the material.
        /// </summary>
        /// <param name="material">Material to set.</param>
        /// <param name="blendType">Blend types enumeration.</param>
        /// <param name="blendSource">Blend operations of source enumeration.</param>
        /// <param name="blendDestination">Blend operations of destination enumeration.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Configura el tipo de fusión del material.
        /// </summary>
        /// <param name="material">Material a configurar.</param>
        /// <param name="blendType">Enumeración de todos los tipos de fusión.</param>
        /// <param name="blendSource">Enumeración de las operaciones de fusión de la fuente.</param>
        /// <param name="blendDestination">Enumeración de las operaciones de fusión del destino.</param>
        /// \endspanish
        public static void SetBlendType(Material material, BlendType blendType, BlendFactor blendSource, BlendFactor blendDestination, SerializedProperty propertyBlendSource, SerializedProperty propertyBlendDestination)
        {

            switch (blendType)
            {

                case BlendType.Custom:

                    material.SetInt("_SrcBlendFactor", (int)blendSource);

                    material.SetInt("_DstBlendFactor", (int)blendDestination);

                    break;

                case BlendType.AlphaBlend:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;

                    break;

                case BlendType.Premultiplied:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);


                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.One;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;

                    break;

                case BlendType.Additive:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);

                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.One;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.One;

                    break;

                case BlendType.SoftAdditive:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.OneMinusDstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);

                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.OneMinusDstColor;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.One;

                    break;

                case BlendType.Multiplicative:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.DstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.Zero);

                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.DstColor;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.Zero;

                    break;

                case BlendType.DoubleMultiplicative:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.DstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.SrcColor);

                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.DstColor;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.SrcColor;

                    break;

                case BlendType.ParticleAdditive:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);

                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.SrcAlpha;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.One;

                    break;

                case BlendType.ParticleBlend:

                    material.SetInt("_SrcBlendFactor", (int)UnityEngine.Rendering.BlendMode.DstColor);

                    material.SetInt("_DstBlendFactor", (int)UnityEngine.Rendering.BlendMode.One);

                    propertyBlendSource.floatValue = (int)UnityEngine.Rendering.BlendMode.DstColor;

                    propertyBlendDestination.floatValue = (int)UnityEngine.Rendering.BlendMode.One;

                    break;

            }


        }

        /// \english
        /// <summary>
        /// Build of the toggle to manage visibility of a gizmo.
        /// </summary>
        /// <param name="enable">Initial status.</param>
        /// <param name="text">Property text.</param>
        /// <param name="description">Property description.</param>
        /// <param name="propertyGizmo">Property that locks the property.</param>
        /// <returns>Compact mode status.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor del toggle que gestiona la visibilidad de un gizmo.
        /// </summary>
        /// <param name="enable">Estado inicial.</param>
        /// <param name="text">Texto de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="propertyGizmo">Propiedad que bloquea la propiedad.</param>
        /// <returns>Estado del modo compacto.</returns>
        /// \endspanish
        public static bool BuildShowGizmo(bool enable, string text, string description, SerializedProperty propertyGizmo)
        {
            bool showGizmoTemp = enable;

            bool showGizmo = EditorGUILayout.Toggle(new GUIContent(text, description), enable);

            if (showGizmoTemp != showGizmo)
            {

                if (propertyGizmo.floatValue == 0)
                {

                    propertyGizmo.floatValue = 1;

                    propertyGizmo.floatValue = 0;

                }
                else
                {

                    propertyGizmo.floatValue = 0;

                    propertyGizmo.floatValue = 1;

                }

            }

            GUILayout.Space(25);

            return showGizmo;

        }

        /// \english
        /// <summary>
        /// Build of the toggle to manage visibility of a gizmo.
        /// </summary>
        /// <param name="enable">Initial status.</param>
        /// <param name="text">Property text.</param>
        /// <param name="description">Property description.</param>
        /// <param name="toggleLock">Boolean that locks the property.</param>
        /// <param name="propertyGizmo">Property that locks the property.</param>
        /// <returns>Compact mode status.</returns>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Constructor del toggle que gestiona la visibilidad de un gizmo.
        /// </summary>
        /// <param name="enable">Estado inicial.</param>
        /// <param name="text">Texto de la propiedad.</param>
        /// <param name="description">Descripción de la propiedad.</param>
        /// <param name="toggleLock">Float que bloquea la propiedad.</param>
        /// <param name="propertyGizmo">Propiedad que bloquea la propiedad.</param>
        /// <returns>Estado del modo compacto.</returns>
        /// \endspanish
        public static bool BuildShowGizmo(bool enable, string text, string description, float toggleLock, SerializedProperty propertyGizmo)
        {
            bool showGizmoTemp = enable;

            if (toggleLock == 1)
            {

                GUI.enabled = true;

            }
            else
            {

                GUI.enabled = false;

            }

            bool showGizmo = EditorGUILayout.Toggle(new GUIContent(text, description), enable);


            if (showGizmoTemp != showGizmo)
            {

                if (propertyGizmo.boolValue)
                {

                    propertyGizmo.boolValue = !propertyGizmo.boolValue;

                    propertyGizmo.boolValue = !propertyGizmo.boolValue;

                }
                else
                {

                    propertyGizmo.boolValue = !propertyGizmo.boolValue;

                    propertyGizmo.boolValue = !propertyGizmo.boolValue;

                }

            }

            GUILayout.Space(25);

            GUI.enabled = true;

            return showGizmo;

        }

        /// \english
        /// <summary>
        /// Draw a height fog position handle.
        /// </summary>
        /// <param name="enableProperty">Property that enables the handle.</param>
        /// <param name="startPosition">Handle position.</param>
        /// <param name="height">Handle position of height.</param>
        /// <param name="localHeightFog">Show the handles of local height fog.</param>
        /// <param name="showHandle">Show the handles.</param>
        /// <param name="editor">Editor of the selected gameobject.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Dibuja un controlador de posición de la niebla por altura.
        /// </summary>
        /// <param name="enableProperty">Propiedad que activa el controlador.</param>
        /// <param name="startPosition">Posición del controlador.</param>
        /// <param name="height">Posición del controlador de la altura.</param>
        /// <param name="localHeightFog">Muestra los controladores de la niebla local por altura.</param>
        /// <param name="showHandle">Muestra los controladores.</param>
        /// <param name="editor">Editor del gameobject seleccionado.</param>
        /// \endspanish
        public static void DrawHeightFogSphereHandle(SerializedProperty enableProperty, SerializedProperty startPosition, SerializedProperty height, SerializedProperty localHeightFog, bool showHandle, UnityEditor.Editor editor)
        {

            Vector3 startPositionHandlePosition = Vector3.zero;

            Vector3 heightHandlePosition;

            if (Selection.activeTransform != null)
            {

                Vector3 activeTransformPosition = Selection.activeTransform.position;

                Vector3 activeTransformLocalScale = Selection.activeTransform.localScale;

                if (showHandle & enableProperty.boolValue)
                {

                    if (localHeightFog.boolValue)
                    {

                        float localStartPosition = activeTransformPosition.y + startPosition.floatValue * activeTransformLocalScale.y;

                        startPositionHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, localStartPosition, activeTransformPosition.z), Quaternion.identity);

                        float localHeightPosition = localStartPosition + height.floatValue * activeTransformLocalScale.y;

                        heightHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, localHeightPosition, activeTransformPosition.z), Quaternion.identity);

                        Handles.DrawDottedLine(startPositionHandlePosition, heightHandlePosition, 3);

                    }
                    else
                    {

                        startPositionHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, startPosition.floatValue, activeTransformPosition.z), Quaternion.identity);

                        heightHandlePosition = Handles.PositionHandle(new Vector3(activeTransformPosition.x, startPosition.floatValue + height.floatValue, activeTransformPosition.z), Quaternion.identity);

                        Handles.DrawDottedLine(startPositionHandlePosition, heightHandlePosition, 3);

                    }

                }

            }

        }

        /// \english
        /// <summary>
        /// Draw distance fog sphere handle.
        /// </summary>
        /// <param name="enableProperty">Property that enables the handle.</param>
        /// <param name="startPosition">Handle position.</param>
        /// <param name="length">Handle radius.</param>
        /// <param name="worldDistanceFog">Show the handles of world distance fog.</param>
        /// <param name="worldDistanceFogPosition">Position of handle position of world distance fog.</param>
        /// <param name="showHandle">Show the handles.</param>
        /// <param name="editor">Editor of the selected gameobject.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Dibuja un controlador esférico de la niebla por distancia.
        /// </summary>
        /// <param name="enableProperty">Propiedad que activa el controlador.</param>
        /// <param name="startPosition">Posición del controlador.</param>
        /// <param name="length">Radio del controlador.</param>
        /// <param name="worldDistanceFog">Muestra los contorladores de la niebla de distancia del mundo.</param>
        /// <param name="worldDistanceFogPosition">Posición del controlador de posicion de la niebla de distancia del mundo.</param>
        /// <param name="showHandle">Muestra los controladores.</param>
        /// <param name="editor">Editor del gameobject seleccionado.</param>
        /// \endspanish
        public static void DrawDistanceFogSphereHandle(SerializedProperty enableProperty, SerializedProperty startPosition, SerializedProperty length, SerializedProperty worldDistanceFog, SerializedProperty worldDistanceFogPosition, bool showHandle, UnityEditor.Editor editor)
        {

            if (showHandle & enableProperty.boolValue)
            {

                if (worldDistanceFog.boolValue)
                {

                    Handles.PositionHandle(worldDistanceFogPosition.vector3Value, Quaternion.identity);

                    Handles.color = Color.blue;

                    Handles.RadiusHandle(Quaternion.identity, worldDistanceFogPosition.vector3Value, startPosition.floatValue);

                    Handles.color = Color.red;

                    Handles.RadiusHandle(Quaternion.identity, worldDistanceFogPosition.vector3Value, length.floatValue);

                }
                else
                {

                    Handles.color = Color.blue;

                    Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, startPosition.floatValue);

                    Handles.color = Color.red;

                    Handles.RadiusHandle(Quaternion.identity, Camera.main.transform.position, length.floatValue);

                }

            }

        }

        /// \english
        /// <summary>
        /// Draw a sphere handle.
        /// </summary>
        /// <param name="enableProperty">Property that enables the handle.</param>
        /// <param name="position">Handle position.</param>
        /// <param name="radius">Handle radius.</param>
        /// <param name="color">Handle color.</param>
        /// <param name="showRadiusHandle">Show the radius handle.</param>
        /// <param name="showPositionHandle">Show the position handle.</param>
        /// <param name="editor">Editor of the selected gameobject.</param>
        /// \endenglish
        /// \spanish
        /// <summary>
        /// Dibuja un controlador esférico.
        /// </summary>
        /// <param name="enableProperty">Propiedad que activa el controlador.</param>
        /// <param name="position">Posición del controlador.</param>
        /// <param name="radius">Radio del controlador</param>
        /// <param name="color">Color del controlador.</param>
        /// <param name="showRadiusHandle">Muestra el controlador de radio.</param>
        /// <param name="showPositionHandle">Muestra el controlador de posición.</param>
        /// <param name="editor">Editor del gameobject seleccionado.</param>
        /// \endspanish
        public static void DrawSphereHandle(SerializedProperty enableProperty, SerializedProperty position, SerializedProperty radius, SerializedProperty color, bool showRadiusHandle, bool showPositionHandle, UnityEditor.Editor editor)
        {

            if (showRadiusHandle & enableProperty.boolValue)
            {

                if (showPositionHandle)
                {

                    Handles.PositionHandle(position.vector3Value, Quaternion.identity);

                }

                Handles.color = color.colorValue;

                Handles.RadiusHandle(Quaternion.identity, position.vector3Value, radius.floatValue / 2);

            }

        }


        #endregion


        #region Utility Events

        #endregion
    }

}
