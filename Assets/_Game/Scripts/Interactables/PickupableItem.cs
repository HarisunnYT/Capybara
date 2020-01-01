using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    [SerializeField]
    private PickupableItemData pickupableItemData;
    public PickupableItemData PickupableItemData { get { return pickupableItemData; } }

    public bool Equiped { get; private set; }

    private Collider collider;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public virtual void PickUpItem(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = pickupableItemData.Position;
        transform.localRotation = Quaternion.Euler(pickupableItemData.EulerRotation);

        rigidbody.isKinematic = true;
        Equiped = true;

        InteractionController.Instance.IgnoreCollisions(collider, true);

        if (pickupableItemData.MovementData)
        {
            //set bools and weights for movement data
            AnimationController.Instance.SetAnimatorLayerWeights(pickupableItemData.MovementData.BoneWeights);
            AnimationController.Instance.SetAnimatorBools(pickupableItemData.MovementData.AnimatorBools);

            //set bools and weights for pickupable item data
            AnimationController.Instance.SetAnimatorLayerWeights(pickupableItemData.BoneWeights);
            AnimationController.Instance.SetAnimatorBools(pickupableItemData.AnimatorBools);

            CapybaraController.Instance.SetMovementStyle(pickupableItemData.MovementData.MovementStyle);
        }
    }

    public virtual void DropItem()
    {
        transform.parent = null;
        rigidbody.isKinematic = false;

        Equiped = false;

        InteractionController.Instance.IgnoreCollisions(collider, false);
    }
}
