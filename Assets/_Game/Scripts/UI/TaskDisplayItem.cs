using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskDisplayItem : DisplayItem
{
    [SerializeField]
    private TMP_Text numberText;

    [SerializeField]
    private TMP_Text taskTitle;

    [SerializeField]
    private GameObject tick;

    [SerializeField]
    private GameObject selectedObj;

    public void Configure(int number, string title, bool completed)
    {
        numberText.text = "#" + number;
        taskTitle.text = title;
        tick.SetActive(completed);
    }

    public void SetSelected(bool selected)
    {
        selectedObj.SetActive(selected);
    }
}
