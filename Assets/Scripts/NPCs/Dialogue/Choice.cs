using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    // This script is assigned to text labels to simulate a click -
    // No need for a new canvas and UI button
    // Index and value are set in inspector

    private NPCController npcController;
    private StoryNPCController storyNPCController;
    public int choiceIndex;
    public int choiceValue;

    private void Awake() {
        npcController = GetComponentInParent<NPCController>();
        storyNPCController = GetComponentInParent<StoryNPCController>();
    }
    private void OnMouseDown() {
        Debug.Log("clicked");
        if (npcController != null) {
            npcController.SetChoice(choiceIndex, choiceValue);
        }
        else {
            storyNPCController.SetChoice(choiceIndex);
        }
    }
}
