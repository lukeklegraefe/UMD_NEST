using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    bool gameHasEnded = false;
    public void GameOver() {
        if (!gameHasEnded) {
            gameHasEnded = true;
            Debug.Log("Game over");
            Invoke("Restart", 1f);
            Debug.Log("Hello");
        }
    }

    private void Restart() {
        FindObjectOfType<Player>().Respawn();
        FindObjectOfType<LevelController>().LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
