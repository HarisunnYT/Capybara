using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimatorBodyPartLayer
{
    Legs,
    BothArms,
    LeftArm,
    RightArm,
    Head
}

[System.Serializable]
public struct AnimatorBool
{
    public string Name;
    public bool Result;
}

public class AnimationController : Controller
{
    [SerializeField]
    private Animator boneAnimator;
    public Animator Animator { get { return boneAnimator; } }

    [SerializeField]
    private Transform[] movingBones;
    public Transform[] MovingBones { get { return movingBones; } }

    [SerializeField]
    private Transform[] animationBones;
    public Transform[] AnimationBones { get { return animationBones; } }

    private float lerpDuration = 10f;
    private float timer = 0;

    private void LateUpdate()
    {
        if (MovementController.CurrentMovementState != MovementState.Ragdoll)
        {
            for (int i = 0; i < movingBones.Length; i++)
            {
                movingBones[i].localPosition = animationBones[i].localPosition;
                movingBones[i].localRotation = animationBones[i].localRotation;
            }
        }
    }

    public void SetAnimatorLayerWeight(AnimatorBodyPartLayer layer, float weight)
    {
        //we add 1 to the layer as 0 is full body which can't be modified
        Animator.SetLayerWeight((int)layer + 1, weight);
    }

    public void SetAnimatorLayerWeights(BoneWeight[] boneWeights)
    {
        foreach(var boneWeight in boneWeights)
        {
            SetAnimatorLayerWeight(boneWeight.bodyPartType, boneWeight.Weight);
        }
    }

    public void SetBool(string name, bool result)
    {
        Animator.SetBool(name, result);
    }

    public void SetFloat(string name, float value)
    {
        Animator.SetFloat(name, value);
    }

    public void SetTrigger(string name)
    {
        Animator.SetTrigger(name);
    }

    public void SetAnimatorBools(AnimatorBool[] bools)
    {
        foreach(var b in bools)
        {
            SetBool(b.Name, b.Result);
        }
    }
}
