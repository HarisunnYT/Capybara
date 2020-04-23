using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrigger : TaskTrigger
{
    private void Awake()
    {
        Interactable interactable = GetComponent<Interactable>();
        if (interactable)
        {
            interactable.OnInteracted += TaskTriggered;
        }
        else
        {
            throw new System.Exception("Object does not contain interactable");
        }
    }
}
