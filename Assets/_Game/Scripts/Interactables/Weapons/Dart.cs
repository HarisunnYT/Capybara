using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : Projectile
{
    protected override void OnEnable()
    {
        base.OnEnable();

        Rigidbody.isKinematic = false;
    }

    protected override void OnCollision(Collision collision)
    {
        base.OnCollision(collision);

        Rigidbody.isKinematic = true;
    }
}
