using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureWithForce : MonoBehaviour
{
    [SerializeField]
    private float force;

    private FracturedObject fracturedObject;

    private void Start()
    {
        fracturedObject = GetComponentInParent<FracturedObject>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > force)
        {
            fracturedObject.Explode(collision.contacts[0].point, collision.relativeVelocity.magnitude);
            gameObject.SetActive(false);
        }
    }
}
