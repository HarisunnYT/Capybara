using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IPullable
{
    [SerializeField]
    private Transform model;

    [Header("Handle")]
    [SerializeField]
    private Rigidbody handle;

    [SerializeField]
    private float min, max;

    public Rigidbody Rigidbody { get; private set; }

    private bool attached = false;
    private Collider collisionCollider;

    private void Start()
    {
        Rigidbody = GetComponentInChildren<Rigidbody>();
        Rigidbody.isKinematic = true;

        collisionCollider = model.GetComponent<Collider>();
    }

    private void Update()
    {
        if (attached)
        {
            Vector3 inputAxis = CapybaraMove.Instance.InputAxis;
            inputAxis = CameraController.Instance.transform.TransformDirection(inputAxis);

            handle.velocity = inputAxis;
        }
    }

    private void LateUpdate()
    {
        Vector3 position = new Vector3(handle.transform.localPosition.x, handle.transform.localPosition.y, Mathf.Clamp(handle.transform.localPosition.z, min, max));
        handle.transform.localPosition = position;       
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

    public void OnDropped()
    {
        attached = false;
        Rigidbody.isKinematic = true;
        IgnoreCollision(false);

        CapybaraMove.Instance.SetParent(null);
    }

    public void OnPulled()
    {
        attached = true;
        Rigidbody.isKinematic = false;
        IgnoreCollision(true);

        CapybaraMove.Instance.SetParent(model);
    }

    private void IgnoreCollision(bool ignore)
    {
        Physics.IgnoreCollision(collisionCollider, CapybaraMove.Instance.Collider, ignore);
    }
}
