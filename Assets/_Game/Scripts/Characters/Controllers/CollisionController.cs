﻿using System.Collections;
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
        CollisionCheck(collision);
    }

    public void BodyPartCollisionEvent(Collision collision)
    {
        CollisionCheck(collision);
    }

    private void CollisionCheck(Collision collision)
    {
        //ragdoll collision
        if (Util.CheckInsideLayer(HittableLayers, collision.gameObject.layer))
        {
            if (collision.relativeVelocity.magnitude >= forceToRagdoll)
            {
                DoRagdoll(collision.relativeVelocity.magnitude);
            }
            CheckKnockoutForce(collision.relativeVelocity.magnitude);
        }
    }

    protected virtual void DoRagdoll(float collisionForce)
    {
        RagdollController.SetRagdoll(true);
    }

    protected virtual void CheckKnockoutForce(float force) { }
}
