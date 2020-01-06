using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIMovementState
{
    Idle,
    Walk,
    Running
}

public class AIMovementController : MovementController
{
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    [SerializeField]
    private float runSpeed;

    protected override bool MoveWithRigidbody { get { return false; } }

    private AIMovementState currentMovementState;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.1f && CurrentMovementState == MovementState.Moving)
        {
            SetMovementState(AIMovementState.Idle);
        }
        else if (CurrentMovementState == MovementState.Ragdoll)
        {
            agent.ResetPath();
        }
    }

    private void SetMovementState(AIMovementState movementState)
    {
        currentMovementState = movementState;

        if (movementState == AIMovementState.Running)
        {
            SetMovementState(MovementState.Moving);
            agent.speed = runSpeed;
        }
        else if (movementState == AIMovementState.Walk)
        {
            SetMovementState(MovementState.Moving);
            agent.speed = baseMovementSpeed;
        }
        else if (movementState == AIMovementState.Idle)
        {
            SetMovementState(MovementState.Idle);
        }
    }

    public void SetDestination(Vector3 position)
    {
        if (CurrentMovementState == MovementState.Ragdoll)
        {
            return;
        }

        agent.SetDestination(position);
        SetMovementState(AIMovementState.Walk);
    }

    protected override Vector3 GetInputVector()
    {
        return agent.velocity / maxVelocity;
    }
}
