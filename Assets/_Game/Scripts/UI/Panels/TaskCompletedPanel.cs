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

    public void CompletedTask(TaskData taskData)
    {
        //do stuff to make the task complete stuff appear in the game UI
        taskPromptUI_TaskTitle.text = taskData.TaskName;
        taskPromptUI_TaskDesc.text = taskData.Description;
        taskPromptUI_TaskIcon.sprite = taskData.Icon;
        taskPromptUI_CurrencyReward.text = taskData.CurrencyReward.ToString();

        ShowPanel();
    }

}
