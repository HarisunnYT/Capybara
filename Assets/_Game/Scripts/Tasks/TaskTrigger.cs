using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskTrigger : MonoBehaviour
{
    [SerializeField]
    protected int maxTriggerAmount = 1;

    [SerializeField]
    private TaskData[] tasks;

    private int timesTriggered;

    protected void TaskTriggered()
    {
        if (tasks.Length == 0)
        {
            throw new System.Exception("Tasks empty");
        }

        if (timesTriggered < maxTriggerAmount)
        {
            foreach (var task in tasks)
            {
                TaskManager.Instance.UpdateTask(task);
            }
            timesTriggered++;

            Debug.Log("triggered");
        }
    }
}
