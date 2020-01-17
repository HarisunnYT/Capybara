using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController CharacterController { get; private set; }
    public AnimationController AnimationController { get; private set; }
    public RagdollController RagdollController { get; private set; }
    public AttackController AttackController { get; private set; }
    public CollisionController CollisionController { get; private set; }
    public MovementController MovementController { get; private set; }
    public InteractionController InteractionController { get; private set; }
    public AimController AimController { get; private set; }

    private List<Controller> Controllers = new List<Controller>();

    protected virtual void Awake()
    {
        Controllers.Add(CharacterController = GetComponent<CharacterController>());
        Controllers.Add(AnimationController = GetComponent<AnimationController>());
        Controllers.Add(RagdollController = GetComponent<RagdollController>());
        Controllers.Add(AttackController = GetComponent<AttackController>());
        Controllers.Add(CollisionController = GetComponent<CollisionController>());
        Controllers.Add(MovementController = GetComponent<MovementController>());
        Controllers.Add(InteractionController = GetComponent<InteractionController>());
        Controllers.Add(AimController = GetComponent<AimController>());
    }

    public T GetController<T>() where T : Controller
    {
        foreach (var controller in Controllers)
        {
            if (typeof(T).BaseType == controller.GetType().BaseType)
            {
                return controller as T;
            }
        }

        return null; 
    }
}
