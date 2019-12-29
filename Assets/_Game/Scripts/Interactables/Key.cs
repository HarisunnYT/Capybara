using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IPickupable
{
    [SerializeField]
    private Vector3 orientation;

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPickedUp()
    {
        rigidbody.isKinematic = true;
        //CapybaraMove.Instance.PickUpObject(transform, )
    }

    public void OnDropped()
    {
        rigidbody.isKinematic = false;
    }

    public Vector3 GetOrientation()
    {
        return orientation;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    public Rigidbody GetRigidbody()
    {
        return rigidbody;
    }
}