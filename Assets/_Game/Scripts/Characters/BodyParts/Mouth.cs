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

    public CharacterController CurrentHeldController { get; private set; }

    private SpringJoint springJoint;

    public void GrabRagdoll(GrabbleBodyPiece bodyPiece)
    {
        DropCurrentItem();

        bodyPiece.transform.position = transform.position;

        HoldingRagdoll = true;
        CurrentHeldController = bodyPiece.CurrentController;

        CurrentHeldController.RagdollController.IgnoreRagdollAgainstCollider(Controller.CollisionController.MainCollider, true);

        CreateJoint(bodyPiece);
    }

    public void DropRagdoll()
    {
        DropCurrentItem();

        if (springJoint != null)
        {
            Destroy(springJoint);
            CurrentHeldController.RagdollController.IgnoreRagdollAgainstCollider(Controller.CollisionController.MainCollider, false);
        }

        HoldingRagdoll = false;
        CurrentHeldController = null;
    }

    private void CreateJoint(GrabbleBodyPiece bodyPiece)
    {
        Vector3 direction = bodyPiece.transform.position - transform.position;

        springJoint = gameObject.AddComponent<SpringJoint>();
        springJoint.anchor = direction;
        springJoint.connectedBody = bodyPiece.Rigidbody;

        springJoint.spring = 0;
        springJoint.breakForce = breakForce;
    }

    private void OnJointBreak(float breakForce)
    {
        DropRagdoll();
    }
}
