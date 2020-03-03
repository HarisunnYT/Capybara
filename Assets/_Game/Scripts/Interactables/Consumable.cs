using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    protected virtual void OnPickedUp() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<CapyMovementController>())
        {
            OnPickedUp();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<CapyMovementController>())
        {
            OnPickedUp();
        }
    }
}
