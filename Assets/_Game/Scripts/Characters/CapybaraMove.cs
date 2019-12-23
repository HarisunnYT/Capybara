using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraMove : CharacterMove
{
    private Vector3 inputAxis;

    private void Update()
    {
        inputAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputAxis != Vector3.zero)
        {
            inputAxis = CameraController.Instance.transform.TransformDirection(inputAxis);
            inputAxis.y = 0;
            SetWalking(inputAxis);
        }
        else
        {
            SetIdle();
        }
    }
}
