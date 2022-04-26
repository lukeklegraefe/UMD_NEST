using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryNPCController : MonoBehaviour
{
    public string characterName;
    public string[] defaultDialogue;

    public string[] mayorDialogue1;
    public string[] mayorDialogue2;
    public string[] mayorDialogue3;
    public string[] mayorDialogue4;

    private Player player;
    public string[] choiceValues;

    private bool inRange = false;
    private bool isTalking = false;
    public bool choicePending = false;
    public bool progressStory = false;

    public Animator textAnimator;
    private DialogueTrigger dialogueTrigger;
    private DialogueController dialogueController;
    private TextMeshPro[] dialogueBoxes;
    public GameObject chevron;

    private void Awake() {
        dialogueTrigger = this.GetComponent<DialogueTrigger>();
        dialogueController = FindObjectOfType<DialogueController>();
        dialogueBoxes = GetComponentsInChildren<TextMeshPro>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        dialogueBoxes[2].gameObject.SetActive(false);
        dialogueBoxes[3].gameObject.SetActive(false);
        chevron.gameObject.SetActive(false);
        dialogueBoxes[2].text = choiceValues[0];
        dialogueBoxes[3].text = choiceValues[1];
    }

    // Update is called once per frame
    void Update()
    {
        if(System.Array.IndexOf(player.eggCombinations, 0) == -1 && !progressStory) {
            progressStory = true;
            dialogueTrigger.dialogue.sentences = defaultDialogue;
        }
        else {
            for(int i = 0; i < player.eggCombinations.Length; i++) {
                if(player.eggCombinations[i] == 0) {
                    switch (i) {
                        case 0:
                            dialogueTrigger.dialogue.sentences = mayorDialogue1;
                            break;
                        case 1:
                            dialogueTrigger.dialogue.sentences = mayorDialogue2;
                            break;
                        case 2:
                            dialogueTrigger.dialogue.sentences = mayorDialogue3;
                            break;
                        case 3:
                            dialogueTrigger.dialogue.sentences = mayorDialogue4;
                            break;
                    }
                    break;
                }
            }
        }
        if (inRange && !isTalking) {
            chevron.gameObject.SetActive(true);
        }
        else {
            chevron.gameObject.SetActive(false);
        }
        if (inRange && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X))) {
            GetDialogue();
        }
        // End dialogue on player leave
        else if (!inRange && isTalking) {
            dialogueController.EndDialogue();
            isTalking = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && choicePending) {
            SetChoice(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && choicePending) {
            SetChoice(2);
        }
    }

    public void GetDialogue() {
        // If NPC is talking, continue talking, if talking is over, close dialogue, if not talking, trigger dialogue
        if (isTalking) {
            if (dialogueController.sentences.Count == 0) {
                if (SceneManager.GetActiveScene().buildIndex == 1) {
                    FindObjectOfType<IntroController>().MoveEgg();
                    this.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            dialogueController.DisplayNextSentence();
        }
        if (textAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueOut")) {
            isTalking = false;
        }
        if (!isTalking) {
            dialogueController.nameText = dialogueBoxes[0];
            dialogueController.dialogueText = dialogueBoxes[1];
            dialogueController.animator = textAnimator;
            dialogueTrigger.TriggerDialogue();
            isTalking = true;
        }
    }

    public void GetChoice() {
        // Prompt the player to choose an option (set buttons active)
        choicePending = true;
        dialogueBoxes[2].gameObject.SetActive(true);
        dialogueBoxes[3].gameObject.SetActive(true);
    }

    public void SetChoice(int value) {
        if(value == 1) {
            dialogueController.EndDialogue();
            FindObjectOfType<LevelController>().LoadLevel(7);
        }
        else {
            dialogueController.EndDialogue();
        }
        dialogueBoxes[2].gameObject.SetActive(false);
        dialogueBoxes[3].gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        inRange = false;
    }
}
