using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/Vehicle Data")]
public class VehicleData : ScriptableObject
{
    public float Speed;
    public float MaxAngle;

    [Space()]
    public float cameraMinDistance = 10;
    public float cameraMaxDistance = 15;
}
