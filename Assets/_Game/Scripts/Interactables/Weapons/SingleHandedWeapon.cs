using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHandedWeapon : Weapon
{
    public override bool Attack()
    {
        string layerName = ((Hand)CurrentBodyPart).HandType == Hand.CapyHandType.Right ? "Right Hand Layer" : "Left Hand Layer";
        if (AnimationController.Instance.Animator.GetCurrentAnimatorStateInfo(AnimationController.Instance.Animator.GetLayerIndex(layerName)).tagHash == Animator.StringToHash("Attacking"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
