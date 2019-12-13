using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private bool locked = false;
    private Vector3 pos;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (locked)
        {
            transform.position = pos;
        }
    }

    public void Lock(bool doLock)
    {
        locked = doLock;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Lock(true);
            pos = transform.position;
        }
    }
}
