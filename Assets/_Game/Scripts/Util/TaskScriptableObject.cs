using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "Capybara/CreateNewTask", order = 1)]
public class TaskData : ScriptableObject
{
    public enum TaskType
    {
        Level,
        Global
    }

    public enum TaskRarity
    {
        Common,
        Rare,
        Epic,
        Legendary,
        Secret
    }

    public TaskType taskType;

    public string TaskName;
    public string TaskDescription;
    public Sprite TaskIcon;

    public TaskRarity taskRarity;

    public int TaskThreshold;

    public int TaskCurrencyReward;
}