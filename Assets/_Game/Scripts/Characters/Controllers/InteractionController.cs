using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : Controller
{
    [SerializeField]
    private float interactionRadius;

    protected void FindInteractableObjects()
    {
        Collider[] hitCols = Physics.OverlapSphere(transform.position, interactionRadius);
        PickupableItem closestObject = null;

        if (hitCols.Length > 0)
        {
            for (int i = 0; i < hitCols.Length; i++)
            {
                PickupableItem item = hitCols[i].GetComponent<PickupableItem>();
                if (item != null && !item.Equiped)
                {
                    if (closestObject == null || Vector3.Distance(hitCols[i].transform.position, transform.position) < Vector3.Distance(hitCols[i].transform.position, closestObject.transform.position))
                    {
                        closestObject = item;
                    }
                }
            }
        }

        if (closestObject != null)
        {
            PickupItem(closestObject);
        }
    }

    private void PickupItem(PickupableItem item)
    {
        foreach (var bodyPart in MovementController.BodyParts)
        {
            if (bodyPart.ItemSlotType == item.PickupableItemData.ItemSlotType)
            {
                AssignItem(bodyPart, item);
                break;
            }
        }
    }

    private void AssignItem(BodyPart bodyPart, PickupableItem item)
    {
        //if there is no movement data or the style is none, move on
        if (item.PickupableItemData.MovementData != null && item.PickupableItemData.MovementData.MovementStyle != MovementStyle.None)
        {
            //we need to check if an other item is modifying the movement style, if so drop that item
            foreach (var part in MovementController.BodyParts)
            {
                if (part.GetMovementData())
                {
                    //if the movement style is not none and does not equal the new items movement style, we will need to drop it
                    if (part.GetMovementData().MovementStyle != MovementStyle.None && part.GetMovementData().MovementStyle != item.PickupableItemData.MovementData.MovementStyle)
                    {
                        part.DropCurrentItem();
                    }
                }
            }
        }

        //if the item has a single arm or both arms, check if it needs to drop others
        if (item.PickupableItemData.ItemSlotType == BodyPartType.EitherHand || item.PickupableItemData.ItemSlotType == BodyPartType.TwoHand)
        {
            //we need to check if an object needs to be dropped
            foreach (var part in MovementController.BodyParts)
            {
                if (part.CurrentItemObject != null)
                {
                    //if the new part is two handed and there are single handers, drop them, and vice versa
                    if ((item.PickupableItemData.ItemSlotType == BodyPartType.TwoHand && part.CurrentItemObject.PickupableItemData.ItemSlotType == BodyPartType.EitherHand) ||
                        (part.CurrentItemObject.PickupableItemData.ItemSlotType == BodyPartType.TwoHand && item.PickupableItemData.ItemSlotType == BodyPartType.EitherHand))
                    {
                        part.DropCurrentItem();
                    }
                }
            }
        }
        
        bodyPart.AssignItem(item);
    }

    public void IgnoreCollisions(Collider collider, bool ignore)
    {
        foreach(var col in MovementController.Colliders)
        {
            Physics.IgnoreCollision(col, collider, ignore);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
#endif
}
