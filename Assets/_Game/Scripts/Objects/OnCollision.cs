using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public delegate void CollisionDelegate(Collision collision);
    public event CollisionDelegate OnCollisionEvent;

    public delegate void TriggerDelegate(Collider collider);
    public event TriggerDelegate OnTriggerEvent;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEvent?.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEvent?.Invoke(other);
    }
}
