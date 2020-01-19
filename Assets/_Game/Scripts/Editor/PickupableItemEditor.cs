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
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(pickupableItem.PickupableItemData);

            int index = 0;
            for (int i = 0; i < pickupableItem.PickupableItemData.DataPerCharacter.Length; i++)
            {
                if (i == (int)pickupableItem.CurrentController.CharacterType)
                {
                    pickupableItem.PickupableItemData.DataPerCharacter[i].Position = pickupableItem.transform.localPosition;
                    pickupableItem.PickupableItemData.DataPerCharacter[i].EulerRotation = pickupableItem.transform.localRotation.eulerAngles;
                }
            }

            Undo.RecordObject(pickupableItem.PickupableItemData, "Transform set for character");
            AssetDatabase.SaveAssets();
        }
    }
}
