using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public delegate void CollisionDelegate(Collision collision);
    public event CollisionDelegate OnCollisionEvent;

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEvent?.Invoke(collision);
    }
}
