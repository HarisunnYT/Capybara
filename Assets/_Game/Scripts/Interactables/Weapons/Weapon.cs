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

    [SerializeField]
    private float attackDelay = 0.2f;

    private float attackTimer = 0;

    protected bool canAttack = false;

    private void Update()
    {
        if (!canAttack && Time.time > attackTimer)
        {
            canAttack = true;
        }
    }

    public virtual bool Attack()
    {
        if (canAttack)
        {
            OnAttack();
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void OnAttack() 
    {
        canAttack = false;
        attackTimer = Time.time + attackDelay;

        CurrentController.AnimationController.SetBoolTrigger("Attack" + ((Hand)CurrentBodyPart).CurrentHandType.ToString(), true);
    }
}
