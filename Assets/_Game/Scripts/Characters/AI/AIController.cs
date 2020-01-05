using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CharacterController
{
    public AIMovementController AIMovementController { get { return MovementController as AIMovementController; } }

    private ObjectiveData currentObjective;

    public void BeginObjective(ObjectiveData objective)
    {
        if (currentObjective == objective)
        {
            return;
        }

        currentObjective = objective;
        currentObjective.BeginObjective(this);
    }

    public void CancelObjective(ObjectiveData objective)
    {
        currentObjective.CancelObjective();
        currentObjective = null;
    }

    public bool HasCurrentObjectiveCompleted()
    {
        return currentObjective.HasCompleteObjective();
    }
}
