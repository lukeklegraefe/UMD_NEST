using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    // Load a new level
    public int levelIndex;
    private LevelController lvlController;

    private void Awake() {
        lvlController = FindObjectOfType<LevelController>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            lvlController.LoadLevel(levelIndex);
        }
    }
}
