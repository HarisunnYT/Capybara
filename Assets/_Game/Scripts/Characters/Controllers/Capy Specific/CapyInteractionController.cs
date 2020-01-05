using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyInteractionController : InteractionController
{
    private void Update()
    {
        if (InputController.InputManager.Interact.WasPressed)
        {
            TryPickUpObject();
        }
    }
}
