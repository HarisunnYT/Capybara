using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class EnumList : PropertyAttribute
{
    public Type enumType;

    public EnumList(Type enumType)
    {
        this.enumType = enumType;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnumList))]
public class EnumListDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        const string searchString = "Element ";
        var displayName = property.displayName;

        if (displayName.StartsWith(searchString))
        {
            var indexString = displayName.Substring(searchString.Length);
            int index;
            if (int.TryParse(indexString, out index))
            {
                var enumList = (EnumList)attribute;
                var values = Enum.GetValues(enumList.enumType).Cast<int>().ToArray();
                Array.Sort(values);
                if (index < values.Length)
                {
                    var value = values.GetValue(index);
                    var name = Enum.GetName(enumList.enumType, value);
                    label = new GUIContent(name);
                }
            }
        }

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label);
    }
}
#endif