using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : Projectile
{
    [SerializeField]
    private float explosionRadius = 50;

    [SerializeField]
    private float force;

    [SerializeField]
    private float hideDelay = 1;

    [SerializeField]
    protected LayerMask explosionLayers;

    protected override void OnCollision(Collision collision)
    {
        Vector3 collisionPoint = collision.GetContact(0).point;
        Collider[] colliders = Physics.OverlapSphere(collisionPoint, explosionRadius, explosionLayers);

        //ragdoll characters first
        foreach(var col in colliders)
        {
            RagdollController ragdollController = col.GetComponent<RagdollController>();
            if (ragdollController && !GameManager.Instance.IsPlayer(ragdollController.CharacterController))
            {
                ragdollController.SetRagdoll(true);
            }
        }

        //add force to rigidbodies
        foreach(var col in colliders)
        {
            Rigidbody rBody = col.GetComponent<Rigidbody>();
            if (rBody)
            {
                rBody.AddExplosionForce(force, collisionPoint, explosionRadius, 1, ForceMode.Impulse);
            }
        }

        OnDestroyed(collisionPoint, hideDelay);
    }
}
