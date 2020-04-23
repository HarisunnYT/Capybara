using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableObject))]
public class DestroyableTask : TaskTrigger
{
    private void Awake()
    {
        DamagableObject damagableObject = GetComponent<DamagableObject>();
        damagableObject.OnDestroyedEvent += TaskTriggered;
    }
}
