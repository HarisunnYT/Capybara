using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRagdollController : RagdollController
{
    protected override void OnRagdollBegin()
    {
        base.OnRagdollBegin();

        InteractionController.DropAllItems();
    }
}
