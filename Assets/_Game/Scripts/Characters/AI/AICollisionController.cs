using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollisionController : CollisionController
{
    [SerializeField]
    private float minForceToKnockOut = 10;

    protected override void DoRagdoll(float collisionForce)
    {
        base.DoRagdoll(collisionForce);

        Debug.Log(collisionForce);

        if (collisionForce >= minForceToKnockOut)
        {
            ((AIRagdollController)RagdollController).KnockOut();
        }
    }
}
