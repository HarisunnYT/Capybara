using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    protected BodyPartType itemSlotType;
    public BodyPartType ItemSlotType { get { return itemSlotType; } }

    private PickupableItem currentItemObject;
    public PickupableItem CurrentItemObject { get { return currentItemObject; } }

    public void AssignItem(PickupableItem newItem)
    {
        if (currentItemObject != null)
        {
            DropCurrentItem();
        }

        currentItemObject = newItem;
        ConnectItem();
    }

    public void DropCurrentItem()
    {
        currentItemObject.DropItem();
        currentItemObject = null;
    }

    private void ConnectItem()
    {        
        currentItemObject.PickUpItem(transform); 
    }

    public PickupableItemData GetPickupableItemData()
    {
        return currentItemObject == null ? null : currentItemObject.PickupableItemData;
    }

    public MovementData GetMovementData()
    {
        if (GetPickupableItemData() != null)
        {
            return currentItemObject.PickupableItemData.MovementData == null ? null : currentItemObject.PickupableItemData.MovementData;
        }

        return null;
    }
}
