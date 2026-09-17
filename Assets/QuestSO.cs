using System.Collections.Generic;
using UnityEngine;

public enum QuestObjectiveType
{
    Kill,
    Collect,
    Interact,
    ReachLocation,
    Custom
}

[System.Serializable]
public class QuestObjectiveData
{
    public string description;
    public QuestObjectiveType type;
    public string targetId;      // enemy tag / item id / location id - whatever you pass into ReportProgress
    public int requiredAmount = 1;
}

public enum RewardType { Currency, Item, Experience }

[System.Serializable]
public class QuestReward
{
    public RewardType type;
    public int amount;
    public string itemId; // only used when type == Item
}

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestSO : ScriptableObject
{
    public string questId;
    public string title;
    [TextArea] public string description;
    public List<QuestObjectiveData> objectives = new List<QuestObjectiveData>();
    public List<QuestReward> rewards = new List<QuestReward>();
    public List<QuestSO> prerequisites = new List<QuestSO>(); // leave empty if none
}
