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

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;

    private Animator animator;

    private void Awake()
    {
        Instance = this;

        animator = GetComponent<Animator>();
    }

    public void SetAnimatorLayerWeight(AnimatorBodyPartLayer layer, float weight)
    {
        //we add 1 to the layer as 0 is full body which can't be modified
        animator.SetLayerWeight((int)layer + 1, weight);
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
        animator.SetBool(name, result);
    }

    public void SetAnimatorBools(AnimatorBool[] bools)
    {
        foreach(var b in bools)
        {
            SetBool(b.Name, b.Result);
        }
    }
}
