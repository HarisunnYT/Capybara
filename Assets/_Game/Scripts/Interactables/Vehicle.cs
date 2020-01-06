using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : Interactable
{
    [SerializeField]
    private VehicleData vehicleData;

    [SerializeField]
    private Transform seatBone;

    public void GetInVehicle(CharacterController characterController)
    {
        CurrentController = characterController;

        characterController.transform.parent = seatBone;
        characterController.transform.localPosition = Vector3.zero;
        characterController.transform.rotation = Quaternion.LookRotation(seatBone.forward, seatBone.up);

        characterController.MovementController.SetKinematic(true);
        characterController.AnimationController.DisableAllLayers();

        CurrentController.InteractionController.IgnoreCollisions(collider, true);
    }

    public void GetOutOfVehicle()
    {
        CurrentController.InteractionController.IgnoreCollisions(collider, false);
    }
}
