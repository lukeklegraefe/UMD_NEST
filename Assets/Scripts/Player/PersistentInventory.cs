using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInventory : MonoBehaviour
{
    // Strictly to attach to inventory and make it DoNotDestroy, delete duplicate instances
    private static PersistentInventory inventoryInstance;

    private void Awake() {
        DontDestroyOnLoad(gameObject);

        if (inventoryInstance == null) {
            inventoryInstance = this;
        }
        else {
            DestroyObject(gameObject);
        }
    }
}
