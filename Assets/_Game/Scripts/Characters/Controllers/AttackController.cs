using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : Controller
{
    [SerializeField]
    private Hand leftHand;

    [SerializeField]
    private Hand rightHand;

    [SerializeField]
    private Hand twoHand;

    protected void Attack()
    {
        //if the two hand can't attack (eg no weapon) try attacking with single hands instead
        if (twoHand.Attack() || leftHand.Attack() || rightHand.Attack())
        {
            AnimationController.SetTrigger("Attack");
        }
    }
}
