using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField]
    private Transform root = null;

    [SerializeField]
    private bool update = false;

    [SerializeField]
    private float force = 0;

    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (update)
        {
            AddDirectionalForce(Vector3.up);
        }
    }

    public void AddDirectionalForce(Vector3 direction = default, float appliedForce = 0)
    {
        Vector3 dir = direction == default ? root.forward : direction;
        float newForce = appliedForce != 0 ? appliedForce : force;

        rigidbody.AddForce(dir * newForce);
    }
}
