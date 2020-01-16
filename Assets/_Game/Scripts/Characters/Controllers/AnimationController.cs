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

public enum SimplifiedBodyLayer
{
    LowerBody,
    UpperBody
}

[System.Serializable]
public struct AnimatorBool
{
    public string Name;
    public bool Result;
}

public class AnimationController : Controller
{
    [System.Serializable]
    struct Layer
    {
        [HideInInspector]
        public bool Active;

        public Transform[] MovingBones;
        public Transform[] AnimatingBones;
    }

    [SerializeField]
    private Animator editorAnimator;

    [SerializeField]
    private Animator boneAnimator;
    public Animator Animator { get { return boneAnimator; } }

    [Space()]
    [SerializeField]
    private Transform hips;
    public Transform Hips { get { return hips; } }

    [Space()]
    [SerializeField]
    private Transform[] movingBones;
    public Transform[] MovingBones { get { return movingBones; } }

    [SerializeField]
    private Transform[] animationBones;
    public Transform[] AnimationBones { get { return animationBones; } }

    [EnumList(typeof(SimplifiedBodyLayer)), SerializeField]
    private Layer[] boneLayers;

    private float lerpDuration = 10f;
    private float timer = 0;

    private bool isAnimating = true;
    private float originalBoneMoveSpeed = 10;

    private float boneMoveSpeed;

    protected override void Awake()
    {
        base.Awake();

        //we don't use this animator, this is just for creating animations
        editorAnimator.enabled = false;
        boneMoveSpeed = originalBoneMoveSpeed;

        DisableAllBoneLayers(false);
    }

    private void LateUpdate()
    {
        if ((int)MovementController.CurrentMovementState <= (int)MovementState.Stunned && isAnimating)
        {
            foreach(var layer in boneLayers)
            {
                if (layer.Active)
                {
                    for (int i = 0; i < layer.MovingBones.Length; i++)
                    {
                        layer.MovingBones[i].localPosition = Vector3.Lerp(layer.MovingBones[i].localPosition, layer.AnimatingBones[i].localPosition, boneMoveSpeed * Time.deltaTime);
                        layer.MovingBones[i].localRotation = Quaternion.Lerp(layer.MovingBones[i].localRotation, layer.AnimatingBones[i].localRotation, boneMoveSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    public void DisableAllBoneLayers(bool disable)
    {
        for (int i = 0; i < boneLayers.Length; i++)
        {
            boneLayers[i].Active = !disable;
        }
    }

    public void DisableBoneLayer(SimplifiedBodyLayer bodyLayer, bool disable)
    {
        for (int i = 0; i < boneLayers.Length; i++)
        {
            if (i == (int)bodyLayer)
            {
                boneLayers[i].Active = !disable;
            }
        }
    }

    public void DisableAllAnimationLayers()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(AnimatorBodyPartLayer)).Length; i++)
        {
            SetAnimatorLayerWeight((AnimatorBodyPartLayer)i, 0);
        }
    }

    public void DisableAnimation(bool disable)
    {
        isAnimating = !disable;
    }

    #region Setters

    public void SetAnimatorLayerWeight(AnimatorBodyPartLayer layer, float weight)
    {
        Animator.SetLayerWeight(Animator.GetLayerIndex(layer.ToString()), weight);
    }

    public void SetAnimatorLayerWeight(string layerName, float weight)
    {
        Animator.SetLayerWeight(Animator.GetLayerIndex(layerName), weight);
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

    #endregion

    public void SetInstantBoneMovement(float duration)
    {
        StartCoroutine(InstantMovementDelay(duration));
    }

    private IEnumerator InstantMovementDelay(float duration)
    {
        boneMoveSpeed = float.MaxValue;

        yield return new WaitForSeconds(duration);

        boneMoveSpeed = originalBoneMoveSpeed;
    }
}
