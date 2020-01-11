using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : Interactable
{
    [SerializeField]
    protected VehicleData vehicleData;

    [SerializeField]
    private Transform seatBone;

    public void GetInVehicle(CharacterController characterController)
    {
        CurrentController = characterController;

        characterController.transform.parent = seatBone;
        characterController.transform.localPosition = Vector3.zero;
        characterController.transform.rotation = Quaternion.LookRotation(seatBone.forward, seatBone.up);

        characterController.MovementController.SetKinematic(true);
        characterController.AnimationController.DisableAllAnimationLayers();

        CurrentController.InteractionController.IgnoreCollisions(collider, true);

        if (GameManager.Instance.IsPlayer(CurrentController))
        {
            CameraController.Instance.SetMinMaxDistance(vehicleData.cameraMinDistance, vehicleData.cameraMaxDistance);
        }
    }

    public void GetOutOfVehicle()
    {
        CurrentController.InteractionController.IgnoreCollisions(collider, false);

        CurrentController.ResetParent();
        CurrentController.MovementController.SetKinematic(false);

        CurrentController.InteractionController.IgnoreCollisions(collider, false);

        if (GameManager.Instance.IsPlayer(CurrentController))
        {
            CameraController.Instance.ResetMinMaxDistance();
        }

        CurrentController = null;
    }
}
