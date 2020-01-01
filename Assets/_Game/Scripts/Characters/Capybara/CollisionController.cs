using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField]
    private LayerMask HittableLayers;

    [SerializeField]
    private float forceToRagdoll = 10;

    private void OnCollisionEnter(Collision collision)
    {
        //ragdoll collision
        if (Util.CheckInsideLayer(HittableLayers, collision.gameObject.layer) && collision.relativeVelocity.magnitude >= forceToRagdoll)
        {
            RagdollController.Instance.SetRagdoll(true);
        }
    }
}
