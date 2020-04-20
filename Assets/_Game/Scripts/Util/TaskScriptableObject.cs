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

    public string taskName;
    public int taskID;
    public string taskDescription;
    public Sprite taskIcon;

    public TaskRarity taskRarity;

    public int taskThreshold;

    public int taskCurrencyReward;
}