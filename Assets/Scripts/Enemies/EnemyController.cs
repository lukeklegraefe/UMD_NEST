using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public string enemyName;
    public int health;
    public int damage;
    public float speed = 5f;

    public bool patrolling = true;
    public bool turning = false;

    private Rigidbody2D rb;
    private Collider2D bodyCollider;
    public Transform groundCheckPos;
    public LayerMask groundLayer;

    private Player player;

    private void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
        bodyCollider = this.GetComponent<BoxCollider2D>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        patrolling = true;
    }

    void Update()
    {

    }

    private void FixedUpdate() {
        if (patrolling) {
            Patrol();
            turning = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);;
        }
    }

    // When called, reduce enemy health / kill
    public void TakeDamage(int dmg) {
        health -= dmg;
        if(health <= 0) {
            player.GetComponent<Player>().KillEnemy(this.name);
            this.GetComponent<ParticleSystem>().Play();
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;
            bodyCollider.enabled = false;
            Destroy(this.gameObject, 0.5f);
        }
        else {
            StopAllCoroutines();
            StartCoroutine(HitEffect());
        }
        FindObjectOfType<AudioController>().Play("Hit");
    }

    // Enemy moving in one direction
    private void Patrol() {
        if (turning || bodyCollider.IsTouchingLayers(groundLayer)) {
            Turn();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime * 100, rb.velocity.y);
    }

    // Enemy hits wall or edge, turns
    private void Turn() {
        patrolling = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        patrolling = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            player.TakeDamage(damage);
        }
    }

    IEnumerator HitEffect() {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.15f);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
