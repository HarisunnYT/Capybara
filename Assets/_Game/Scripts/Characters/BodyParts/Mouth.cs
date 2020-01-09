using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : BodyPart
{
    [SerializeField]
    private float spring = 1000;

    [SerializeField]
    private float breakForce = 20000;

    [SerializeField]
    private float breakTorque = 20000;

    public bool HoldingRagdoll { get; private set; }

    private SpringJoint springJoint;

    public void GrabRagdoll(GrabbleBodyPiece bodyPiece)
    {
        DropCurrentItem();

        bodyPiece.transform.position = transform.position;
        HoldingRagdoll = true;

        CreateJoint(bodyPiece);
    }

    public void DropRagdoll()
    {
        DropCurrentItem();

        if (springJoint != null)
        {
            Destroy(springJoint);
        }
    }

    private void CreateJoint(GrabbleBodyPiece bodyPiece)
    {
        Vector3 direction = bodyPiece.transform.position - transform.position;

        springJoint = gameObject.AddComponent<SpringJoint>();
        springJoint.anchor = direction;
        springJoint.connectedBody = bodyPiece.Rigidbody;

        springJoint.spring = spring;
        springJoint.breakForce = breakForce;
    }

    private void OnJointBreak(float breakForce)
    {
        DropRagdoll();
    }
}
