using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum AIMovementState
{
    Idle,
    Walk,
    Running,
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
        else if ((int)CurrentMovementState >= 3)
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
        if ((int)CurrentMovementState >= (int)MovementState.Ragdoll)
        {
            return;
        }

        agent.SetDestination(position);
        SetMovementState(AIMovementState.Walk);
    }

    public void SetDestinationInArea(Vector3 position, float radius)
    {
        Vector3 randomPosition = Random.insideUnitSphere * radius;
        randomPosition += position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, radius, 1);

        Vector3 finalPosition = hit.position;
        SetDestination(finalPosition);
    }

    public override Vector3 GetInputVector(bool includeYAxis = false, bool inverseZAxis = false, bool cameraRelative = true)
    {
        return agent.velocity / maxVelocity;
    }

    public void RotateTowardsTransform(Transform target, float duration)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.DORotate(targetRotation.eulerAngles, duration);
    }
}
