using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskCompletedPanel : Panel, IAnimationHandler
{
    [Space()]
    [SerializeField]
    private Image taskPromptUI_TaskIcon;

    [SerializeField]
    private TextMeshProUGUI taskPromptUI_TaskTitle;

    [SerializeField]
    private TextMeshProUGUI taskPromptUI_TaskDesc;

    [SerializeField]
    private TextMeshProUGUI taskPromptUI_CurrencyReward;

    private List<TaskData> completedTasks = new List<TaskData>();

    public void CompletedTask(TaskData taskData)
    {
        completedTasks.Add(taskData);

        if (!gameObject.activeSelf)
        {
            ShowPrompt();
        }
    }

    private void ShowPrompt()
    {
        TaskData taskData = completedTasks[completedTasks.Count - 1];
        completedTasks.Remove(taskData);

        //do stuff to make the task complete stuff appear in the game UI
        taskPromptUI_TaskTitle.text = taskData.TaskName;
        taskPromptUI_TaskDesc.text = taskData.Description;
        taskPromptUI_TaskIcon.sprite = taskData.Icon;
        taskPromptUI_CurrencyReward.text = taskData.CurrencyReward.ToString();

        ShowPanel();
    }

    protected override void OnClose()
    {
        if (completedTasks.Count > 0)
        {
            ShowPrompt();
        }
    }

}
