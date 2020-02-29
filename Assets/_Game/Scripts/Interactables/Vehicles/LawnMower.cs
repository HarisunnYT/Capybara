using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMower : Vehicle
{
    [SerializeField]
    private SpringJoint leftHandBone;

    [SerializeField]
    private SpringJoint rightHandBone;

    protected override IEnumerator GetInVehicleIE()
    {
        yield return base.GetInVehicleIE();

        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, true);

        yield return new WaitForEndOfFrame();

        Rigidbody leftHand = CurrentController.RagdollController.LeftHandBones[CurrentController.RagdollController.LeftHandBones.Length - 1];
        leftHand.isKinematic = true;
        leftHand.transform.position = leftHandBone.transform.position;

        Rigidbody rightHand = CurrentController.RagdollController.RightHandBones[CurrentController.RagdollController.RightHandBones.Length - 1];
        rightHand.isKinematic = true;
        rightHand.transform.position = rightHandBone.transform.position;

        yield return new WaitForEndOfFrame();

        CurrentController.RagdollController.RagdollArms(true);

        rightHandBone.connectedBody = rightHand;
        rightHandBone.connectedAnchor = rightHand.transform.position - rightHandBone.transform.position;
        rightHand.isKinematic = false;

        leftHandBone.connectedBody = leftHand;
        leftHandBone.connectedAnchor = leftHand.transform.position - leftHandBone.transform.position;
        leftHand.isKinematic = false;

        CurrentController.MovementController.SetMovementStyle(MovementStyle.Normal);
        CurrentController.MovementController.SetKinematic(false);
    }

    public override void GetOutOfVehicle()
    {
        rightHandBone.connectedBody = null;
        leftHandBone.connectedBody = null;

        CurrentController.RagdollController.RagdollArms(false);

        CurrentController.AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, false);

        base.GetOutOfVehicle();
    }
}
