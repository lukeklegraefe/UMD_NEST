using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Player : MonoBehaviour
{
    public List<Quest> quests;

    public int health = 100;
    //public List<string> items;
    public string[] items = new string[8];

    private Slider healthBar;
    private Inventory inventory;

    // 0 indicates no choice has been made for that NPC. Each index can equal 1 or 2 based on the two options for each NPC
    public int[] eggCombinations = { 0, 0, 0, 0 };

    private void Awake() {
        healthBar = GameObject.FindGameObjectWithTag("Health Bar").GetComponent<Slider>();
    }

    private void Start() {
        // Load data from static PlayerData - retains health, egg, items, etc.
        LoadData();
    }

    public void Update() {
        // Update player quests
        this.GetComponent<Quests>().UpdateList(quests);
        healthBar.value = health;
        if(health <= 0) {
            FindObjectOfType<GameController>().GameOver();
        }

        foreach(Quest q in quests) {
            if(q.goal.goalType == GoalType.Gathering) {
                int count = 0;
                for (int i = 0; i < items.Length; i++) {
                    if (q.goal.objectName == items[i]) {
                        count++;
                    }
                }
                q.goal.currentAmount = count;
            }
        }

        if (Input.GetKey(KeyCode.Comma)) {
            health = 100;
        }
        if (Input.GetKey(KeyCode.Period)) {
            for (int j = 0; j < eggCombinations.Length; j++) {
                eggCombinations[j] = 1;
            }
        }
    }

    public void EggDecision(int choiceID, int value) {
        // Story based choice affects "egg combinations"
        eggCombinations[choiceID] = value;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        healthBar.value = health;
        this.GetComponent<Animator>().SetTrigger("hurt");
        FindObjectOfType<AudioController>().Play("Hurt");
    }

    // Saves all local variables to static instance of PlayerData
    public void SaveData() {
        PlayerData.health = health;
        PlayerData.items = items;
        PlayerData.eggCombinations = eggCombinations;
        PlayerData.quests = quests;
    }

    // Loads all static variables as local
    private void LoadData() {
        health = PlayerData.health;
        eggCombinations = PlayerData.eggCombinations;
        if(PlayerData.items != null) {
            items = PlayerData.items;
            this.GetComponent<Inventory>().ReloadInventory(items);
        }
        if(PlayerData.quests != null) {
            quests = PlayerData.quests;
        }
        if(FindObjectOfType<HubController>() != null) {
            FindObjectOfType<HubController>().SpawnNPCs();
        }
    }

    public void Respawn() {
        health = 100;
        FindObjectOfType<AudioController>().Play("Interact");
    }

    public void AddItem(string itemName) {
        // Add an item for gathering quests
        for(int i = 0; i < quests.Count; i++) {
            if (quests[i].goal.objectName == itemName) {
                quests[i].goal.ItemObtained();
                if (quests[i].goal.isComplete()) {
                    quests[i].Complete();
                }
                break;
            }
        }
    }

    public void KillEnemy(string enemyName) {
        Debug.Log("Killed");
        // Add a kill for kill quests
        for (int i = 0; i < quests.Count; i++) {
            if (quests[i].goal.objectName == enemyName) {
                quests[i].goal.ItemObtained();
                if (quests[i].goal.isComplete()) {
                    quests[i].Complete();
                }
                break;
            }
        }
    }
}
