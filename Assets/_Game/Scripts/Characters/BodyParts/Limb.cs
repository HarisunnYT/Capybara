using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] bodies;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
    }

    public void RagdollForDuration(float duration)
    {
        Ragdoll(true);

        StartCoroutine(RagdollDelay(duration));
    }

    private IEnumerator RagdollDelay(float duration)
    {
        yield return new WaitForSeconds(duration);

        Ragdoll(false);
    }

    private void Ragdoll(bool ragdoll)
    {
        if ((int)characterController.MovementController.CurrentMovementState >= (int)MovementState.Ragdoll)
            return;

        foreach (var body in bodies)
        {
            body.isKinematic = !ragdoll;
        }
    }

    public bool ContainsBody(Rigidbody body)
    {
        foreach (var bod in bodies)
        {
            if (bod == body)
            {
                return true;
            }
        }

        return false;
    }

    public void AddForceToBodies(Vector3 force)
    {
        if ((int)characterController.MovementController.CurrentMovementState >= (int)MovementState.Ragdoll)
            return;

        foreach (var bod in bodies)
        {
            bod.AddForce(force, ForceMode.Impulse);
        }
    }
}
