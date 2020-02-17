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

    private Vector3 inputVectorCameraRelative;
    private Vector3 inputVectorRaw;

    private float wobbleValue = 0;

    protected override IEnumerator GetInVehicleIE()
    {
        transform.forward = CameraController.Instance.transform.forward;

        Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        yield return base.GetInVehicleIE();
    }

    public override void GetOutOfVehicle()
    {
        base.GetOutOfVehicle();

        Rigidbody.constraints = RigidbodyConstraints.None;
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

            transform.rotation = rotation;
        }
    }
}
