using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isComplete;
    public bool isReportable;

    public string questName;
    public string description;

    public QuestGoal goal;

    public void Complete() {
        isReportable = true;
        Debug.Log("Quest: (" + questName + ") is reportable");
    }
}
