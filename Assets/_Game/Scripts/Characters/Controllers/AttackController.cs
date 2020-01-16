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
        //if the two hand can't attack (eg no weapon) try attacking with single hands instead
        if (twoHand.Attack() || leftHand.Attack() || rightHand.Attack())
        {
            AnimationController.SetTrigger("Attack");
        }
    }

    public bool IsHoldingWeapon()
    {
        return twoHand.HoldingWeapon() || leftHand.HoldingWeapon() || rightHand.HoldingWeapon();
    }
}
