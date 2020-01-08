using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool Equiped { get; protected set; }

    public CharacterController CurrentController { get; protected set; }

    public Rigidbody Rigidbody { get; protected set; }

    protected Collider collider;

    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
}
