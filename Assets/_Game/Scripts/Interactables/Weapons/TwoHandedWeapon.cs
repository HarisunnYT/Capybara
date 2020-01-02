using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandedWeapon : Weapon
{
    public override bool Attack()
    {
        if (AnimationController.Instance.Animator.GetCurrentAnimatorStateInfo(AnimationController.Instance.Animator.GetLayerIndex("Both Arms Layer")).tagHash == Animator.StringToHash("Attacking"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
