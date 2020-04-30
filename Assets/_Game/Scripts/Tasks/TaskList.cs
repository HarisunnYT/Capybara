using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Capybara/Task List")]
public class TaskList : ScriptableObject
{
    public TaskData[] tasks;
}
