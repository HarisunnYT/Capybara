using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : BodyPart
{
    [SerializeField]
    private float spring = 1000;

    public void GrabRagdoll(GrabbleBodyPiece bodyPiece)
    {
        DropCurrentItem();

        bodyPiece.transform.position = transform.position;
        //bodyPiece.Rigidbody.isKinematic = true;

        CreateJoint(bodyPiece);
    }

    public void DropRagdoll()
    {

    }

    private void CreateJoint(GrabbleBodyPiece bodyPiece)
    {
        Vector3 direction = bodyPiece.transform.position - transform.position;

        SpringJoint joint = gameObject.AddComponent<SpringJoint>();
        joint.anchor = direction;
        joint.connectedBody = bodyPiece.Rigidbody;
        joint.spring = spring;
    }
}
