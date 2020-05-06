using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TaskJournalPanel : Panel
{
    [Space()]
    [SerializeField]
    private TMP_Text pageTitle;

    [SerializeField]
    private TMP_Text progressText;

    [SerializeField]
    private RectTransform progressBar;

    [SerializeField]
    private DisplayList displayList;

    [Space()]
    [SerializeField]
    private TMP_Text taskTitle;

    [SerializeField]
    private Image headerImage;

    [SerializeField]
    private TMP_Text description;

    [SerializeField]
    private TMP_Text taskProgressText;

    [SerializeField]
    private Slider taskProgressBar;

    [SerializeField]
    private TMP_Text rewardText;

    [Space()]
    [SerializeField]
    private Button[] tabs;

    private int currentPageIndex = -1;

    private const int pageCount = 7;

    protected override void OnShow()
    {        
        DisplayPage(0);
    }

    private void Update()
    {
        if (InputController.InputManager.LeftBumper.WasPressed)
        {
            int index = currentPageIndex - 1 < 0 ? pageCount - 1 : currentPageIndex - 1;
            DisplayPage(index);
        }
        else if (InputController.InputManager.RightBumper.WasPressed)
        {
            int index = currentPageIndex + 1 >= pageCount ? 0 : currentPageIndex + 1;
            DisplayPage(index);
        }
    }

    public void DisplayPage(int pageIndex)
    {
        if (currentPageIndex == pageIndex)
            return;

        pageTitle.text = TaskManager.Instance.TaskLists[pageIndex].name;

        displayList.ClearList();

        int amountCompleted = 0;
        for (int i = 0; i < TaskManager.Instance.TaskLists[pageIndex].tasks.Length; i++)
        {
            TaskData task = TaskManager.Instance.TaskLists[pageIndex].tasks[i];
            bool taskComplete = TaskManager.Instance.IsTaskComplete(task);
            displayList.DisplayItem<TaskDisplayItem>().Configure(i + 1, task.TaskName, taskComplete, task);

            amountCompleted += taskComplete ? 1 : 0;
        }

        EventSystem.current.SetSelectedGameObject(displayList.DisplayItems[0].gameObject);

        ColorBlock cb;
        foreach (var button in tabs)
        {
            cb = button.colors;
            cb.colorMultiplier = 1;
            button.colors = cb;
        }

        cb = tabs[pageIndex].colors;
        cb.colorMultiplier = 5;
        tabs[pageIndex].colors = cb;

        progressText.text = amountCompleted + "/" + TaskManager.Instance.TaskLists[pageIndex].tasks.Length;

        float progress = (float)amountCompleted / TaskManager.Instance.TaskLists[pageIndex].tasks.Length;
        progressBar.sizeDelta = new Vector2(Util.ConvertRange(progress, 0, 1, 0, ((RectTransform)progressBar.parent.transform).rect.width), progressBar.sizeDelta.y);

        currentPageIndex = pageIndex;
    }

    public void DisplayTaskInformation(TaskData taskData)
    {
        taskTitle.text = taskData.TaskName;
        headerImage.sprite = taskData.Icon;
        description.text = taskData.Description;
        rewardText.text = taskData.CurrencyReward.ToString();

        taskProgressText.text = TaskManager.Instance.GetTaskProgress(taskData) + "/" + taskData.Threshold;

        float progress = (float)TaskManager.Instance.GetTaskProgress(taskData) / taskData.Threshold;
        taskProgressBar.value = progress * 100;
    }
}
