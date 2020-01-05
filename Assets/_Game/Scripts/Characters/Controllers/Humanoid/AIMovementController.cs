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

    [SerializeField]
    private float runSpeed;

    protected override bool MoveWithRigidbody { get { return false; } }

    private AIMovementState currentMovementState;

    protected override void Awake()
    {
        base.Awake();

        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetDestination(Vector3.zero);
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.1f && MovementController.CurrentMovementState == MovementState.Moving)
        {
            SetMovementState(AIMovementState.Idle);
        }
    }

    public void SetMovementState(AIMovementState movementState)
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
            SetMovementState(AIMovementState.Idle);
        }
    }

    private void SetDestination(Vector3 position)
    {
        agent.SetDestination(position);
        SetMovementState(AIMovementState.Walk);
    }

    protected override Vector3 GetInputVector()
    {
        return agent.velocity / maxVelocity;
    }
}
