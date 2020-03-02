using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : Projectile
{
    protected override void OnCollision(Collision collision)
    {
        base.OnCollision(collision);

        Limb limb = collision.gameObject.GetComponentInParent<Limb>();
        Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();

        if (limb != null && rigidbody != null && limb.ContainsBody(rigidbody))
        {
            limb.RagdollForDuration(0.5f);
            limb.AddForceToBodies(-collision.relativeVelocity / 2);

            gameObject.SetActive(false);
        }
    }
}
