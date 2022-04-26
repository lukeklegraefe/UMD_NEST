using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public string objectName;

    public int requiredAmount;
    public int currentAmount;

    public bool isComplete() {
        return (currentAmount >= requiredAmount);
    }

    public void ItemObtained() {
        // Add to amount
        currentAmount++;
    }
}

// Quest types
public enum GoalType {
    Kill,
    Gathering
}
