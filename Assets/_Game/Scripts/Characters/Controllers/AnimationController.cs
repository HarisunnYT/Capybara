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

        [System.NonSerialized]
        public Rigidbody[] MovingRigidbodies;
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

    private Dictionary<string, int> bools = new Dictionary<string, int>();
    private Dictionary<string, int> animatorLayers = new Dictionary<string, int>();

    protected override void Awake()
    {
        base.Awake();

        //we don't use this animator, this is just for creating animations
        editorAnimator.enabled = false;
        boneMoveSpeed = originalBoneMoveSpeed;

        for (int i = 0; i < boneLayers.Length; i++)
        {
            boneLayers[i].MovingRigidbodies = new Rigidbody[boneLayers[i].MovingBones.Length];
            for (int x = 0; x < boneLayers[i].MovingBones.Length; x++)
            {
                Rigidbody body = boneLayers[i].MovingBones[x].GetComponent<Rigidbody>();
                if (body)
                {
                    boneLayers[i].MovingRigidbodies[x] = body;
                }
            }
        }

        DisableAllBoneLayers(false);
    }

    private void LateUpdate()
    {
        UpdateBones();
    }

    private void UpdateBones()
    {
        if ((int)MovementController.CurrentMovementState <= (int)MovementState.Stunned && isAnimating)
        {
            for (int L = 0; L < boneLayers.Length; L++)
            {
                if (boneLayers[L].Active)
                {
                    Layer layer = boneLayers[L];
                    for (int i = 0; i < layer.MovingBones.Length; i++)
                    {
                        if (layer.MovingRigidbodies[i] == null || layer.MovingRigidbodies[i].isKinematic)
                        {
                            float time = boneMoveSpeed == float.MaxValue ? boneMoveSpeed : boneMoveSpeed * Time.deltaTime;
                            layer.MovingBones[i].localPosition = Vector3.Lerp(layer.MovingBones[i].localPosition, layer.AnimatingBones[i].localPosition, time);
                            layer.MovingBones[i].localRotation = Quaternion.Lerp(layer.MovingBones[i].localRotation, layer.AnimatingBones[i].localRotation, time);
                        }
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
        SetAnimatorLayerWeight(layer.ToString(), weight);
    }

    public void SetAnimatorLayerWeight(AnimatorBodyPartLayer layer, float weight, float delay)
    {
        StartCoroutine(AnimatorLayerWeightDelay(layer, weight, delay));
    }

    private IEnumerator AnimatorLayerWeightDelay(AnimatorBodyPartLayer layer, float weight, float delay)
    {
        yield return new WaitForSeconds(delay);

        SetAnimatorLayerWeight(layer.ToString(), weight);
    }

    public void SetAnimatorLayerWeight(string layerName, float weight)
    {
        if (weight == 1)
        {
            Animator.SetLayerWeight(Animator.GetLayerIndex(layerName), weight);
            AddAnimatorLayer(layerName);
        }
        else
        {
            RemoveAnimatorLayer(layerName);
        }
    }

    public void SetAnimatorLayerWeights(BoneWeight[] boneWeights)
    {
        foreach(var boneWeight in boneWeights)
        {
            SetAnimatorLayerWeight(boneWeight.bodyPartType, boneWeight.Weight);
        }
    }

    public void InvertAnimatorLayerWeights(BoneWeight[] boneWeights)
    {
        foreach (var boneWeight in boneWeights)
        {
            SetAnimatorLayerWeight(boneWeight.bodyPartType, boneWeight.Weight == 0 ? 1 : 0);
        }
    }

    /// <summary>
    /// Only use if you want to ignore the bool que
    /// </summary>
    /// <param name="name"></param>
    /// <param name="result"></param>
    public void ForceSetBool(string name, bool result)
    {
        Animator.SetBool(name, result);
    }

    public void SetBool(string name, bool result)
    {
        if (result)
        {
            Animator.SetBool(name, result);
            AddBool(name);
        }
        else
        {
            RemoveBool(name);
        }
    }

    public void SetBoolTrigger(string name, bool result)
    {
        StartCoroutine(BoolTrigger(name, result));
    }

    private IEnumerator BoolTrigger(string name, bool result)
    {
        SetBool(name, result);

        yield return new WaitForEndOfFrame();

        SetBool(name, !result);
    }

    public void SetFloat(string name, float value)
    {
        Animator.SetFloat(name, value);
    }

    public void SetTrigger(string name)
    {
        Animator.SetTrigger(name);
    }

    public void SetTrigger(string name, float delay)
    {
        StartCoroutine(TriggerDelay(name, delay));
    }

    private IEnumerator TriggerDelay(string name, float delay)
    {
        yield return new WaitForSeconds(delay);

        SetTrigger(name);
    }

    public void SetAnimatorBools(AnimatorBool[] bools)
    {
        foreach(var b in bools)
        {
            SetBool(b.Name, b.Result);
        }
    }

    public void InvertAnimatorBools(AnimatorBool[] bools)
    {
        foreach (var b in bools)
        {
            SetBool(b.Name, !b.Result);
        }
    }

    public void SetInstantBoneMovement(float duration, System.Action callback = null)
    {
        StartCoroutine(InstantMovementDelay(duration, callback));
    }

    private IEnumerator InstantMovementDelay(float duration, System.Action callback = null)
    {
        boneMoveSpeed = float.MaxValue;

        UpdateBones();

        yield return new WaitForSeconds(duration);

        boneMoveSpeed = originalBoneMoveSpeed;

        yield return new WaitForEndOfFrame();

        callback?.Invoke();
    }

    #endregion

    public bool GetBool(string name)
    {
        return bools.ContainsKey(name);        
    }

    private void AddAnimatorLayer(string layer)
    {
        if (animatorLayers.ContainsKey(name))
        {
            animatorLayers[name]++;
        }
        else
        {
            animatorLayers.Add(name, 1);
        }
    }

    private void RemoveAnimatorLayer(string layer)
    {
        if (animatorLayers.ContainsKey(name) && animatorLayers[name] > 0)
        {
            animatorLayers[name]--;
            if (animatorLayers[name] == 0)
            {
                Animator.SetLayerWeight(Animator.GetLayerIndex(layer), 0);
            }
        }
    }

    private void AddBool(string name)
    {
        if (bools.ContainsKey(name))
        {
            bools[name]++;
        }
        else
        {
            bools.Add(name, 1);
        }
    }

    private void RemoveBool(string name)
    {
        if (bools.ContainsKey(name) && bools[name] > 0)
        {
            bools[name]--;
            if (bools[name] == 0)
            {
                Animator.SetBool(name, false);
            }
        }
    }
}
