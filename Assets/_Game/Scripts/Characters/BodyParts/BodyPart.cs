using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    protected ItemSlotType itemSlotType;
    public ItemSlotType ItemSlotType { get { return itemSlotType; } }

    private PickupableItem currentItemObject;

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
    }

    private void ConnectItem()
    {        
        currentItemObject.PickUpItem(transform); 
    }
}
