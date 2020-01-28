using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyMovementController : MovementController
{
    [SerializeField]
    private Transform neckBone;

    [SerializeField]
    private float neckMovementSpeed = 2;

    private Quaternion originalRotation;

    protected override void Awake()
    {
        base.Awake();

        //originalRotation = neckBone.localRotation;
    }

    private void Update()
    {
        if (GetInputVector() == Vector3.zero)
        {
            //neckBone.localRotation = originalRotation;
        }
        else
        {
            Quaternion toRotation = Quaternion.LookRotation(GetInputVector());
            //Quaternion rotation = Quaternion.Lerp(neckBone.rotation, toRotation, neckMovementSpeed * Time.deltaTime);

            //toRotation.eulerAngles = new Vector3(0, toRotation.eulerAngles.y, 0);
            //neckBone.rotation = toRotation;
        }

        if (InputController.InputManager.Jump.WasPressed)
        {
            TryJump();
        }
    }

    public override Vector3 GetInputVector(bool includeYAxis = false, bool inverseZAxis = false, bool cameraRelative = true)
    {
        Vector3 inputVec = new Vector3(InputController.InputManager.Move.Value.x, 0, InputController.InputManager.Move.Value.y);

        if (cameraRelative)
        {
            inputVec = CameraController.Instance.transform.TransformDirection(inputVec);
        }

        if (!includeYAxis)
        {
            inputVec.y = 0;
        }
        if (inverseZAxis)
        {
            inputVec.z *= -1;
        }

        return inputVec;
    }

    protected override void Jump()
    {
        Vector3 force = Vector3.up * upwardJumpForce;

        if (!AnimationController.GetBool("Standing"))
        {
            force += (transform.forward * forwardJumpForce);
        }

        MainBody.AddForce(force, ForceMode.Impulse);
    }
}
