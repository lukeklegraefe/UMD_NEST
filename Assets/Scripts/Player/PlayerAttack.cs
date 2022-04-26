using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Variables")]
    public int damage = 10;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public GameObject spell;
    public Transform attackPoint;
    private Vector2 dir;
    private bool canCast = true;

    private void Awake() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //attackPoint = this.GetComponentInChildren<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Z)) && canCast) {
            Attack();
        }

        // Change direction of attack based on movement vector
        if(dir.x > 0) {
            attackPoint.position = new Vector2(transform.position.x + 1f, attackPoint.position.y);
        }
        else if(dir.x < 0){
            attackPoint.position = new Vector2(transform.position.x - 1f, attackPoint.position.y);
        }
    }

    private void Attack() {
        FindObjectOfType<AudioController>().Play("Attack");
        this.GetComponent<Animator>().SetTrigger("attack");
        Instantiate(spell, attackPoint.position, attackPoint.rotation);
        StartCoroutine(Cooldown());
    }

    // Shows hitbox
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator Cooldown() {
        canCast = false;
        yield return new WaitForSeconds(0.75f);
        canCast = true;
    }
}
