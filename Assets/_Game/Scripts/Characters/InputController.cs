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

    public PlayerTwoAxisAction Move;

    //button presses
    public PlayerAction Attack;
    public PlayerAction Aim;
    public PlayerAction Interact;

    public InputProfile()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Up = CreatePlayerAction("Move Up");
        Down = CreatePlayerAction("Move Down");

        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);

        Attack = CreatePlayerAction("Basic Attack");
        Aim = CreatePlayerAction("Aim");
        Interact = CreatePlayerAction("Interact");

        AddKeyboardBindings();
        AddControllerBindings();
    }

    public void AddKeyboardBindings()
    {
        Left.AddDefaultBinding(Key.A);
        Right.AddDefaultBinding(Key.D);
        Up.AddDefaultBinding(Key.W);
        Down.AddDefaultBinding(Key.S);

        Attack.AddDefaultBinding(Mouse.LeftButton);
        Aim.AddDefaultBinding(Mouse.RightButton);
        Interact.AddDefaultBinding(Key.E);
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

        Attack.AddDefaultBinding(InputControlType.RightTrigger);
        Aim.AddDefaultBinding(InputControlType.LeftTrigger);
        Interact.AddDefaultBinding(InputControlType.Action2);
    }
}
