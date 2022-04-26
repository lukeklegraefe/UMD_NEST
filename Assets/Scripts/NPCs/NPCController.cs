using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour
{
    public string characterName;
    public string[] queryJobDialogue;
    public string[] defaultDialogue;

    private Player player;
    public string[] choiceValues;

    private bool inRange = false;
    private bool isTalking = false;
    public bool choicePending = false;
    public bool triggered = false;

    private QuestController questController;
    public bool hasQuest;
    private bool questAccepted = false;
    public bool questComplete = false;
    public int questIndex = 0;
    public int eggIndex = 0;

    public Animator textAnimator;
    private DialogueTrigger dialogueTrigger;
    private DialogueController dialogueController;
    public TextMeshPro[] dialogueBoxes;
    public GameObject chevron;

    private void Awake() {
        questController = this.GetComponent<QuestController>();
        dialogueTrigger = this.GetComponent<DialogueTrigger>();
        dialogueController = FindObjectOfType<DialogueController>();
        dialogueBoxes = GetComponentsInChildren<TextMeshPro>();
        player = FindObjectOfType<Player>();
    }

    void Start() {
        // If quest is complete or no quest exists, use default dialogue
        if (questComplete || !hasQuest) {
            dialogueTrigger.dialogue.sentences = defaultDialogue;
        }
        dialogueBoxes[2].gameObject.SetActive(false);
        dialogueBoxes[3].gameObject.SetActive(false);
        chevron.gameObject.SetActive(false);
        dialogueBoxes[2].text = choiceValues[0];
        dialogueBoxes[3].text = choiceValues[1];

        int i = 0;

        if(SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 2 && SceneManager.GetActiveScene().buildIndex != 7) {
            Debug.Log("hello");
            if(player.eggCombinations[eggIndex] != 0) {
                Destroy(gameObject);
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 2 && player.eggCombinations[eggIndex] != 0) {
            hasQuest = false;
            dialogueTrigger.dialogue.sentences = defaultDialogue;
        }
        
        // Works but doesnt work anymore?
        /*
        Debug.Log(player.quests.Count);
        for (int j = 0; j < player.quests.Count; j++) {
            Debug.Log("loaded");
            if (player.quests[j].questName == questController.quest.questName) {
                Debug.Log("found");
            }
        }
        */
    }

    void Update() {
        int i = 0;
        foreach (Quest q in player.quests) {
            Debug.Log(q.questName + "\n" + questController.quest.questName);
            if (q.questName == questController.quest.questName) {
                questAccepted = true;
                questController.quest.isActive = true;
                player.quests[i] = questController.quest;
                if(questController.quest.goal.currentAmount == questController.quest.goal.requiredAmount) {
                    questController.quest.isReportable = true;
                }
                break;
            }
            i++;
        }
        if (player.transform.position.x > this.transform.position.x) {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        // Handle dialogue events
        if (inRange && !isTalking) {
            chevron.gameObject.SetActive(true);
        }
        else {
            chevron.gameObject.SetActive(false);
        }
        if (inRange && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.X))) {
            if (questController.quest.isReportable) {
                CompleteQuest();
                questController.quest.isReportable = false;
            }
            GetDialogue();
            if(dialogueController.sentences.Count == 0) {

                if (!questAccepted && hasQuest) {
                    questController.AcceptQuest();
                    questAccepted = true;
                }
                else if (choicePending) {
                    dialogueBoxes[2].gameObject.SetActive(true);
                    dialogueBoxes[3].gameObject.SetActive(true);
                }
            }
        }
        // End dialogue on player leave
        else if(!inRange && isTalking) {
            dialogueController.EndDialogue();
            isTalking = false;
            dialogueBoxes[2].gameObject.SetActive(false);
            dialogueBoxes[3].gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && choicePending) {
            SetChoice(eggIndex, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && choicePending) {
            SetChoice(eggIndex, 2);
        }
    }

    public void GetDialogue() {
        // If NPC is talking, continue talking, if talking is over, close dialogue, if not talking, trigger dialogue
        if (isTalking) {
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

    public void CompleteQuest() {
        // Set vars for NPC and quest controller for completion (redundant)
        questComplete = true;
        questController.quest.isComplete = true;
        dialogueTrigger.dialogue.sentences = queryJobDialogue;
    }
    
    public void GetChoice() {
        // Prompt the player to choose an option (set buttons active)
        choicePending = true;
        dialogueBoxes[2].gameObject.SetActive(true);
        dialogueBoxes[3].gameObject.SetActive(true);
    }

    public void SetChoice(int index, int value) {
        // Make the choice and alter player variables (egg combinations)
        FindObjectOfType<AudioController>().Play("Interact");
        dialogueBoxes[2].gameObject.SetActive(false);
        dialogueBoxes[3].gameObject.SetActive(false);
        choicePending = false;
        player.eggCombinations[index] = value;
        triggered = true;
        GetDialogue();
        // Set to non-quest related dialogue
        dialogueTrigger.dialogue.sentences = defaultDialogue;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        inRange = false;
    }
}
