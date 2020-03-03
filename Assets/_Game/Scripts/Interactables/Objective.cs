using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : Consumable
{
    protected override void OnPickedUp()
    {
        base.OnPickedUp();

        LevelLoader.Instance.LoadMenu();
    }
}
