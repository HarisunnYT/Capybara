using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollisionController : CollisionController
{
    [SerializeField]
    private float minForceToKnockOut = 10;

    protected override void CheckKnockoutForce(float collisionForce)
    {
        if (collisionForce >= minForceToKnockOut && MovementController.CurrentMovementState == MovementState.Ragdoll)
        {
            ((AIRagdollController)RagdollController).KnockOut();
        }
    }
}
