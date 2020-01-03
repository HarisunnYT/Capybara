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
            pickupableItem.PickupableItemData.Position = pickupableItem.transform.localPosition;
            pickupableItem.PickupableItemData.EulerRotation = pickupableItem.transform.localRotation.eulerAngles;
        }
    }
}
