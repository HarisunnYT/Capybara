using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PickupableItem), true)]
public class PickupableItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PickupableItem pickupableItem = (PickupableItem)target;

        if (GUILayout.Button("Set Transform"))
        {
            SerializedObject serializedObject = new SerializedObject(pickupableItem.PickupableItemData);

            serializedObject.FindProperty("Position").vector3Value = pickupableItem.transform.localPosition;
            serializedObject.FindProperty("EulerRotation").vector3Value = pickupableItem.transform.localRotation.eulerAngles;

            serializedObject.ApplyModifiedProperties();
        }
    }
}
