using UnityEngine;

public enum QuestType
{
    Collect,
    Kill,
}

[System.Serializable]
public class Reward
{
    public string itemName;
    public int amount;
}


[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string questGiverName;
    public QuestType questType;
    public string targetTag;
    public int targetAmount;

    public Reward reward;

    public string[] questDialog;
}
