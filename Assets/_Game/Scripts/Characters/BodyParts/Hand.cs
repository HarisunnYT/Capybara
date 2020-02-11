using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : BodyPart
{
    public enum HandType
    {
        Left,
        Right,
        Both
    }

    [SerializeField]
    private HandType currentHandType;
    public HandType CurrentHandType { get { return currentHandType; } }

    [Header("Leave unassigned if two handed")]
    [SerializeField]
    private Hand otherHand;

    public override void PickUpItem(PickupableItem newItem)
    {
        //try and chuck it on other hand first
        if (otherHand != null && otherHand.CurrentItemObject == null)
        {
            otherHand.ConnectItem(newItem);
            return;
        }
        else
        {
            DropCurrentItem();
        }

        ConnectItem(newItem);
    }

    public override void DropCurrentItem()
    {
        base.DropCurrentItem();

        SetAnims();
    }

    public override void ConnectItem(PickupableItem newItem)
    {
        base.ConnectItem(newItem);

        SetAnims();
    }

    private void SetAnims()
    {
        if (CurrentHandType != HandType.Both)
        {
            controller.AnimationController.SetBool(currentHandType == HandType.Right ? "RightHandEquiped" : "LeftHandEquiped", CurrentItemObject != null);
        }
        else
        {
            controller.AnimationController.SetBool("RightHandEquiped", CurrentItemObject != null);
            controller.AnimationController.SetBool("LeftHandEquiped", CurrentItemObject != null);
        }
    }

    public bool Attack()
    {
        if (HoldingWeapon())
        {
            return ((Weapon)currentItemObject).Attack();
        }

        return false;
    }

    public bool HoldingWeapon(WeaponType weaponType = WeaponType.None)
    {
        if (currentItemObject != null && currentItemObject is Weapon)
        {
            if (weaponType == WeaponType.None)
            {
                return true;
            }
            else
            {
                return ((Weapon)currentItemObject).WeaponType == weaponType;
            }
        }

        return false;
    }

    public WeaponType GetWeaponType()
    {
        if (HoldingWeapon())
        {
            return ((Weapon)currentItemObject).WeaponType;
        }

        return WeaponType.None;
    }
}
