using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : Interactable
{
    [SerializeField]
    private PickupableItemData pickupableItemData;
    public PickupableItemData PickupableItemData { get { return pickupableItemData; } }

    public BodyPart CurrentBodyPart { get; private set; }

    private Animator animator;

    private const float pickUpDelay = 1;
    private float timer = float.MaxValue;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time > timer)
        {
            Equipped = false;
            timer = float.MaxValue;
        }
    }

    /// <summary>
    /// Call ConnectItem on the current body part instead
    /// </summary>
    public virtual void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        CurrentController = controller;

        transform.parent = parent;
        transform.localPosition = pickupableItemData.GetPosition(controller.CharacterType);
        transform.localRotation = Quaternion.Euler(pickupableItemData.GetEulerRotation(controller.CharacterType));

        CurrentBodyPart = currentBodyPart;

        Rigidbody.isKinematic = true;
        Equipped = true;

        CurrentController.InteractionController.IgnoreCollisions(collider, true);

        MovementData movementData = pickupableItemData.GetMovementData(controller.CharacterType);

        //set bools and weights for pickupable item data
        controller.AnimationController.SetAnimatorLayerWeights(pickupableItemData.GetBoneWeights(controller.CharacterType));
        controller.AnimationController.SetAnimatorBools(pickupableItemData.GetAnimatorBools(controller.CharacterType));

        if (movementData)
        {
            //set bools and weights for movement data
            controller.AnimationController.SetAnimatorLayerWeights(movementData.BoneWeights);
            controller.AnimationController.SetAnimatorBools(movementData.AnimatorBools);

            controller.MovementController.SetMovementStyle(movementData.MovementStyle);

            CameraController.Instance.SetOffset(movementData.CameraOffset, 0.5f);
        }

        if (animator)
        {
            animator.SetTrigger("OnPickUp");
        }
    }

    /// <summary>
    /// Call Drop on the current body part instead
    /// </summary>
    public virtual void DropItem()
    {
        CurrentController.InteractionController.IgnoreCollisions(collider, false);

        CurrentController = null;
        transform.parent = null;
        Rigidbody.isKinematic = false;

        Equipped = false;

        timer = Time.time + pickUpDelay;

        if (animator)
        {
            animator.SetTrigger("OnDrop");
        }
    }
}
