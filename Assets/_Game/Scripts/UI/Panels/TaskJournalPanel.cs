using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskJournalPanel : Panel
{
    [Space()]
    [SerializeField]
    private string[] pageTitles;

    [Space()]
    [SerializeField]
    private TMP_Text pageTitle;

    [SerializeField]
    private TMP_Text progressText;

    [SerializeField]
    private RectTransform progressBar;

    [SerializeField]
    private DisplayList displayList;

    private int currentPageIndex = -1;

    protected override void OnShow()
    {
        
        DisplayPage(0);
    }

    public void DisplayPage(int pageIndex)
    {
        if (currentPageIndex == pageIndex)
            return;

        pageTitle.text = pageTitles[pageIndex];

        displayList.ClearList();

        int amountCompleted = 0;
        for (int i = 0; i < TaskManager.Instance.TaskLists[pageIndex].tasks.Length; i++)
        {
            TaskData task = TaskManager.Instance.TaskLists[pageIndex].tasks[i];
            bool taskComplete = TaskManager.Instance.IsTaskComplete(task);
            displayList.DisplayItem<TaskDisplayItem>().Configure(i + 1, task.TaskName, taskComplete);

            amountCompleted += taskComplete ? 1 : 0;
        }

        progressText.text = amountCompleted + "/" + TaskManager.Instance.TaskLists[pageIndex].tasks.Length;

        float progress = (float)amountCompleted / TaskManager.Instance.TaskLists[pageIndex].tasks.Length;
        progressBar.sizeDelta = new Vector2(Util.ConvertRange(progress, 0, 1, 0, ((RectTransform)progressBar.parent.transform).rect.width), progressBar.sizeDelta.y);

        currentPageIndex = pageIndex;
    }
}
