using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{
    private Player player;

    // Array of NPC prefabs
    public GameObject[] npcs;

    public GameObject[] tropBuildings;
    public GameObject[] jungleBuildings;
    public GameObject[] cityBuildings;
    public GameObject[] snowBuildings;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    public void SpawnNPCs() {
        for (int i = 0; i < player.eggCombinations.Length; i++) {
            if (player.eggCombinations[i] != 0) {
                Instantiate(npcs[i]);
            }
            switch (i) {
                case 0:
                    if(player.eggCombinations[i] == 1) {
                        Instantiate(tropBuildings[0]);
                    }
                    else if(player.eggCombinations[i] == 2) {
                        Instantiate(tropBuildings[1]);
                    }
                    break;
                case 1:
                    if (player.eggCombinations[i] == 1) {
                        Instantiate(jungleBuildings[0]);
                    }
                    else if (player.eggCombinations[i] == 2) {
                        Instantiate(jungleBuildings[1]);
                    }
                    break;
                case 2:
                    if (player.eggCombinations[i] == 1) {
                        Instantiate(cityBuildings[0]);
                    }
                    else if (player.eggCombinations[i] == 2) {
                        Instantiate(cityBuildings[1]);
                    }
                    break;
                case 3:
                    if (player.eggCombinations[i] == 1) {
                        Instantiate(snowBuildings[0]);
                    }
                    else if (player.eggCombinations[i] == 2) {
                        Instantiate(snowBuildings[1]);
                    }
                    break;
            }
        }
    }
}
