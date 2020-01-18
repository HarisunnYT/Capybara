using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputUI : MonoBehaviour
{
    [SerializeField]
    private UnityStandardAssets.CrossPlatformInput.Joystick leftJoystick;

    [SerializeField]
    private UnityStandardAssets.CrossPlatformInput.Joystick rightJoystick;

    private ulong updateTick;
    private float deltaTime;

    private bool attackPressed = false;
    private bool interactPressed = false;

    void Start()
    {
        InputManager.OnUpdateDevices += UpdateDevice;
        CameraController.Instance.lockCursor = false;
    }

    void UpdateDevice(ulong updateTick, float deltaTime)
    {
        //updating left stick
        if (leftJoystick.Value.x > 0)
            InputController.InputManager.Right.UpdateWithValue(leftJoystick.Value.x, updateTick, deltaTime);
        else if (leftJoystick.Value.x < 0)
            InputController.InputManager.Left.UpdateWithValue(leftJoystick.Value.x, updateTick, deltaTime);
        if (leftJoystick.Value.y > 0)
            InputController.InputManager.Up.UpdateWithValue(leftJoystick.Value.y, updateTick, deltaTime);
        else if (leftJoystick.Value.y < 0)
            InputController.InputManager.Down.UpdateWithValue(leftJoystick.Value.y, updateTick, deltaTime);

        //updating right stick
        if (rightJoystick.Value.x > 0)
            InputController.InputManager.RotateRight.UpdateWithValue(rightJoystick.Value.x, updateTick, deltaTime);
        else if (rightJoystick.Value.x < 0)
            InputController.InputManager.RotateLeft.UpdateWithValue(rightJoystick.Value.x, updateTick, deltaTime);
        if (rightJoystick.Value.y > 0)
            InputController.InputManager.RotateUp.UpdateWithValue(rightJoystick.Value.y, updateTick, deltaTime);
        else if (rightJoystick.Value.y < 0)
            InputController.InputManager.RotateDown.UpdateWithValue(rightJoystick.Value.y, updateTick, deltaTime);

        this.updateTick = updateTick;
        this.deltaTime = deltaTime;

        if (attackPressed)
        {
            InputController.InputManager.Attack.UpdateWithValue(1, updateTick, deltaTime);
            attackPressed = false;
        }
        if (interactPressed)
        {
            InputController.InputManager.Interact.UpdateWithValue(1, updateTick, deltaTime);
            interactPressed = false;
        }
    }

    public void Attack()
    {
        attackPressed = true;
    }

    public void Interact()
    {
        interactPressed = true;
    }
}
