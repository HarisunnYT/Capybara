using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/AI/Pick Up Closest Object Objective")]
public class PickUpObjectObjective : ObjectiveData
{
    public override void BeginObjective(AIController controller)
    {
        base.BeginObjective(controller);

        controller.InteractionController.TryPickUpObject();
    }
}
