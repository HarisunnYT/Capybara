using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    protected BodyPartType itemSlotType;
    public BodyPartType ItemSlotType { get { return itemSlotType; } }

    [SerializeField]
    protected PickupableItem currentItemObject;
    public PickupableItem CurrentItemObject { get { return currentItemObject; } }

    protected CharacterController controller;
    public CharacterController Controller { get { return controller; } }

    public Rigidbody Rigidbody { get; private set; }

    public bool HasItem { get { return currentItemObject != null; } }

    private void Awake()
    {
        controller = GetComponentInParent<CharacterController>();
        Rigidbody = GetComponent<Rigidbody>();

        if (currentItemObject != null)
        {
            ConnectItem(currentItemObject);
        }
    }

    public virtual void PickUpItem(PickupableItem newItem)
    {
        DropCurrentItem();
        ConnectItem(newItem);
    }

    public virtual void DropCurrentItem()
    {
        PickupableItem previousItem = currentItemObject;
        if (currentItemObject)
        {
            currentItemObject.DropItem();
            currentItemObject = null;
        }

        if (previousItem != null)
        {
            MovementData movementData = previousItem.PickupableItemData.GetMovementData(Controller.CharacterType);
            if (movementData != null)
            {
                foreach(var b in movementData.AnimatorBools)
                {
                    Controller.AnimationController.SetBool(b.Name, !b.Result);
                }
            }

            //loop through each bone weight and check if any other body parts contain that weight
            for (int i = 0; i < System.Enum.GetNames(typeof(AnimatorBodyPartLayer)).Length; i++)
            {
                foreach (var bodyPart in Controller.BodyParts)
                {
                    if (!bodyPart.ContainsWeight((AnimatorBodyPartLayer)i))
                    {
                        Controller.AnimationController.SetAnimatorLayerWeight((AnimatorBodyPartLayer)i, 0);
                    }
                }
            }

            foreach (var b in previousItem.PickupableItemData.GetAnimatorBools(Controller.CharacterType))
            {
                Controller.AnimationController.SetBool(b.Name, !b.Result);
            }
        }
    }

    public virtual void ConnectItem(PickupableItem newItem)
    {
        PickupableItem resultedItem;

        //drop any non compatible items first
        foreach (var nonCompatibleItem in newItem.PickupableItemData.GetNonCompatibleItems(Controller.CharacterType))
        {
            foreach(var bodyPartItem in Controller.InteractionController.GetCurrentItems())
            {
                if (bodyPartItem.PickupableItemData == nonCompatibleItem)
                {
                    bodyPartItem.CurrentBodyPart.DropCurrentItem();
                }
            }
        }

        currentItemObject = newItem;
        currentItemObject.PickUpItem(transform, this, controller);
    }

    public PickupableItemData GetPickupableItemData()
    {
        return currentItemObject == null ? null : currentItemObject.PickupableItemData;
    }

    public MovementData GetMovementData()
    {
        if (GetPickupableItemData() != null)
        {
            MovementData movementData = currentItemObject.PickupableItemData.GetMovementData(controller.CharacterType);
            return movementData == null ? null : movementData;
        }

        return null;
    }

    public bool ContainsWeight(AnimatorBodyPartLayer bodyPartLayer)
    {
        if (GetMovementData())
        {
            return GetMovementData().GetWeight(bodyPartLayer) != 0;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Controller.CollisionController.BodyPartCollisionEvent(collision);
    }
}
