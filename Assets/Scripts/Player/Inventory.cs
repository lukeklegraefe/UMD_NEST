using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public string[] items;
    public GameObject[] itemsPrefabs;
    public GameObject inventory;
    public GameObject pauseEffect;

    public void Start() {
        // Get object, make it invisible
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        pauseEffect = GameObject.FindGameObjectWithTag("PauseEffect");
        inventory.SetActive(false);
        pauseEffect.SetActive(false);
        items = new string[slots.Length];
    }

    public void Update() {
        // Open inventory / close
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.C)) {
            if(inventory.activeSelf == false) {
                inventory.SetActive(true);
                pauseEffect.SetActive(true);
                Time.timeScale = 0f;
            }
            else {
                inventory.SetActive(false);
                pauseEffect.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void ReloadInventory(string[] playerItems) {
        items = new string[slots.Length];
        for (int i = 0; i < playerItems.Length; i++) {
            for (int j = 0; j < slots.Length; j++) {
                if (isFull[j] == false && playerItems[i] != "") {
                    isFull[j] = true;
                    items[j] = playerItems[i];
                    switch (items[i]) {
                        case "Flower":
                            Instantiate(itemsPrefabs[0], slots[j].transform, false);
                            break;
                        case "Map":
                            Instantiate(itemsPrefabs[1], slots[j].transform, false);
                            break;
                        case "Snowflake":
                            Instantiate(itemsPrefabs[2], slots[j].transform, false);
                            break;
                        case "Processor":
                            Instantiate(itemsPrefabs[3], slots[j].transform, false);
                            break;
                    }
                    break;
                }
            }
        }
    }
}
