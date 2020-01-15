using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : Interactable
{
    [SerializeField]
    protected VehicleData vehicleData;

    [SerializeField]
    private Transform seatBone;

    private Collider[] colliders;

    protected override void Start()
    {
        base.Start();

        colliders = GetComponents<Collider>();
    }

    public void GetInVehicle(CharacterController characterController)
    {
        CurrentController = characterController;

        characterController.transform.parent = seatBone;
        characterController.transform.localPosition = Vector3.zero;
        characterController.transform.rotation = Quaternion.LookRotation(seatBone.forward, seatBone.up);

        characterController.MovementController.SetKinematic(true);
        characterController.AnimationController.DisableAllAnimationLayers();

        foreach (var col in colliders)
        {
            CurrentController.InteractionController.IgnoreCollisions(col, true);
        }

        if (GameManager.Instance.IsPlayer(CurrentController))
        {
            CameraController.Instance.SetMinMaxDistance(vehicleData.cameraMinDistance, vehicleData.cameraMaxDistance);
        }
    }

    public void GetOutOfVehicle()
    {
        CurrentController.ResetParent();
        CurrentController.MovementController.SetKinematic(false);

        foreach (var col in colliders)
        {
            CurrentController.InteractionController.IgnoreCollisions(col, false);
        }

        if (GameManager.Instance.IsPlayer(CurrentController))
        {
            CameraController.Instance.ResetMinMaxDistance();
        }

        CurrentController.MovementController.SetMovementStyle(MovementStyle.Grounded);
        CurrentController.AnimationController.SetInstantBoneMovement(0);

        CurrentController = null;
    }
}
