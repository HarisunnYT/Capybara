using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : TaskTrigger
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.GetComponentInParent<CharacterController>();
        if (controller != null && controller == GameManager.Instance.CapyController)
        {
            TaskTriggered();
        }
    }
}
