using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OutroController : MonoBehaviour
{
    public PlayableDirector director;
    void Start()
    {
        
    }

    void Update()
    {
        // Go back to main menu when timeline is complete
        if(director.state.ToString() == "Paused") {
            for (int j = 0; j < PlayerData.eggCombinations.Length; j++) {
                PlayerData.eggCombinations[j] = 0;
            }
            PlayerData.health = 100;
            PlayerData.items = new string[8];
            PlayerData.quests.Clear();
            FindObjectOfType<LevelController>().MainMenu();
        }
    }
}
