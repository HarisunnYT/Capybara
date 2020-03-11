using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : Controller
{
    [SerializeField]
    protected Hand leftHand;

    [SerializeField]
    protected Hand rightHand;

    [SerializeField]
    protected Hand twoHand;

    protected void Attack()
    {
        bool attacked = true;

        //if the two hand can't attack (eg no weapon) try attacking with single hands instead
        if (twoHand.Attack()) 
        {
            //empty
        }
        else if (leftHand.Attack())
        {
            AnimationController.Animator.SetInteger("CurrentHand", 1);
        }
        else if (rightHand.Attack())
        {
            AnimationController.Animator.SetInteger("CurrentHand", 2);
        }
        else
        {
            attacked = false;
        }

        if (attacked)
        {
            AnimationController.SetTrigger("Attack");
        }
    }

    public bool IsHoldingWeapon(WeaponType weaponType = WeaponType.None)
    {
        return twoHand.HoldingWeapon(weaponType) || leftHand.HoldingWeapon(weaponType) || rightHand.HoldingWeapon(weaponType);
    }
}
