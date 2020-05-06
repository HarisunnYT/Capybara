using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TaskDisplayItem : DisplayItem, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private TMP_Text numberText;

    [SerializeField]
    private TMP_Text taskTitle;

    [SerializeField]
    private GameObject tick;

    [SerializeField]
    private GameObject selectedObj;

    private TaskData taskData;

    public void Configure(int number, string title, bool completed, TaskData data)
    {
        numberText.text = "#" + number;
        taskTitle.text = title;
        taskData = data;

        tick.SetActive(completed);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selectedObj.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        CanvasManager.Instance.GetPanel<TaskJournalPanel>().DisplayTaskInformation(taskData);

        selectedObj.SetActive(true);
    }
}
