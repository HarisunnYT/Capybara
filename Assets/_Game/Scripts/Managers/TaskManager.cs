using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    TaskData[] tasks;

    Dictionary<int, int> taskDictionary;

    [SerializeField]
    Animator taskPromptUI_Animator;
    [SerializeField]
    Image taskPromptUI_TaskIcon;
    [SerializeField]
    TextMeshProUGUI taskPromptUI_TaskTitle;
    [SerializeField]
    TextMeshProUGUI taskPromptUI_TaskDesc;
    [SerializeField]
    TextMeshProUGUI taskPromptUI_CurrencyReward;


    // Start is called before the first frame update
    void Start()
    {
        GenerateDictionary();
        UpdateTask(0);
    }

    void GenerateDictionary()
    {
        taskDictionary = new Dictionary<int, int>();
    }

    //Called when an event happens in the game to progress a task, passes though the ID of the task that is to be progressed
    void UpdateTask(int taskID)
    {
        foreach (TaskData task in tasks)
        {
            if (taskID == task.taskID)
            {
                //If the taskDictionary does not yet have an entry for this task, create it and set its value to 1.
                if (!taskDictionary.ContainsKey(taskID))
                {
                    taskDictionary.Add(taskID, 1);
                }

                //If the taskDictionary DOES have an existing entry for this task, increment it by 1. 
                else
                {
                    taskDictionary[taskID] = taskDictionary[taskID]++;
                }

                //If the threshold for the task has been reached (per the value in the dictionary) then save that data to player prefs to perminantly unlock it and then go and do the fanfare of the task being completed! 
                //NOTE: Have used == here instead of => as I can see instances where the number could go beyond the threshold and activate the task unlocking process multiple times
                if (taskDictionary[taskID] == task.taskThreshold) 
                {
                    SaveTaskData(task);
                    TaskCompleted(task);

                    return;
                }

                //If the Task Type is global, we also want it to persist over sessions so we also save it to player prefs.
                if (task.taskType == TaskData.TaskType.Global)
                {
                    SaveTaskData(task);
                }
                return;
            }
            return;
        }
    }

    void SaveTaskData(TaskData task)
    {
        //Check to see if that task has already had some progress made on it by seeing if the pref for it exists. 
        //If it does, increment it further, if not, create the new pref for it and set it to 1.
        if (PlayerPrefs.HasKey("task-" + task.taskID + "-progress"))
        {
            PlayerPrefs.SetInt("task-" + task.taskID + "-progress", PlayerPrefs.GetInt("task-" + task.taskID + "-progress") + 1);
            Debug.Log("Pref for task '" + task.taskName + "(" + task.taskID + ")" + "' exists. New value is " + PlayerPrefs.GetInt("task-" + task.taskID + "-progress") + ".");
        }

        else
        {
            Debug.Log("Pref for task '" + task.taskName + "(" + task.taskID + ")" + "' does not exist. Creating new pref...");
            PlayerPrefs.SetInt("task-" + task.taskID + "-progress", 1);
        }
    }

    void TaskCompleted(TaskData task)
    {
        Debug.Log("The Task: " + task.taskName + "(" + task.taskID + ")" + " has been completed!");

        //do stuff to make the task complete stuff appear in the game UI
        taskPromptUI_TaskTitle.text = task.taskName;
        taskPromptUI_TaskDesc.text = task.taskDescription;
        taskPromptUI_TaskIcon.sprite = task.taskIcon;
        taskPromptUI_CurrencyReward.text = task.taskCurrencyReward.ToString();

        taskPromptUI_Animator.SetBool("TaskComplete", true);

        //Do a thing to actually award the player with currency
    }

    void DeletePrefs ()
    {
        PlayerPrefs.DeleteAll();
    }
}
