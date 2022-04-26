using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public Quest quest;
    public Player player;
    private bool questEnded;

    public void Awake() {
        player = FindObjectOfType<Player>();
    }


    public void Update() {
        // Save on performance by adding function specifically for this?
        if (quest.isComplete && !questEnded) {
            this.GetComponentInParent<NPCController>().CompleteQuest();
            questEnded = true;
            player.quests.Remove(quest);
        }
    }

    public void AcceptQuest() {
        // Alter local quest var and player quest var (add to list)
        quest.isActive = true;
        player.quests.Add(quest);
    }

}
