using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InputController : MonoBehaviour
{
    public static InputProfile InputManager;

    private void Start()
    {
        InputManager = new InputProfile();
    }
}

public class InputProfile : PlayerActionSet
{
    //movement
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Up;
    public PlayerAction Down;

    public PlayerAction RotateLeft;
    public PlayerAction RotateRight;
    public PlayerAction RotateUp;
    public PlayerAction RotateDown;

    public PlayerTwoAxisAction Move;
    public PlayerTwoAxisAction RotateCamera;

    //button presses
    public PlayerAction Attack;
    public PlayerAction Aim;
    public PlayerAction Interact;
    public PlayerAction Jump;

    public PlayerAction RightBumper;
    public PlayerAction LeftBumper;

    public PlayerAction Escape;

    private ulong updateTick;
    private float deltaTime;

    public InputProfile()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Up = CreatePlayerAction("Move Up");
        Down = CreatePlayerAction("Move Down");

        RotateLeft = CreatePlayerAction("Rotate Left");
        RotateRight = CreatePlayerAction("Rotate Right");
        RotateUp = CreatePlayerAction("Rotate Up");
        RotateDown = CreatePlayerAction("Rotate Down");

        RightBumper = CreatePlayerAction("Right Bumper");
        LeftBumper = CreatePlayerAction("Left Bumper");

        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
        RotateCamera = CreateTwoAxisPlayerAction(RotateLeft, RotateRight, RotateDown, RotateUp);

        Attack = CreatePlayerAction("Basic Attack");
        Aim = CreatePlayerAction("Aim");
        Interact = CreatePlayerAction("Interact");
        Jump = CreatePlayerAction("Jump");

        Escape = CreatePlayerAction("Escape");

        InputManager.OnUpdateDevices += UpdateValues;

        AddControllerBindings();

#if UNITY_ANDROID || UNITY_IOS
#else
        AddKeyboardBindings();
#endif
    }

    public void AddKeyboardBindings()
    {
        Left.AddDefaultBinding(Key.A);
        Right.AddDefaultBinding(Key.D);
        Up.AddDefaultBinding(Key.W);
        Down.AddDefaultBinding(Key.S);

        RotateLeft.AddDefaultBinding(Mouse.NegativeX);
        RotateRight.AddDefaultBinding(Mouse.PositiveX);
        RotateUp.AddDefaultBinding(Mouse.PositiveY);
        RotateDown.AddDefaultBinding(Mouse.NegativeY);

        Attack.AddDefaultBinding(Mouse.LeftButton);
        Aim.AddDefaultBinding(Mouse.RightButton);
        Interact.AddDefaultBinding(Key.E);
        Jump.AddDefaultBinding(Key.Space);

        Escape.AddDefaultBinding(Key.Escape);
    }

    public void AddControllerBindings()
    {
        //sets the bindings if the device is a controller
        Left.AddDefaultBinding(InputControlType.DPadLeft);
        Left.AddDefaultBinding(InputControlType.LeftStickLeft);

        Right.AddDefaultBinding(InputControlType.DPadRight);
        Right.AddDefaultBinding(InputControlType.LeftStickRight);

        Up.AddDefaultBinding(InputControlType.DPadUp);
        Up.AddDefaultBinding(InputControlType.LeftStickUp);

        Down.AddDefaultBinding(InputControlType.DPadDown);
        Down.AddDefaultBinding(InputControlType.LeftStickDown);

        RotateLeft.AddDefaultBinding(InputControlType.RightStickLeft);
        RotateRight.AddDefaultBinding(InputControlType.RightStickRight);
        RotateUp.AddDefaultBinding(InputControlType.RightStickUp);
        RotateDown.AddDefaultBinding(InputControlType.RightStickDown);

        Attack.AddDefaultBinding(InputControlType.RightTrigger);
        Aim.AddDefaultBinding(InputControlType.LeftTrigger);
        Interact.AddDefaultBinding(InputControlType.Action3);
        Jump.AddDefaultBinding(InputControlType.Action1);

        RightBumper.AddDefaultBinding(InputControlType.RightBumper);
        LeftBumper.AddDefaultBinding(InputControlType.LeftBumper);

        Escape.AddDefaultBinding(InputControlType.Start);
    }

    private void UpdateValues(ulong updateTick, float deltaTime)
    {
        this.updateTick = updateTick;
        this.deltaTime = deltaTime;
    }

    public void ForceUpdateMovement(Vector2 value)
    {
        //updating left stick
        if (value.x > 0)
            InputController.InputManager.Right.UpdateWithValue(value.x, updateTick, deltaTime);
        else if (value.x < 0)
            InputController.InputManager.Left.UpdateWithValue(value.x, updateTick, deltaTime);
        if (value.y > 0)
            InputController.InputManager.Up.UpdateWithValue(value.y, updateTick, deltaTime);
        else if (value.y < 0)
            InputController.InputManager.Down.UpdateWithValue(value.y, updateTick, deltaTime);
    }

    public void ForceUpdateCameraRotation(Vector2 value)
    {
        //updating right stick
        if (value.x > 0)
            InputController.InputManager.RotateRight.UpdateWithValue(value.x, updateTick, deltaTime);
        else if (value.x < 0)
            InputController.InputManager.RotateLeft.UpdateWithValue(value.x, updateTick, deltaTime);
        if (value.y > 0)
            InputController.InputManager.RotateUp.UpdateWithValue(value.y, updateTick, deltaTime);
        else if (value.y < 0)
            InputController.InputManager.RotateDown.UpdateWithValue(value.y, updateTick, deltaTime);
    }
}
