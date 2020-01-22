using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Ranged,
    Melee
}

public class Weapon : PickupableItem
{
    [SerializeField]
    private WeaponType weaponType;
    public WeaponType WeaponType { get { return weaponType; } }

    public virtual bool Attack()
    {
        OnAttack();

        return true;
    }

    protected virtual void OnAttack() { }
}
