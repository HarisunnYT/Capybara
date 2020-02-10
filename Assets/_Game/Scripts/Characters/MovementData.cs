using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BoneWeight
{
    public AnimatorBodyPartLayer bodyPartType;
    [Range(0, 1)]
    public float Weight;
}

[CreateAssetMenu(menuName = "Capybara/Movement Data")]
public class MovementData : ScriptableObject
{
    public MovementStyle MovementStyle;

    [Space()]
    public float MovementSpeedMultiplier = 1;
    public Vector3 GravityMultiplier = new Vector3(0, 1, 0);

    [Space()]
    public Vector3 CameraOffset;

    [Space()]
    public BoneWeight[] BoneWeights;

    public AnimatorBool[] AnimatorBools;

    public float GetWeight(AnimatorBodyPartLayer bodyPartLayer)
    {
        foreach(var part in BoneWeights)
        {
            if (bodyPartLayer == part.bodyPartType)
            {
                return part.Weight;
            }
        }

        return 0;
    }

    public bool ContainsWeight(AnimatorBodyPartLayer bodyPartLayer)
    {
        return GetWeight(bodyPartLayer) != 0;
    }
}
