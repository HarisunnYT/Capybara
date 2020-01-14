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

    public GameObject PivotObject { get; private set; }
    public Rigidbody PivotBody { get; private set; }

    private BoxCollider pivotCollider;

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
        SetPivot(bodyPiece.transform);
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

        RemovePivot();

        HoldingRagdoll = false;
        CurrentHeldController = null;

        CurrentHeldBone = null;
    }

    private void SetPivot(Transform grabbedPiece)
    {
        if (PivotObject == null)
        {
            PivotObject = new GameObject(controller.Parent.name + " Mouth Pivot");
            PivotBody = PivotObject.gameObject.AddComponent<Rigidbody>();
            PivotBody.useGravity = false;

            controller.MovementController.MainBody.isKinematic = true;

            //pivotCollider = PivotObject.AddComponent<BoxCollider>();

            //controller.RagdollController.IgnoreRagdollAgainstCollider(pivotCollider, true);
            //CurrentHeldController.RagdollController.IgnoreRagdollAgainstCollider(pivotCollider, true);

            //Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
            //Bounds controllerBounds = controller.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
            //controllerBounds.size /= 100;

            //bounds.Encapsulate(controllerBounds);

            //pivotCollider.center = bounds.center - PivotObject.transform.position;
            //pivotCollider.size = bounds.size;
        }

        PivotObject.transform.forward = controller.transform.forward;
        PivotObject.transform.position = transform.position;
        controller.Parent.SetParent(PivotObject.transform);
    }

    private void Update()
    {
        if (HoldingRagdoll)
        {
            
        }
    }

    private void RemovePivot()
    {
        controller.Parent.SetParent(null);
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
