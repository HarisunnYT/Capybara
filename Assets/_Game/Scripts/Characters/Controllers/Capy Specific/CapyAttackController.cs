using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapyAttackController : AttackController
{
    private void Update()
    {
        if (InputController.InputManager.Attack.WasPressed)
        {
            Attack();
        }
    }
}
