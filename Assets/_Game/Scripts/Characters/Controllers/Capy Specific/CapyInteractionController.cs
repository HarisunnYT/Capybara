using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyInteractionController : InteractionController
{
    private void Update()
    {
        if (InputController.InputManager.Interact.WasPressed)
        {
            if (InteractionController.CurrentVehicle != null)
            {
                CurrentVehicle.GetOutOfVehicle();
            }
            else
            {
                TryPickUpObject(true);
            }
        }
    }
}
