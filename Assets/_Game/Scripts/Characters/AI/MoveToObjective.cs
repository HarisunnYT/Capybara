using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/AI/Move To Objective")]
public class MoveToObjective : ObjectiveData
{
    [SerializeField]
    private Vector3 targetPosition;

    public override void BeginObjective(AIController controller)
    {
        base.BeginObjective(controller);

        controller.AIMovementController.SetDestination(targetPosition);
    }
}
