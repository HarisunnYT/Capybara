using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/Pickupable Item")]
public class PickupableItemData : ScriptableObject
{
    [System.Serializable]
    public class ItemData
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
    }

    [EnumList(typeof(CharacterType)), SerializeField]
    public ItemData[] DataPerCharacter;

    private ItemData GetData(CharacterType characterType)
    {
        return DataPerCharacter[(int)characterType];
    }

    public float GetWeight(AnimatorBodyPartLayer bodyPartLayer, CharacterType characterType)
    {
        foreach (var part in GetData(characterType).BoneWeights)
        {
            if (bodyPartLayer == part.bodyPartType)
            {
                return part.Weight;
            }
        }

        return 0;
    }

    public Vector3 GetPosition(CharacterType characterType)
    {
        return GetData(characterType).Position;
    }

    public Vector3 GetEulerRotation(CharacterType characterType)
    {
        return GetData(characterType).EulerRotation;
    }

    public BoneWeight[] GetBoneWeights(CharacterType characterType)
    {
        return GetData(characterType).BoneWeights;
    }

    public AnimatorBool[] GetAnimatorBools(CharacterType characterType)
    {
        return GetData(characterType).AnimatorBools;
    }

    public MovementData GetMovementData(CharacterType characterType)
    {
        return GetData(characterType).MovementData;
    }

    public BodyPartType GetBodyPartSlotType(CharacterType characterType)
    {
        return GetData(characterType).ItemSlotType;
    }
}
