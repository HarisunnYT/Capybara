using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardBox : PickupableItem
{
    [SerializeField]
    private float movingHeight = 2;

    [SerializeField]
    private float boxMovementSpeed = 5;

    private Vector3 originalPosition;

    public override void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        base.PickUpItem(parent, currentBodyPart, controller);

        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (Equipped)
        {
            if (CurrentController.MovementController.GetInputVector().magnitude > 0)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(originalPosition.x, originalPosition.y, movingHeight), boxMovementSpeed * Time.deltaTime);
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(originalPosition.x, originalPosition.y, originalPosition.z), boxMovementSpeed * Time.deltaTime);
            }
        }
    }
}
