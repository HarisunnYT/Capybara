using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeTrap : MonoBehaviour
{
    [SerializeField]
    private float minFallVelocity;

    [SerializeField]
    private float exaggerationForceMultiplier = 1;

    private ObiSolver solver;
    private Obi.ObiSolver.ObiCollisionEventArgs collisionEvent;

    private void Awake()
    {
        solver = GetComponent<Obi.ObiSolver>();
    }

    private void OnEnable()
    {
        solver.OnCollision += Solver_OnCollision;
    }

    private void OnDisable()
    {
        solver.OnCollision -= Solver_OnCollision;
    }

    private void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        foreach (Oni.Contact contact in e.contacts)
        {
            // this one is an actual collision:
            if (contact.distance < 0.01)
            {
                Component collider;
                if (ObiCollider.idToCollider.TryGetValue(contact.other, out collider))
                {
                    CharacterController controller = collider.GetComponent<CharacterController>();
                    if (controller && controller.MovementController.MainBody.velocity.magnitude >= minFallVelocity)
                    {
                        Vector3 direction = controller.MovementController.MainBody.velocity.normalized;
                        float force = controller.MovementController.MainBody.velocity.magnitude;

                        controller.RagdollController.SetRagdoll(true);
                        controller.RagdollController.AddForceToBodies(direction, force * exaggerationForceMultiplier);
                    }
                }
            }
        }
    }
}
