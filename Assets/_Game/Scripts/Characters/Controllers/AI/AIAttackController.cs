using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackController : AttackController
{
    [SerializeField]
    private Transform hips;

    [SerializeField]
    private Transform head;

    [SerializeField]
    private float maxAimHipRotation = 50;

    private float timer = 0;

    public void ShootAtTarget(Transform target, float duration)
    {
        AnimationController.SetAnimatorLayerWeight("UpperBody", 1);
        AnimationController.SetBool("Aiming", true);
        AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, true);

        Vector3 direction = target.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        //if the direction is over max, it'll rotate the whole body instead
        if (angle > maxAimHipRotation)
        {
            GetController<AIMovementController>().RotateTowardsTransform(target, duration);
            hips.transform.DOLocalRotate(Quaternion.identity.eulerAngles, duration).OnComplete(() =>
            {
                Shoot();
            });
        }
        else
        {
            hips.transform.DORotate(targetRotation.eulerAngles, duration).OnComplete(() =>
            {
                Shoot();
            });
        }

        head.transform.DORotate(targetRotation.eulerAngles, duration / 2);
    }

    private void Shoot()
    {
        Attack();

        AnimationController.SetBool("Aiming", false);
        AnimationController.DisableBoneLayer(SimplifiedBodyLayer.UpperBody, false);
    }

    public bool IsHoldingWeapon()
    {
        if ((leftHand.CurrentItemObject != null && leftHand.CurrentItemObject is Weapon) || (rightHand.CurrentItemObject != null && rightHand.CurrentItemObject is Weapon)
           || twoHand.CurrentItemObject != null && twoHand.CurrentItemObject is Weapon)
        {
            return true;
        }

        return false;
    }
}
