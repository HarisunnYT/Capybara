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

        if (Debug.isDebugBuild)
        {
            InControl.InputDevice device = InControl.InputManager.ActiveDevice;
            if ((device.LeftTrigger && device.RightTrigger && device.LeftBumper && device.RightBumper) || (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D)) && 
                !CanvasManager.Instance.GetPanel<DebugPanel>().gameObject.activeSelf)
            {
                CanvasManager.Instance.ShowPanel<DebugPanel>();
            }
            if (InputController.InputManager.Escape.WasPressed && CanvasManager.Instance.GetPanel<DebugPanel>().gameObject.activeSelf)
            {
                CanvasManager.Instance.ClosePanel<DebugPanel>();
            }
        }
    }
}
