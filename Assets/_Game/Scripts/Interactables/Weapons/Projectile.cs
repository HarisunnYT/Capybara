using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }

    private Collider collider;

    private float timeUntilColliderEnabled;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        timeUntilColliderEnabled = Time.time + 0.1f;

        collider.enabled = false;
    }

    private void Update()
    {
        if (Time.time > timeUntilColliderEnabled)
        {
            collider.enabled = true;
        }
    }
}
