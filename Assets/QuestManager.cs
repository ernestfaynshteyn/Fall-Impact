using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest currentQuest;
    public TextMeshProUGUI questGiverName;
    public TextMeshProUGUI questGiverDialog;

    public int dialogIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NPC>())
        {
            OnQuestStart(other.GetComponent<NPC>().quest);
        }
    }

    public void OnQuestFinish()
    {
        currentQuest = null;
    }
    public void OnQuestStart(Quest quest)
    {
        currentQuest = quest;
        questGiverName.text = quest.questGiverName;
        questGiverDialog.text = quest.questDialog[0];
    }

    public void OnNextDialog()
    {
        dialogIndex++;
        if (dialogIndex < currentQuest.questDialog.Length)
        {
            questGiverDialog.text = currentQuest.questDialog[dialogIndex];
        }
        else
        {
            dialogIndex = 0;
            questGiverDialog.text = currentQuest.questDialog[dialogIndex];
        }
    }
}
