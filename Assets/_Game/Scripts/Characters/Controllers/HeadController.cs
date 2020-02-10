using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : Controller
{
    [SerializeField]
    private Transform headBone;

    [SerializeField]
    private Rigidbody headBody;
    public Rigidbody HeadBody { get { return headBody; } }

    [SerializeField]
    private LayerMask interestLayers;

    [SerializeField]
    private float lookSpeed;

    [SerializeField]
    private float maxAngle;

    private Transform currentInterest;

    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = headBone.transform.localRotation;
    }

    private void Update()
    {
        if (currentInterest != null)
        {
            Vector3 direction = currentInterest.position - headBone.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            float angle = Quaternion.Angle(headBone.rotation, targetRotation);
            if (angle < maxAngle)
            {
                headBone.rotation = Quaternion.Lerp(headBone.rotation, targetRotation, lookSpeed * Time.deltaTime);
            }
            else
            {
                headBone.localRotation = Quaternion.Lerp(headBone.localRotation, originalRotation, lookSpeed * Time.deltaTime);
            }
        }
        else
        {
            headBone.localRotation = Quaternion.Lerp(headBone.localRotation, originalRotation, lookSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Util.CheckInsideLayer(interestLayers, other.gameObject.layer))
        {
            PickupableItem pickupableItem = other.gameObject.GetComponent<PickupableItem>();
            if (pickupableItem != null && pickupableItem.Equiped)
            {
                return;
            }

            currentInterest = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInterest != null && other.gameObject == currentInterest.gameObject)
        {
            currentInterest = null;
        }
    }
}
