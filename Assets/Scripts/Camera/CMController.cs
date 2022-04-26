using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMController : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera cm1;
    private Player player;

    private void Awake() {
        // Find the player and follow
        player = FindObjectOfType<Player>();
        cm1 = this.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cm1.Follow = player.transform;
    }
}
