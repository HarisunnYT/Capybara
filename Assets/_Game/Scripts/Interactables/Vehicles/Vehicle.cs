using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : Interactable
{
    [SerializeField]
    protected VehicleData vehicleData;

    [SerializeField]
    private LayerMask causesDamageLayers;

    [SerializeField]
    private Rigidbody[] breakableParts;

    [Space()]
    [SerializeField]
    private Transform seatBone;

    [SerializeField]
    private Collider[] carliders;

    protected float health;

    private void Start()
    {
        health = vehicleData.StartingHealth;
    }
       
    public void GetInVehicle(CharacterController characterController)
    {
        CurrentController = characterController;

        StartCoroutine(GetInVehicleIE());
    }

    protected virtual IEnumerator GetInVehicleIE()
    {
        InputController.InputManager.ForceUpdateMovement(Vector2.zero);
        CurrentController.MovementController.MainBody.velocity = Vector3.zero;

        CurrentController.AnimationController.SetInstantBoneMovement(1);
        CurrentController.AnimationController.DisableAllAnimationLayers();

        yield return new WaitForEndOfFrame();

        CurrentController.MovementController.SetMovementStyle(MovementStyle.Driving);

        yield return new WaitForEndOfFrame();

        CurrentController.transform.parent = seatBone;
        CurrentController.transform.localPosition = Vector3.zero;
        CurrentController.transform.rotation = Quaternion.LookRotation(seatBone.forward, seatBone.up);

        CurrentController.MovementController.SetKinematic(true);

        foreach (var col in carliders)
        {
            CurrentController.InteractionController.IgnoreCollisions(col, true);
        }

        if (GameManager.Instance.IsPlayer(CurrentController))
        {
            CameraController.Instance.SetMinMaxDistance(vehicleData.cameraMinDistance, vehicleData.cameraMaxDistance);
        }
    }

    public virtual void GetOutOfVehicle()
    {
        CurrentController.ResetParent();
        CurrentController.MovementController.SetKinematic(false);

        foreach (var col in carliders)
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

    protected virtual void UpdateParts()
    {
        float healthPerPart = vehicleData.StartingHealth / (breakableParts.Length + 1);
        for (int i = 0; i < breakableParts.Length; i++)
        {
            if (health < vehicleData.StartingHealth - (healthPerPart * (i + 1)))
            {
                breakableParts[i].isKinematic = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Util.CheckInsideLayer(causesDamageLayers, collision.gameObject.layer))
        {
            health -= collision.relativeVelocity.magnitude;
            UpdateParts();
        }
    }
}
