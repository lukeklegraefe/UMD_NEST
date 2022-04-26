using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;

	public void TriggerDialogue() {
		// Just calls the trigger dialogue based on dialogue from parent object
		FindObjectOfType<DialogueController>().StartDialogue(dialogue);
	}
}
