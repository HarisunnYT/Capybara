using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHandedWeapon : Weapon
{
    public override bool Attack()
    {
        Animator animator = CurrentController.AnimationController.Animator;
        string layerName = ((Hand)CurrentBodyPart).CurrentHandType == Hand.HandType.Right ? "Right Hand Layer" : "Left Hand Layer";

        if (animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex(layerName)).tagHash == Animator.StringToHash("Attacking"))
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
