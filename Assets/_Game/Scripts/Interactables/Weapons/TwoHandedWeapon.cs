using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoHandedWeapon : Weapon
{
    public override bool Attack()
    {
        Animator animator = CurrentController.AnimationController.Animator;
        if (animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("BothArms")).tagHash == Animator.StringToHash("Attacking"))
        {
            return false;
        }
        else
        {
            OnAttack();
            return true;
        }
    }
}
