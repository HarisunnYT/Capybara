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
    }

    public virtual void DropItem()
    {
        transform.parent = null;
        rigidbody.isKinematic = false;

        Equiped = false;

        InteractionController.Instance.IgnoreCollisions(collider, false);
    }
}
