using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionController : Controller
{
    [SerializeField]
    private float interactionRadius;

    [SerializeField]
    private Vector3 interactionOffset = new Vector3(0, 0.5f, 0.5f);

    public DragCharacterPart DragCharacterPart { get; private set; }
    public Vehicle CurrentVehicle { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        DragCharacterPart = GetComponentInChildren<DragCharacterPart>();
    }

    public bool TryPickUpObject(bool includeRagdollCharacters = false)
    {
        if ((int)MovementController.CurrentMovementState >= (int)MovementState.Stunned)
        {
            return false;
        }

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
                if (DragCharacterPart.HoldingRagdoll)
                {
                    DragCharacterPart.DropRagdoll();
                }
                else
                {
                    DragCharacterPart.GrabRagdoll(closestObject as GrabbleBodyPiece);
                }
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
        if (MovementController == null)
        {
            return null;
        }

        radius = radius == -1 ? interactionRadius : radius;

        Collider[] hitCols = Physics.OverlapSphere(transform.position + interactionOffset, radius);
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
                            if (includeRagdollCharacters && (int)((GrabbleBodyPiece)item).CurrentController.MovementController.CurrentMovementState >= (int)MovementState.Ragdoll)
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

    public bool ObjectInArea(float radius)
    {
        return FindClosestObject(radius) != null;
    }

    private void PickupItem(PickupableItem item)
    {
        foreach (var bodyPart in CharacterController.BodyParts)
        {
            if (bodyPart.ItemSlotType == item.PickupableItemData.GetBodyPartSlotType(CharacterController.CharacterType))
            {
                AssignItem(bodyPart, item);
                break;
            }
        }
    }

    private void GetInVehicle(Vehicle vehicle)
    {
        DropAllItems();

        vehicle.GetInVehicle(CharacterController);

        CurrentVehicle = vehicle;
    }

    private void AssignItem(BodyPart bodyPart, PickupableItem item)
    {
        MovementData movementData = item.PickupableItemData.GetMovementData(CharacterController.CharacterType);
        BodyPartType bodyPartSlotType = item.PickupableItemData.GetBodyPartSlotType(CharacterController.CharacterType);

        //if there is no movement data or the style is none, move on
        if (movementData != null && movementData.MovementStyle != MovementStyle.None)
        {
            //we need to check if an other item is modifying the movement style, if so drop that item
            foreach (var part in CharacterController.BodyParts)
            {
                if (part.GetMovementData())
                {
                    //if the movement style is not none and does not equal the new items movement style, we will need to drop it
                    if (part.GetMovementData().MovementStyle != MovementStyle.None && part.GetMovementData().MovementStyle != movementData.MovementStyle)
                    {
                        part.DropCurrentItem();
                    }
                }
            }
        }

        //if the item has a single arm or both arms, check if it needs to drop others
        if (bodyPartSlotType == BodyPartType.EitherHand || bodyPartSlotType == BodyPartType.TwoHand)
        {
            //we need to check if an object needs to be dropped
            foreach (var part in CharacterController.BodyParts)
            {
                if (part.CurrentItemObject != null)
                {
                    BodyPartType partSlotType = part.CurrentItemObject.PickupableItemData.GetBodyPartSlotType(part.Controller.CharacterType);
                    //if the new part is two handed and there are single handers, drop them, and vice versa
                    if ((bodyPartSlotType == BodyPartType.TwoHand && partSlotType == BodyPartType.EitherHand) ||
                        (partSlotType == BodyPartType.TwoHand && bodyPartSlotType == BodyPartType.EitherHand))
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
        foreach (var part in CharacterController.BodyParts)
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
        foreach (var bodyPart in CharacterController.BodyParts)
        {
            bodyPart.DropCurrentItem();
        }

        AnimationController.DisableAllAnimationLayers();
    }

    public void IgnoreCollisions(Collider collider, bool ignore)
    {
        foreach (var col in MovementController.Colliders)
        {
            Physics.IgnoreCollision(col, collider, ignore);
        }
    }

    public bool IsSlotEmpty(BodyPartType slotType)
    {
        foreach(var bodyPart in CharacterController.BodyParts)
        {
            if (bodyPart.ItemSlotType == slotType)
            {
                return bodyPart.CurrentItemObject;
            }
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + interactionOffset, interactionRadius);
    }
#endif
}
