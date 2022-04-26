using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
	public TextMeshPro nameText;
	public TextMeshPro dialogueText;

	public Animator animator;
	public Queue<string> sentences;
	public bool isTyping = false;

	void Start() {
		// Queue up new dialogue
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue) {
		// Open text box animation, set name and reset old dialogue
		animator.SetBool("isTalking", true);
		nameText.text = dialogue.name;
		sentences.Clear();

		// For each sentence, queue them
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence() {
		// If choice is promptable, prompt, else end dialogue if no more sentences
		FindObjectOfType<AudioController>().Play("Dialogue");
		if (sentences.Count == 0) {
            if (dialogueText.gameObject.GetComponentInParent<NPCController>() != null && dialogueText.gameObject.GetComponentInParent<NPCController>().questComplete && !dialogueText.gameObject.GetComponentInParent<NPCController>().choicePending && dialogueText.gameObject.GetComponentInParent<NPCController>().questComplete && !dialogueText.gameObject.GetComponentInParent<NPCController>().triggered) {
				dialogueText.gameObject.GetComponentInParent<NPCController>().GetChoice();
			}
			else if (dialogueText.gameObject.GetComponentInParent<StoryNPCController>() != null && dialogueText.gameObject.GetComponentInParent<StoryNPCController>().progressStory) {
				dialogueText.gameObject.GetComponentInParent<StoryNPCController>().GetChoice();
			}
            else {
				EndDialogue();
			}
			return;
		}

        // Dequeue sentences and skip typing
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence) {
		// Coroutine to display word by word
		dialogueText.text = sentence;
		int totalVisibleCharacters = sentence.ToCharArray().Length;
		int counter = 0;
		isTyping = true;

        while (isTyping) {
			int visibleCount = counter % (totalVisibleCharacters + 1);
			dialogueText.maxVisibleCharacters = visibleCount;
			if(visibleCount >= totalVisibleCharacters) {
				isTyping = false;
            }
			counter++;
			yield return new WaitForSeconds(0.025f);
        }
		/*foreach (char letter in sentence.ToCharArray()) {
			yield return new WaitForFixedUpdate();
			dialogueText.text += letter;
			yield return null;
		}*/
	}

	public void EndDialogue() {
		animator.SetBool("isTalking", false);
	}
}
