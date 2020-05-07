using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanglePickupableObject : PickupableItem
{
    public override void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        base.PickUpItem(parent, currentBodyPart, controller);

        transform.parent = null;

    }
}
