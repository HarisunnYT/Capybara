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
            MovementController movementController = col.GetComponent<MovementController>();
            if (movementController)
            {
                float distanceFromCharacter = Vector3.Distance(movementController.transform.position, collisionPoint);
                float explosionForce = explosionRadius - distanceFromCharacter;

                if (explosionForce > explosionRadius / 2)
                {
                    movementController.RagdollController.SetRagdoll(true);
                }
                else
                {
                    movementController.AddKnockBackForce((collisionPoint - movementController.transform.position).normalized, explosionForce);
                }
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

        float distanceFromCapy = Vector3.Distance(GameManager.Instance.CapyController.transform.position, collisionPoint);
        float strength = explosionRadius - distanceFromCapy;

        if (strength > 0)
        {
            CameraController.Instance.ShakeScreen(1, strength);
        }

        OnDestroyed(collisionPoint, hideDelay);
    }
}
