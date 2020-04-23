using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableTrigger : TaskTrigger
{
    private void Awake()
    {
        Interactable interactable = GetComponent<Interactable>();
        interactable.OnInteracted += TaskTriggered;
    }
}
