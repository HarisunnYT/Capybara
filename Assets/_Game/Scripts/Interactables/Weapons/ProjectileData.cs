using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public int Damage = 1;

    [Space()]
    public float ForceMultiplier = 1;
    public float KnockBackForceMultiplier = 1;

    [Space()]

    public GameObject ProjectilePrefab;
}
