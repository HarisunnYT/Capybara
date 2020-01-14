using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyMovementController : MovementController
{
    public override Vector3 GetInputVector(bool includeYAxis = false, bool inverseZAxis = false)
    {
        Vector3 inputVec = new Vector3(InputController.InputManager.Move.Value.x, 0, InputController.InputManager.Move.Value.y);
        inputVec = CameraController.Instance.transform.TransformDirection(inputVec);

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
}
