using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : BodyPart
{
    public enum CapyHandType
    {
        Left,
        Right,
        Both
    }

    [SerializeField]
    private CapyHandType handType;
    public CapyHandType HandType { get { return handType; } }

    [Header("Leave unassigned if two handed")]
    [SerializeField]
    private Hand otherHand;

    public override void AssignItem(PickupableItem newItem)
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
        if (HandType != CapyHandType.Both)
        {
            AnimationController.Instance.SetBool(handType == CapyHandType.Right ? "RightHandEquiped" : "LeftHandEquiped", CurrentItemObject != null);
        }
        else
        {
            AnimationController.Instance.SetBool("RightHandEquiped", CurrentItemObject != null);
            AnimationController.Instance.SetBool("LeftHandEquiped", CurrentItemObject != null);
        }
    }

    public bool Attack()
    {
        if (currentItemObject != null && currentItemObject is Weapon)
        {
            return ((Weapon)currentItemObject).Attack();
        }

        return false;
    }
}
