﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : PickupableItem
{
    public virtual bool Attack()
    {
        OnAttack();

        return true;
    }

    protected virtual void OnAttack() { }
}
