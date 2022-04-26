using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    // Sets trigger enter for item, adds to inventory if not full
    private Inventory inventory;
    public GameObject itemIcon;

    public void Start() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            for (int i = 0; i < inventory.slots.Length; i++) {
                if(inventory.isFull[i] == false) {
                    FindObjectOfType<AudioController>().Play("Pickup");
                    collision.GetComponent<Player>().AddItem(this.name);
                    collision.GetComponent<Player>().items[i] = this.name;
                    inventory.isFull[i] = true;
                    inventory.items[i] = this.name;
                    Instantiate(itemIcon, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
