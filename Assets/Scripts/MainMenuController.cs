using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public bool tutorialOpen = false;
    public GameObject tutorialObject;

    public void Awake() {
        tutorialObject.SetActive(false);
    }

    public void PlayGame() {
        FindObjectOfType<LevelController>().LoadLevel(1);
    }

    public void Tutorial() {
        if (tutorialOpen) {
            tutorialObject.SetActive(false);
            tutorialOpen = false;
        }
        else {
            tutorialObject.SetActive(true);
            tutorialOpen = true;
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
