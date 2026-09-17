using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    Active,
    ReadyToTurnIn,
    TurnedIn
}

public class ActiveQuest
{
    public QuestSO data;
    public int[] progress;
    public QuestState state;

    public ActiveQuest(QuestSO data)
    {
        this.data = data;
        progress = new int[data.objectives.Count];
        state = QuestState.Active;
    }

    public bool AllObjectivesComplete()
    {
        for (int i = 0; i < data.objectives.Count; i++)
            if (progress[i] < data.objectives[i].requiredAmount) return false;
        return true;
    }
}

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private Dictionary<string, ActiveQuest> activeQuests = new Dictionary<string, ActiveQuest>();
    private HashSet<string> turnedInQuestIds = new HashSet<string>();

    public event Action<QuestSO> OnQuestStarted;
    public event Action<ActiveQuest> OnQuestProgress;
    public event Action<ActiveQuest> OnQuestReadyToTurnIn;
    public event Action<QuestSO> OnQuestTurnedIn;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public bool IsQuestActive(string questId) => activeQuests.ContainsKey(questId);
    public bool IsQuestTurnedIn(string questId) => turnedInQuestIds.Contains(questId);

    public bool CanStartQuest(QuestSO quest)
    {
        if (IsQuestActive(quest.questId) || IsQuestTurnedIn(quest.questId)) return false;
        return quest.prerequisites.All(p => turnedInQuestIds.Contains(p.questId));
    }

    public void StartQuest(QuestSO quest)
    {
        if (!CanStartQuest(quest)) return;
        var active = new ActiveQuest(quest);
        activeQuests[quest.questId] = active;
        OnQuestStarted?.Invoke(quest);
    }

    // Call this from wherever progress actually happens -
    // e.g. EnemyHealth.Die(), an item pickup script, a trigger volume, etc.
    public void ReportProgress(QuestObjectiveType type, string targetId, int amount = 1)
    {
        foreach (var active in activeQuests.Values)
        {
            if (active.state != QuestState.Active) continue;
            bool changed = false;

            for (int i = 0; i < active.data.objectives.Count; i++)
            {
                var obj = active.data.objectives[i];
                if (obj.type != type || obj.targetId != targetId) continue;
                if (active.progress[i] >= obj.requiredAmount) continue;

                active.progress[i] = Mathf.Min(active.progress[i] + amount, obj.requiredAmount);
                changed = true;
            }

            if (changed)
            {
                OnQuestProgress?.Invoke(active);
                if (active.AllObjectivesComplete())
                {
                    active.state = QuestState.ReadyToTurnIn;
                    OnQuestReadyToTurnIn?.Invoke(active);
                }
            }
        }
    }

    public bool TurnInQuest(string questId, PlayerStats player)
    {
        if (!activeQuests.TryGetValue(questId, out var active)) return false;
        if (active.state != QuestState.ReadyToTurnIn) return false;

        foreach (var reward in active.data.rewards)
            GrantReward(reward, player);

        active.state = QuestState.TurnedIn;
        turnedInQuestIds.Add(questId);
        activeQuests.Remove(questId);
        OnQuestTurnedIn?.Invoke(active.data);
        return true;
    }

    private void GrantReward(QuestReward reward, PlayerStats player)
    {
        switch (reward.type)
        {
            case RewardType.Currency:
                player.AddCurrency(reward.amount); // add this method to PlayerStats if missing
                break;
            case RewardType.Experience:
                player.AddExperience(reward.amount); // same
                break;
            case RewardType.Item:
                InventoryManager.Instance?.AddItem(reward.itemId, reward.amount); // hook to your inventory
                break;
        }
    }

    public ActiveQuest GetActiveQuest(string questId) =>
        activeQuests.TryGetValue(questId, out var q) ? q : null;

    public IEnumerable<ActiveQuest> GetAllActiveQuests() => activeQuests.Values;
}
