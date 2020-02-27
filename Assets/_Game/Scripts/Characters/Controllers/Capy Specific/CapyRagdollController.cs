using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyRagdollController : RagdollController
{
    protected override void OnRagdollBegin()
    {
        //set camera target
        CameraController.Instance.SetTarget(spineBody.transform, true);
    }

    protected override void OnRagdollEnd()
    {
        //set camera target
        CameraController.Instance.SetTarget(transform, false);

        AnimationController.SetAnimatorLayerWeight(AnimatorBodyPartLayer.Head, 1);
        AnimationController.SetTrigger("HeadShake");

        AnimationController.SetAnimatorLayerWeight(AnimatorBodyPartLayer.Head, 0, 1.5f);
    }
}
