using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quests : MonoBehaviour
{
    // Not sure why I named this Quests when there is already 'Quest.cs'
    public GameObject questView;
    GameObject[] questLogs;

    private void Awake() {
        questView = GameObject.FindGameObjectWithTag("QuestView");
        questLogs = GameObject.FindGameObjectsWithTag("Quest");
    }

    public void Start() {
        questView.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.C)) {
            if (questView.activeSelf == false) {
                questView.SetActive(true);
            }
            else {
                questView.SetActive(false);
            }
        }
    }

    public void UpdateList(List<Quest> quests) {
        // Called to update the list of quests in the UI
        for(int i = 0; i < questLogs.Length; i++) {
            questLogs[i].GetComponent<TextMeshProUGUI>().text = "";
        }
        foreach(Quest q in quests) {
            questLogs[quests.IndexOf(q)].GetComponent<TextMeshProUGUI>().text = q.questName;
        }
    }
}
