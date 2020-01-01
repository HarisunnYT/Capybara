using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/Pickupable Item")]
public class PickupableItemData : ScriptableObject
{
    public BodyPartType ItemSlotType;

    [Space()]
    public Vector3 Position;
    public Vector3 EulerRotation;

    [Space()]
    public MovementData MovementData;

    [Space()]
    public BoneWeight[] BoneWeights;

    public AnimatorBool[] AnimatorBools;

    public float GetWeight(AnimatorBodyPartLayer bodyPartLayer)
    {
        foreach (var part in BoneWeights)
        {
            if (bodyPartLayer == part.bodyPartType)
            {
                return part.Weight;
            }
        }

        return 0;
    }
}
