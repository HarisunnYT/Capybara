using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterHat : PickupableItem
{
    public override void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        base.PickUpItem(parent, currentBodyPart, controller);

        controller.RagdollController.FakeRagdoll(true);
        foreach(var spine in controller.Spines)
        {
            spine.isKinematic = true;
        }
    }

    public override void DropItem()
    {
        CurrentController.RagdollController.FakeRagdoll(false);
        CurrentController.GetBodyPart(BodyPartType.Head).Rigidbody.isKinematic = false;
        foreach (var spine in CurrentController.Spines)
        {
            spine.isKinematic = false;
        }

        base.DropItem();
    }
}
