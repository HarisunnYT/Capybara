using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : BodyPart
{
    [SerializeField]
    private Hand otherHand;

    public override void AssignItem(PickupableItem newItem)
    {
        //try and chuck it on other hand first
        if (otherHand.CurrentItemObject == null)
        {
            otherHand.ConnectItem(newItem);
            return;
        }
        else
        {
            DropCurrentItem();
        }

        ConnectItem(newItem);
    }
}
