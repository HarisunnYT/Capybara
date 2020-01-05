using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/AI/Objective Data")]
public class ObjectiveData : ScriptableObject
{
    protected AIController currentController;

    public virtual void BeginObjective(AIController controller)
    {

    }

    public virtual void CancelObjective()
    {

    }

    public virtual bool HasCompleteObjective()
    {
        return true;
    }
}
