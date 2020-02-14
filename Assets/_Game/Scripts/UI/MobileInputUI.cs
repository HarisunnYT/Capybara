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
    }

    void UpdateDevice(ulong updateTick, float deltaTime)
    {
        InputController.InputManager.ForceUpdateMovement(leftJoystick.Value);
        InputController.InputManager.ForceUpdateCameraRotation(rightJoystick.Value);

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
