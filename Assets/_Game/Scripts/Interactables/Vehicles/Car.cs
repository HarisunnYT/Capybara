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

    private Vector3 inputVector;
    private float smoothXAxis;
    private float xAxisVelocity;

    [HideInInspector]
    public float velocity;

    public float maxSteeringAngle = 35;
    public float minSteeringAngle = 20;

    public float maxMotorTorque;
    public float maxBrakeTorque;
    public float antiRollBarForce;

    private const float minVelocityToKnockCharacter = 1;

    public void FixedUpdate()
    {
        if (CurrentController != null)
        {
            //inputVector = CurrentController.MovementController.GetInputVector();
            inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ragdoll"))
        {
            if (Rigidbody.velocity.magnitude > minVelocityToKnockCharacter)
            {
                CharacterController controller = collision.transform.GetComponentInParent<CharacterController>();
                if (controller)
                {
                    controller.RagdollController.SetRagdoll(true, true);
                }
            }
        }
    }
}
