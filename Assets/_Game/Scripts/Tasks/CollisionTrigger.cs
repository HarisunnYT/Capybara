using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : TaskTrigger
{
    [Space()]
    [SerializeField]
    private bool triggersFromChildrenColliders = false;

    private void Awake()
    {
        if (triggersFromChildrenColliders)
        {
            foreach(var col in transform.GetComponentsInChildren<Collider>())
            {
                col.gameObject.AddComponent<ChildCollisionTrigger>().SetUp(this);
            }
        }
    }

    public void TriggerFromChild()
    {
        TaskTriggered();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CharacterController controller = collision.gameObject.GetComponentInParent<CharacterController>();
        if (controller != null && controller == GameManager.Instance.CapyController)
        {
            TaskTriggered();
        }
    }
}
