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

    private void Start()
    {
        controller = GetComponentInParent<CharacterController>();
        Rigidbody = GetComponent<Rigidbody>();

        if (currentItemObject != null)
        {
            ConnectItem(currentItemObject);
        }
    }

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

    private void OnCollisionEnter(Collision collision)
    {
        Controller.CollisionController.BodyPartCollisionEvent(collision);
    }
}
