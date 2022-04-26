using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    // Gets values that determine the size and color of the egg
    private Player player;

    public SpriteRenderer[] spriteRenderers;
    public Sprite[] eggColors;
    public Sprite[] eggPatterns;
    public Sprite[] eggGlows;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // Make modular
        if(player.eggCombinations[0] == 1) {
            spriteRenderers[0].sprite = eggColors[0];
        }
        else if(player.eggCombinations[0] == 2){
            spriteRenderers[0].sprite = eggColors[1];
        }

        if (player.eggCombinations[1] == 1) {
            spriteRenderers[1].sprite = eggPatterns[0];
        }
        else if (player.eggCombinations[1] == 2) {
            spriteRenderers[1].sprite = eggPatterns[1];
        }

        if (player.eggCombinations[2] == 1) {
            spriteRenderers[2].sprite = eggGlows[0];
        }
        else if (player.eggCombinations[2] == 2) {
            spriteRenderers[2].sprite = eggGlows[1];
        }

        if (player.eggCombinations[3] == 1) {
            this.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        }
        else if (player.eggCombinations[3] == 2) {
            this.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
    }
}
