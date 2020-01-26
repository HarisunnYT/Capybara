using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NWH.WheelController3D;

public class Car : Vehicle
{
    [System.Serializable]
    public class Wheel
    {
        public WheelController wc;
        public bool steer;
        public bool power;
    }

    [System.Serializable]
    public struct WheelVisual
    {
        public Rigidbody Rigidbody;
        public Collider Collider;
    }

    [SerializeField]
    public List<Wheel> wheels;

    [SerializeField]
    private WheelVisual[] wheelVisuals;

    [Space()]
    [SerializeField]
    private float maxSteeringAngle = 35;

    [SerializeField]
    private float minSteeringAngle = 20;

    [SerializeField]
    private float maxMotorTorque;

    [SerializeField]
    private float maxBrakeTorque;

    [SerializeField]
    private float antiRollBarForce;

    [Space()]
    [SerializeField]
    private float minVelocityToKnockCharacter = 1;

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
    private FixedJoint leftFootBone;

    [SerializeField]
    private FixedJoint rightFootBone;

    [SerializeField]
    private Transform brake;

    [SerializeField]
    private Transform accelerator;

    [SerializeField]
    private float pedalPushSpeed = 10;

    private Vector3 inputVector;
    private float smoothXAxis;
    private float xAxisVelocity;

    private float velocity;

    public void FixedUpdate()
    {
        if (CurrentController != null)
        {
            inputVector = CurrentController.MovementController.GetInputVector(cameraRelative: false);

            velocity = transform.InverseTransformDirection(this.GetComponent<Rigidbody>().velocity).z;
            smoothXAxis = Mathf.SmoothDamp(smoothXAxis, inputVector.x, ref xAxisVelocity, 0.12f);

            foreach (Wheel w in wheels)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    w.wc.brakeTorque = maxBrakeTorque;
                }
                else
                {
                    w.wc.brakeTorque = 0.0f;
                }

                if (Mathf.Sign(velocity) < 0.1f && inputVector.z > 0.1f)
                    w.wc.brakeTorque = maxBrakeTorque;

                if (w.power) w.wc.motorTorque = maxMotorTorque * inputVector.z;
                if (w.steer) w.wc.steerAngle = Mathf.Lerp(maxSteeringAngle, minSteeringAngle, Mathf.Abs(velocity) * 0.05f) * inputVector.x;
            }
        }

        ApplyAntirollBar();
    }

    protected virtual void Update()
    {
        if (CurrentController != null)
        {
            Vector3 inputVector = CurrentController.MovementController.GetInputVector(cameraRelative: false);

            steeringWheel.transform.localRotation = Quaternion.Lerp(steeringWheel.transform.localRotation, Quaternion.Euler(0, 0, -25 * inputVector.x), wheelTurnSpeed * Time.deltaTime);
            brake.transform.localRotation = Quaternion.Lerp(brake.transform.localRotation, Quaternion.Euler(-10 * inputVector.z, 0, 0), pedalPushSpeed * Time.deltaTime);
            accelerator.transform.localRotation = Quaternion.Lerp(accelerator.transform.localRotation, Quaternion.Euler(10 * inputVector.z, 0, 0), pedalPushSpeed * Time.deltaTime);
        }
    }

    protected override IEnumerator GetInVehicleIE()
    {
        yield return base.GetInVehicleIE();

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

    public override void GetOutOfVehicle()
    {
        rightHandBone.connectedBody = null;
        leftHandBone.connectedBody = null;
        rightFootBone.connectedBody = null;
        leftFootBone.connectedBody = null;

        CurrentController.RagdollController.RagdollArms(false);
        CurrentController.RagdollController.RagdollLegs(false);

        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, false);
        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.LowerBody, false);

        base.GetOutOfVehicle();
    }

    protected override void UpdateParts()
    {
        base.UpdateParts();

        if (health <= 0)
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                wheels[i].wc.enabled = false;
            }

            for (int i = 0; i < wheelVisuals.Length; i++)
            {
                wheelVisuals[i].Rigidbody.isKinematic = false;
                wheelVisuals[i].Collider.enabled = true;
            }
        }
    }

    public void ApplyAntirollBar()
    {
        for (int i = 0; i < wheels.Count; i += 2)
        {
            WheelController leftWheel = wheels[i].wc;
            WheelController rightWheel = wheels[i + 1].wc;

            if (!leftWheel.springOverExtended && !leftWheel.springBottomedOut && !rightWheel.springOverExtended && !rightWheel.springBottomedOut)
            {
                float leftTravel = leftWheel.springTravel;
                float rightTravel = rightWheel.springTravel;

                float arf = (leftTravel - rightTravel) * antiRollBarForce;

                if (leftWheel.isGrounded)
                    leftWheel.parent.GetComponent<Rigidbody>().AddForceAtPosition(leftWheel.wheel.up * -arf, leftWheel.wheel.worldPosition);

                if (rightWheel.isGrounded)
                    rightWheel.parent.GetComponent<Rigidbody>().AddForceAtPosition(rightWheel.wheel.up * arf, rightWheel.wheel.worldPosition);
            }
        }
    }

    public void OnMotorValueChanged(float v)
    {
        maxMotorTorque = v;
    }

    public void OnBrakeValueChanged(float a)
    {
        maxBrakeTorque = a;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ragdoll") || collider.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            if (Rigidbody.velocity.magnitude > minVelocityToKnockCharacter)
            {
                CharacterController controller = collider.transform.GetComponentInParent<CharacterController>();
                if (controller)
                {
                    controller.RagdollController.SetRagdoll(true, false);
                }
            }
        }
    }
}
