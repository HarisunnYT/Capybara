using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : Controller
{
    [SerializeField]
    protected LayerMask HittableLayers;

    [SerializeField]
    protected float forceToRagdoll = 10;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //ragdoll collision
        if (Util.CheckInsideLayer(HittableLayers, collision.gameObject.layer) && collision.relativeVelocity.magnitude >= forceToRagdoll)
        {
            DoRagdoll(collision.relativeVelocity.magnitude);
        }
    }

    protected virtual void DoRagdoll(float collisionForce)
    {
        RagdollController.SetRagdoll(true);
    }
}
