using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicycle : Vehicle
{
    [Space()]
    [SerializeField]
    private float speed = 2;

    [SerializeField]
    private float maxSpeed = 10;

    [SerializeField]
    private float rotationSpeed = 5;

    [SerializeField]
    private float maxTilt = 30;

    [SerializeField]
    private float wobbleSpeed = 2;

    [SerializeField]
    private float wobbleMultiplier = 10;

    [Space()]
    [SerializeField]
    private Transform leftFootTarget;

    [SerializeField]
    private FixedJoint leftFootBone;

    [SerializeField]
    private Transform rightFootTarget;

    [SerializeField]
    private FixedJoint rightFootBone;

    [SerializeField]
    private Transform leftPedalArm;

    [SerializeField]
    private Transform rightPedalArm;

    [Space()]
    [SerializeField]
    private Transform wheel;

    private Vector3 inputVectorCameraRelative;
    private Vector3 inputVectorRaw;

    private float wobbleValue = 0;

    private float circumference { get { return 2 * Mathf.PI * 2; } }

    protected override IEnumerator GetInVehicleIE()
    {
        transform.forward = CameraController.Instance.transform.forward;

        Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        yield return base.GetInVehicleIE();

        if (!CurrentController.AttackController.IsHoldingWeapon())
        {
            CurrentController.RagdollController.RagdollArms(true);
        }

        CurrentController.RagdollController.RagdollLegs(true);

        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.LowerBody, true);
        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, true);

        yield return new WaitForEndOfFrame();

        Rigidbody leftFoot = CurrentController.RagdollController.LeftLegBones[CurrentController.RagdollController.LeftLegBones.Length - 1];
        leftFoot.isKinematic = true;
        leftFoot.transform.position = leftFootBone.transform.position;

        Rigidbody rightFoot = CurrentController.RagdollController.RightLegBones[CurrentController.RagdollController.RightLegBones.Length - 1];
        rightFoot.isKinematic = true;
        rightFoot.transform.position = rightFootBone.transform.position;

        rightFootBone.connectedBody = rightFoot;
        rightFootBone.connectedAnchor = rightFoot.transform.position - rightFootBone.transform.position;
        rightFoot.isKinematic = false;

        leftFootBone.connectedBody = leftFoot;
        leftFootBone.connectedAnchor = leftFoot.transform.position - leftFootBone.transform.position;
        leftFoot.isKinematic = false;
    }

    public override void GetOutOfVehicle()
    {
        Rigidbody.constraints = RigidbodyConstraints.None;

        leftFootBone.connectedBody = null;
        rightFootBone.connectedBody = null;

        CurrentController.RagdollController.RagdollLegs(false);
        CurrentController.RagdollController.RagdollArms(false);

        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.LowerBody, false);
        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, false);

        base.GetOutOfVehicle();
    }

    private void Update()
    {
        if (Equiped)
        {
            wobbleValue = Mathf.PingPong(Time.time * wobbleSpeed, 2) - 1;

            inputVectorCameraRelative = CurrentController.MovementController.GetInputVector();
            inputVectorRaw = CurrentController.MovementController.GetInputVector(cameraRelative: false);

            if (Rigidbody.velocity.magnitude < maxSpeed)
            {
                Rigidbody.AddForce(inputVectorCameraRelative * speed, ForceMode.Acceleration);
            }

            Quaternion toRotation = Quaternion.LookRotation(Rigidbody.velocity, Vector3.up);
            toRotation = Quaternion.Euler((Mathf.Abs(inputVectorRaw.magnitude) * maxTilt) + (wobbleValue * wobbleMultiplier), toRotation.eulerAngles.y, toRotation.eulerAngles.z);

            Quaternion rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (inputVectorCameraRelative == Vector3.zero)
            {
                rotation = Quaternion.Euler(rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotation.eulerAngles.z);
            }

            if (!CurrentController.AimController.IsAiming)
            {
                transform.rotation = rotation;
            }

            float angle = (Rigidbody.velocity.magnitude * 360 / circumference);
            wheel.Rotate(wheel.transform.right, angle * rotationSpeed * Time.fixedDeltaTime, Space.World);

            //rotate pedal arms
            leftPedalArm.Rotate(leftPedalArm.right, angle * Time.fixedDeltaTime, Space.World);
            rightPedalArm.Rotate(rightPedalArm.right, angle * Time.fixedDeltaTime, Space.World);

            leftFootBone.transform.position = leftFootTarget.position;
            rightFootBone.transform.position = rightFootTarget.position;
        }
    }
}
