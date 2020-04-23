using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionTrigger : MonoBehaviour
{
    private CollisionTrigger collisionTrigger;

    public void SetUp(CollisionTrigger collisionTrigger)
    {
        this.collisionTrigger = collisionTrigger;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CharacterController controller = collision.gameObject.GetComponentInParent<CharacterController>();
        if (controller != null && controller == GameManager.Instance.CapyController)
        {
            collisionTrigger.TriggerFromChild();
        }
    }
}
