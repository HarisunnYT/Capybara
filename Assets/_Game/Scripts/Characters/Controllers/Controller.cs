using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AnimationController AnimationController { get; private set; }
    public RagdollController RagdollController { get; private set; }
    public AttackController AttackController { get; private set; }
    public CollisionController CollisionController { get; private set; }
    public MovementController MovementController { get; private set; }
    public InteractionController InteractionController { get; private set; }

    protected virtual void Awake()
    {
        AnimationController = GetComponent<AnimationController>();
        RagdollController = GetComponent<RagdollController>();
        AttackController = GetComponent<AttackController>();
        CollisionController = GetComponent<CollisionController>();
        MovementController = GetComponent<MovementController>();
        InteractionController = GetComponent<InteractionController>();
    }
}
