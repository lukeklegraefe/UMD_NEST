using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public int index = 0;

    private void Update() {
        if (transform.childCount <= 0) {
            FindObjectOfType<Inventory>().isFull[index] = false;
        }
    }
    public void DropItem() {
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        FindObjectOfType<Player>().items[index] = "";
    }
}
