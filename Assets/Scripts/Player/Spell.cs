using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Player player;
    public Rigidbody2D rb;

    private void Awake() {
        player = FindObjectOfType<Player>();
    }

    private void Start() {
        if (player.GetComponent<SpriteRenderer>().flipX == true) {
            rb.velocity = transform.right * 15f;
        }
        else {
            rb.velocity = -transform.right * 15f;
        }
        StartCoroutine(DestroySpell(1f));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Confiner")) {
            EnemyController eController = collision.GetComponent<EnemyController>();
            if (eController != null) {
                eController.TakeDamage(20);
            }
            StartCoroutine(DestroySpell(0f));
        }
    }

    IEnumerator DestroySpell(float delay) {
        yield return new WaitForSeconds(delay);
        this.GetComponent<ParticleSystem>().Play();
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
