using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyAimController : Controller
{
    enum AimingState
    {
        None,
        ToAim,
        Aiming,
        FromAim
    }

    [SerializeField]
    private float aimRotationSpeed = 15;

    [Header("Camera")]
    [SerializeField]
    private Vector3 cameraOffset;

    [SerializeField]
    private float cameraDistance;

    [SerializeField]
    private float lerpDuration = 1;

    private Vector3 offsetLerp;
    private float distanceLerp;

    private float distanceTarget;

    private Quaternion fromRotation;
    private Quaternion targetRotation;

    private float timer = 0;

    private AimingState currentState;

    private void Update()
    {
        if (AttackController.IsHoldingWeapon())
        {
            AimLerp();
        }
    }

    private void LateUpdate()
    {
        if (currentState == AimingState.Aiming)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, CameraController.Instance.transform.rotation, aimRotationSpeed * Time.deltaTime);
        }
    }

    private void AimLerp()
    {
        if (InputController.InputManager.Aim.WasPressed)
        {
            currentState = AimingState.ToAim;
            timer = 0;

            offsetLerp = CameraController.Instance.offset;
            distanceLerp = CameraController.Instance.distance;

            CameraController.Instance.SetMinMaxDistance(float.MinValue, float.MaxValue);
            AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, true);

            CanvasManager.Instance.ShowCrosshair(true);
        }
        else if (InputController.InputManager.Aim.WasReleased)
        {
            currentState = AimingState.FromAim;
            timer = 0;

            offsetLerp = CameraController.Instance.offset;
            distanceLerp = CameraController.Instance.distance;

            distanceTarget = CameraController.Instance.distance + CameraController.Instance.distanceTarget;

            CameraController.Instance.SetMinMaxDistance(float.MinValue, float.MaxValue);
            AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, false);

            fromRotation = transform.rotation;
            targetRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));

            CanvasManager.Instance.ShowCrosshair(false);
        }

        if (currentState == AimingState.ToAim)
        {
            timer += Time.deltaTime;
            float normalisedTime = timer / lerpDuration;

            Vector3 offset = Vector3.Lerp(offsetLerp, cameraOffset, normalisedTime);
            float distance = Mathf.Lerp(distanceLerp, cameraDistance, normalisedTime);

            CameraController.Instance.SetOffset(offset);
            CameraController.Instance.distance = distance;

            if (normalisedTime > 0.99f)
            {
                CameraController.Instance.SetMinMaxDistance(distance, distance);
                currentState = AimingState.Aiming;
            }
        }
        else if (currentState == AimingState.FromAim)
        {
            timer += Time.deltaTime;
            float normalisedTime = timer / lerpDuration;

            Vector3 offset = Vector3.Lerp(offsetLerp, CameraController.Instance.OriginalOffset, normalisedTime);
            float distance = Mathf.Lerp(distanceLerp, distanceTarget, normalisedTime);

            CameraController.Instance.SetOffset(offset);
            CameraController.Instance.distance = distance;

            transform.rotation = Quaternion.Lerp(fromRotation, targetRotation, normalisedTime);

            if (normalisedTime > 0.99f)
            {
                CameraController.Instance.ResetMinMaxDistance();
                currentState = AimingState.None;
            }
        }
    }
}
