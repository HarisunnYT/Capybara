using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mouth : BodyPart
{
    [SerializeField]
    private float spring = 1000;

    [SerializeField]
    private float snapDuration = 1;

    [SerializeField]
    private float breakForce = 20000;

    [SerializeField]
    private float breakTorque = 20000;

    public bool HoldingRagdoll { get; private set; }

    public CharacterController CurrentHeldController { get; private set; }
    public Transform CurrentHeldBone { get; private set; }

    private SpringJoint springJoint;

    public void GrabRagdoll(GrabbleBodyPiece bodyPiece)
    {
        DropCurrentItem();

        bodyPiece.transform.position = transform.position;
        HoldingRagdoll = true;
        CurrentHeldBone = bodyPiece.transform;

        CurrentHeldController = bodyPiece.CurrentController;
        CurrentHeldController.RagdollController.IgnoreRagdollAgainstCollider(Controller.CollisionController.MainCollider, true);

        controller.MovementController.SetMovementStyle(MovementStyle.Dragging);

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

        controller.MovementController.SetMovementStyle(MovementStyle.Grounded);

        HoldingRagdoll = false;
        CurrentHeldController = null;

        CurrentHeldBone = null;
    }

    private Vector3 reciprocal(Vector3 input)
    {
        return new Vector3(1f / input.x, 1f / input.y, 1f / input.z);
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
