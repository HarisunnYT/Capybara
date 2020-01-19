using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : Interactable
{
    [SerializeField]
    protected VehicleData vehicleData;

    [Space()]
    [SerializeField]
    private FixedJoint leftHandBone;

    [SerializeField]
    private FixedJoint rightHandBone;

    [SerializeField]
    private Transform steeringWheel;

    [SerializeField]
    private float wheelTurnSpeed = 10;

    [Space()]
    [SerializeField]
    private Transform seatBone;

    [Space()]
    [SerializeField]
    private FixedJoint leftFootBone;

    [SerializeField]
    private FixedJoint rightFootBone;

    [SerializeField]
    private Transform brake;

    [SerializeField]
    private Transform accelerator;

    [SerializeField]
    private float pedalPushSpeed = 10;

    private Collider[] colliders;

    protected override void Start()
    {
        base.Start();

        colliders = GetComponents<Collider>();
    }

    protected virtual void Update()
    {
        if (CurrentController != null)
        {
            Vector3 inputVector = CurrentController.MovementController.GetInputVector(cameraRelative: false);

            steeringWheel.transform.localRotation = Quaternion.Lerp(steeringWheel.transform.localRotation, Quaternion.Euler(0, 0, -45 * inputVector.x), wheelTurnSpeed * Time.deltaTime);
            brake.transform.localRotation = Quaternion.Lerp(brake.transform.localRotation, Quaternion.Euler(-20 * inputVector.z, 0, 0), pedalPushSpeed * Time.deltaTime);
            accelerator.transform.localRotation = Quaternion.Lerp(accelerator.transform.localRotation, Quaternion.Euler(20 * inputVector.z, 0, 0), pedalPushSpeed * Time.deltaTime);
        }
    }

    public void GetInVehicle(CharacterController characterController)
    {
        CurrentController = characterController;

        StartCoroutine(GetInVehicleIE());
    }

    private IEnumerator GetInVehicleIE()
    {
        InputController.InputManager.ForceUpdateMovement(Vector2.zero);
        CurrentController.MovementController.MainBody.velocity = Vector3.zero;

        CurrentController.AnimationController.SetInstantBoneMovement(1);
        CurrentController.AnimationController.DisableAllAnimationLayers();

        yield return new WaitForEndOfFrame();

        CurrentController.MovementController.SetMovementStyle(MovementStyle.Driving);

        yield return new WaitForEndOfFrame();

        CurrentController.transform.parent = seatBone;
        CurrentController.transform.localPosition = Vector3.zero;
        CurrentController.transform.rotation = Quaternion.LookRotation(seatBone.forward, seatBone.up);

        CurrentController.MovementController.SetKinematic(true);

        foreach (var col in colliders)
        {
            CurrentController.InteractionController.IgnoreCollisions(col, true);
        }

        if (GameManager.Instance.IsPlayer(CurrentController))
        {
            CameraController.Instance.SetMinMaxDistance(vehicleData.cameraMinDistance, vehicleData.cameraMaxDistance);
        }

        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, true);
        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.LowerBody, true);

        yield return new WaitForEndOfFrame();

        Rigidbody leftHand = CurrentController.RagdollController.LeftHandBones[CurrentController.RagdollController.LeftHandBones.Length - 1];
        leftHand.isKinematic = true;
        leftHand.transform.position = leftHandBone.transform.position;

        Rigidbody rightHand = CurrentController.RagdollController.RightHandBones[CurrentController.RagdollController.RightHandBones.Length - 1];
        rightHand.isKinematic = true;
        rightHand.transform.position = rightHandBone.transform.position;

        Rigidbody leftFoot = CurrentController.RagdollController.LeftLegBones[CurrentController.RagdollController.LeftLegBones.Length - 1];
        leftFoot.isKinematic = true;
        leftFoot.transform.position = leftFootBone.transform.position;

        Rigidbody rightFoot = CurrentController.RagdollController.RightLegBones[CurrentController.RagdollController.RightLegBones.Length - 1];
        rightFoot.isKinematic = true;
        rightFoot.transform.position = rightFootBone.transform.position;

        yield return new WaitForEndOfFrame();

        CurrentController.RagdollController.RagdollArms(true);
        CurrentController.RagdollController.RagdollLegs(true);

        rightHandBone.connectedBody = rightHand;
        rightHandBone.connectedAnchor = rightHand.transform.position - rightHandBone.transform.position;
        rightHand.isKinematic = false;

        leftHandBone.connectedBody = leftHand;
        leftHandBone.connectedAnchor = leftHand.transform.position - leftHandBone.transform.position;
        leftHand.isKinematic = false;

        rightFootBone.connectedBody = rightFoot;
        rightFootBone.connectedAnchor = rightFoot.transform.position - rightFootBone.transform.position;
        rightFoot.isKinematic = false;

        leftFootBone.connectedBody = leftFoot;
        leftFootBone.connectedAnchor = leftFoot.transform.position - leftFootBone.transform.position;
        leftFoot.isKinematic = false;

    }

    public void GetOutOfVehicle()
    {
        CurrentController.ResetParent();
        CurrentController.MovementController.SetKinematic(false);

        foreach (var col in colliders)
        {
            CurrentController.InteractionController.IgnoreCollisions(col, false);
        }

        if (GameManager.Instance.IsPlayer(CurrentController))
        {
            CameraController.Instance.ResetMinMaxDistance();
        }

        CurrentController.MovementController.SetMovementStyle(MovementStyle.Grounded);
        CurrentController.AnimationController.SetInstantBoneMovement(0);

        CurrentController = null;
    }
}
