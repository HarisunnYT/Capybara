using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterHat : PickupableItem
{
    private float pingPongValue = 0;

    public override void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        base.PickUpItem(parent, currentBodyPart, controller);

        controller.RagdollController.ResetVelocity();

        pingPongValue = 0;

        controller.AnimationController.SetInstantBoneMovement(0, () =>
        {
            controller.RagdollController.FakeRagdoll(true);
            controller.RagdollController.SpineBody.isKinematic = true;

            controller.MovementController.MainBody.transform.position += Vector3.up;

            controller.RagdollController.SetMetaRigRotationX(-40);
            controller.RagdollController.SetMetaRigRotationY(0);
        });
    }

    public override void DropItem()
    {
        CurrentController.RagdollController.FakeRagdoll(false);
        CurrentController.RagdollController.ResetMetaRigRotation();

        base.DropItem();
    }

    private void FixedUpdate()
    {
        if (Equipped && CurrentController == GameManager.Instance.CapyController)
        {
            pingPongValue = Mathf.PingPong(Time.time, 2) - 1;

            Vector3 movementVector = CurrentController.MovementController.GetInputVector(true);
            float movementSpeed = CurrentController.MovementController.GetMaxVelocity();
            float fixedDeltaTime = Time.fixedDeltaTime;

            movementVector = new Vector3(movementVector.x * movementSpeed * fixedDeltaTime, movementVector.y * movementSpeed * fixedDeltaTime, movementVector.z * movementSpeed * fixedDeltaTime);
            movementVector.y += pingPongValue;

            CurrentController.MovementController.MainBody.velocity = movementVector;
        }
    }
}
