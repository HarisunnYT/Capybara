using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyMovementController : MovementController
{
    protected override Vector3 GetInputVector()
    {
        Vector3 inputVec = new Vector3(InputController.InputManager.Move.Value.x, 0, InputController.InputManager.Move.Value.y);
        inputVec = CameraController.Instance.transform.TransformDirection(inputVec);

        return inputVec;
    }
}
