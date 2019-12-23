using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraMove : CharacterMove
{
    [SerializeField]
    private float deadzone = 0.1f;

    private Vector3 inputAxis;

    private void Update()
    {
        inputAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputAxis != Vector3.zero && inputAxis.magnitude > deadzone)
        {
            inputAxis = CameraController.Instance.transform.TransformDirection(inputAxis);
            inputAxis.y = 0;

            if (inputAxis.magnitude > 0.5f)
            {
                SetRunning(inputAxis);
            }
            else
            {
                SetWalking(inputAxis);
            }
        }
        else
        {
            SetIdle();
        }
    }
}
