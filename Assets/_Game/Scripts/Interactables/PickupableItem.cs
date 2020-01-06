﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : Interactable
{
    [SerializeField]
    private PickupableItemData pickupableItemData;
    public PickupableItemData PickupableItemData { get { return pickupableItemData; } }

    public BodyPart CurrentBodyPart { get; private set; }

    public virtual void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        CurrentController = controller;

        transform.parent = parent;
        transform.localPosition = pickupableItemData.Position;
        transform.localRotation = Quaternion.Euler(pickupableItemData.EulerRotation);

        CurrentBodyPart = currentBodyPart;

        rigidbody.isKinematic = true;
        Equiped = true;

        CurrentController.InteractionController.IgnoreCollisions(collider, true);

        if (pickupableItemData.MovementData)
        {
            //set bools and weights for movement data
            controller.AnimationController.SetAnimatorLayerWeights(pickupableItemData.MovementData.BoneWeights);
            controller.AnimationController.SetAnimatorBools(pickupableItemData.MovementData.AnimatorBools);

            //set bools and weights for pickupable item data
            controller.AnimationController.SetAnimatorLayerWeights(pickupableItemData.BoneWeights);
            controller.AnimationController.SetAnimatorBools(pickupableItemData.AnimatorBools);

            controller.MovementController.SetMovementStyle(pickupableItemData.MovementData.MovementStyle);
        }
    }

    public virtual void DropItem()
    {
        CurrentController.InteractionController.IgnoreCollisions(collider, false);

        CurrentController = null;
        transform.parent = null;
        rigidbody.isKinematic = false;

        Equiped = false;
    }
}
