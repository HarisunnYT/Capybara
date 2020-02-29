using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/Vehicle Data")]
public class VehicleData : ScriptableObject
{
    public int StartingHealth = 100;

    public bool DropAllItemsOnEnter = true;

    [Space()]
    public float CameraMinDistance = 10;
    public float CameraMaxDistance = 15;

    public Vector3 CameraOffset = Vector3.one;

    [Space()]
    public BoneWeight[] BoneWeights;
    public AnimatorBool[] AnimatorBools;
}
