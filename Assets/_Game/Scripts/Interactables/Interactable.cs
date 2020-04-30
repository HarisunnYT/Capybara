using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool Equipped { get; protected set; }

    public CharacterController CurrentController { get; protected set; }

    public Rigidbody Rigidbody { get; protected set; }

    protected Collider collider;

    public delegate void OnInteractedEvent();
    public event OnInteractedEvent OnInteracted;

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public virtual void OnInteraction()
    {
        OnInteracted?.Invoke();
    }
}
