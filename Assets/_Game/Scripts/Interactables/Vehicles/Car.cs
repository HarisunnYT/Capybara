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

    [SerializeField]
    public List<Wheel> wheels;

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

    private Vector3 inputVector;
    private float smoothXAxis;
    private float xAxisVelocity;

    private float velocity;

    public void FixedUpdate()
    {
        if (CurrentController != null)
        {
            //inputVector = CurrentController.MovementController.GetInputVector();
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
