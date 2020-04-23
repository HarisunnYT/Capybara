using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableTask : TaskTrigger
{
    private void Awake()
    {
        DamagableObject damagableObject = GetComponent<DamagableObject>();
        damagableObject.OnDamagedEvent += TaskTriggered;
    }
}
