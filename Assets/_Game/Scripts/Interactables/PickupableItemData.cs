using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/Pickupable Item")]
public class PickupableItemData : ScriptableObject
{
    public BodyPartType ItemSlotType;

    [Space()]
    public Vector3 EulerRotation;
    public Vector3 Position;

    [Space()]
    public MovementData MovementData;
}
