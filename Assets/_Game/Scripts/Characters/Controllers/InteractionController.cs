using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : Controller
{
    [SerializeField]
    private float interactionRadius;

    private Mouth mouth;

    private void Start()
    {
        mouth = GetComponentInChildren<Mouth>();
    }

    public bool TryPickUpObject(bool includeRagdollCharacters = false)
    {
        Interactable closestObject = FindClosestObject(-1, includeRagdollCharacters);
        if (closestObject != null)
        {
            if (closestObject is PickupableItem)
            {
                PickupItem(closestObject as PickupableItem);
                return true;
            }
            else if (closestObject is GrabbleBodyPiece)
            {
                mouth.GrabRagdoll(closestObject as GrabbleBodyPiece);
            }
            else if (closestObject is Vehicle)
            {
                GetInVehicle(closestObject as Vehicle);
                return true;
            }
        }

        return false;
    }

    public Interactable FindClosestObject(float radius = -1, bool includeRagdollCharacters = false)
    {
        radius = radius == -1 ? interactionRadius : radius;

        Collider[] hitCols = Physics.OverlapSphere(transform.position, radius);
        Interactable closestObject = null;

        if (hitCols.Length > 0)
        {
            for (int i = 0; i < hitCols.Length; i++)
            {
                Interactable item = hitCols[i].GetComponent<Interactable>();
                if (item != null && !item.Equiped)
                {
                    if (closestObject == null || Vector3.Distance(hitCols[i].transform.position, transform.position) < Vector3.Distance(hitCols[i].transform.position, closestObject.transform.position))
                    {
                        if (item is GrabbleBodyPiece)
                        {
                            //check if the grabble piece character is in ragdoll mode
                            if (includeRagdollCharacters && ((GrabbleBodyPiece)item).CurrentController.MovementController.CurrentMovementState == MovementState.Ragdoll)
                            {
                                closestObject = item;
                            }
                        }
                        else
                        {
                            closestObject = item;
                        }
                    }
                }
            }
        }

        if (closestObject != null)
        {
            return closestObject;
        }

        return null;
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

    private void GetInVehicle(Vehicle vehicle)
    {
        DropAllItems();

        MovementController.SetMovementStyle(MovementStyle.Driving);
        vehicle.GetInVehicle(CharacterController);
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

    public void DropItem(BodyPartType bodySlot)
    {
        foreach (var part in MovementController.BodyParts)
        {
            if (part.ItemSlotType == bodySlot && part.GetPickupableItemData() != null)
            {
                part.DropCurrentItem();
                return;
            }
        }
    }

    public void DropAllItems()
    {
        foreach (var bodyPart in MovementController.BodyParts)
        {
            bodyPart.DropCurrentItem();
        }
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
