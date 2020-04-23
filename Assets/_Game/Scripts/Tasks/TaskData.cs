using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "Capybara/Task", order = 1)]
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

    public string TaskName;
    public string Description;

    [Space()]
    public Sprite Icon;

    [Space()]
    public TaskType Type;
    public TaskRarity Rarity;

    [Space()]
    public int Threshold;
    public int CurrencyReward;

    [Header("Optional")]
    public TaskData RequiredTask;
}