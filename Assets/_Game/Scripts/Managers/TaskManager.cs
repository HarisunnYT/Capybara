using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : Singleton<TaskManager>
{
    [SerializeField]
    private TaskList[] taskLists;
    public TaskList[] TaskLists { get { return taskLists; } private set { taskLists = value; } }

    Dictionary<string, int> taskDictionary;

    private const string taskProgressPrefFormat = "task-{0}-progress";

    private void Start()
    {
        GenerateDictionary();
    }

    private void GenerateDictionary()
    {
        int tasksLength = 0;
        foreach (var tasks in TaskLists)
        {
            tasksLength += tasks.tasks.Length;
        }

        taskDictionary = new Dictionary<string, int>(tasksLength);
        foreach (var tasks in TaskLists)
        {
            for (int i = 0; i < tasks.tasks.Length; i++)
            {
                taskDictionary.Add(tasks.tasks[i].name, 0);
                if (PlayerPrefs.HasKey(string.Format(taskProgressPrefFormat, tasks.tasks[i].name)))
                {
                    taskDictionary[tasks.tasks[i].name] = PlayerPrefs.GetInt(string.Format(taskProgressPrefFormat, tasks.tasks[i].name));
                }
            }
        }
    }

    public void UpdateTask(TaskData taskData)
    {
        UpdateTask(taskData.name);
    }

    //Called when an event happens in the game to progress a task, passes though the ID of the task that is to be progressed
    public void UpdateTask(string taskName)
    {
        foreach (var tasks in TaskLists)
        {
            foreach (TaskData task in tasks.tasks)
            {
                //if the task has a required task and the required task isn't completed, continue
                if (task.RequiredTask != null && taskDictionary[task.RequiredTask.TaskName] < task.RequiredTask.Threshold)
                {
                    continue;
                }

                if (taskName == task.name)
                {
                    taskDictionary[taskName]++;

                    //If the threshold for the task has been reached (per the value in the dictionary) then save that data to player prefs to perminantly unlock it and then go and do the fanfare of the task being completed! 
                    //NOTE: Have used == here instead of => as I can see instances where the number could go beyond the threshold and activate the task unlocking process multiple times
                    if (taskDictionary[taskName] == task.Threshold)
                    {
                        SaveTaskData(task);
                        TaskCompleted(task);

                        return;
                    }

                    //If the Task Type is global, we also want it to persist over sessions so we also save it to player prefs.
                    if (task.Type == TaskData.TaskType.Global)
                    {
                        SaveTaskData(task);
                    }
                    return;
                }
            }
        }
    }

    public bool IsTaskComplete(TaskData task)
    {
        return PlayerPrefs.GetInt(string.Format(taskProgressPrefFormat, task.name)) >= taskDictionary[task.name];
    }

    public int GetTaskProgress(TaskData task)
    {
        return taskDictionary[task.name];
    }

    private void SaveTaskData(TaskData task)
    {
        PlayerPrefs.SetInt(string.Format(taskProgressPrefFormat, task.name), taskDictionary[task.name]);
    }

    private void TaskCompleted(TaskData task)
    {
        Debug.Log("The Task: " + task.name + "(" + task.name + ")" + " has been completed!");

        CanvasManager.Instance.GetPanel<TaskCompletedPanel>().CompletedTask(task);

        //Do a thing to actually award the player with currency
    }

    private void DeletePrefs ()
    {
        PlayerPrefs.DeleteAll();
    }
}
