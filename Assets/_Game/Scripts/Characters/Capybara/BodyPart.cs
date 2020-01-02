using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    protected BodyPartType itemSlotType;
    public BodyPartType ItemSlotType { get { return itemSlotType; } }

    protected PickupableItem currentItemObject;
    public PickupableItem CurrentItemObject { get { return currentItemObject; } }

    public virtual void AssignItem(PickupableItem newItem)
    {
        DropCurrentItem();
        ConnectItem(newItem);
    }

    public virtual void DropCurrentItem()
    {
        if (currentItemObject != null)
        {
            currentItemObject.DropItem();
            currentItemObject = null;
        }
    }

    public virtual void ConnectItem(PickupableItem newItem)
    {
        currentItemObject = newItem;
        currentItemObject.PickUpItem(transform, this);
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
