using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public static InteractionController Instance;

    [SerializeField]
    private float interactionRadius;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.E))
        {
            FindInteractableObjects();
        }
    }

    private void FindInteractableObjects()
    {
        Collider[] hitCols = Physics.OverlapSphere(transform.position, interactionRadius);
        PickupableItem closestObject = null;

        if (hitCols.Length > 0)
        {
            for (int i = 0; i < hitCols.Length; i++)
            {
                PickupableItem item = hitCols[i].GetComponent<PickupableItem>();
                if (item != null && !item.Equiped)
                {
                    if (closestObject == null || Vector3.Distance(hitCols[i].transform.position, transform.position) < Vector3.Distance(hitCols[i].transform.position, closestObject.transform.position))
                    {
                        closestObject = item;
                    }
                }
            }
        }

        if (closestObject != null)
        {
            PickupItem(closestObject);
        }
    }

    private void PickupItem(PickupableItem item)
    {
        foreach (var bodyPart in CapybaraController.Instance.BodyParts)
        {
            if (bodyPart.ItemSlotType == item.PickupableItemData.ItemSlotType)
            {
                bodyPart.AssignItem(item);
                break;
            }
        }
    }

    public void IgnoreCollisions(Collider collider, bool ignore)
    {
        foreach(var col in CapybaraController.Instance.Colliders)
        {
            Physics.IgnoreCollision(col, collider, ignore);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
#endif
}
