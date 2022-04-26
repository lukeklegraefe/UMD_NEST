using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//This is my manual Change

public class PlayerMovement : MonoBehaviour {
    [Header("Jump and Move Speed")]
    public float speed = 10f;
    public float maxSpeed = 12f;
    public float jumpSpeed = 15f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    [Header("RayCast")]
    public LayerMask groundLayer;
    public bool isGrounded;

    [Header("Physics")]
    public float gravity = 1;
    public float fallMultiplier = 8f;
    public float drag = 0f;
    private Rigidbody2D rb;
    private Vector2 dir;
    private Vector3 velocity = Vector3.zero;

    private Animator animator;

    private void Awake() {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        // Outputs (-1, 0, or 1)
        dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);

        // Jump only if grounded
        if(Input.GetKey(KeyCode.Space) && isGrounded) {
            Jump();
        }

        // Flip sprite
        if(dir.x > 0) {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(dir.x < 0){
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate() {
        // Sets a velocity cap on the players movement
        Vector3 targetVelocity = new Vector2(dir.x * 10f, rb.velocity.y);
        if (isGrounded) {
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);
            animator.SetBool("isJumping", false);
            // Only start animation if > 1.2 speed
            if(Mathf.Abs(rb.velocity.x) > 1.2) {
                animator.SetBool("isRunning", true);
            }
        }
        else {
            // Gives you less control in the air
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * 5);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", true);
        }
        bool changeDir = (dir.x > 0 && rb.velocity.x < 0 || dir.x < 0 && rb.velocity.x > 0);

        // Set velocity using rigidbody
        if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // Player is touching the ground
        if (isGrounded) {
            if (Mathf.Abs(dir.x) < 0.4f || changeDir) {
                rb.drag = drag;
            }
            else {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }

        // Player is in the air, adds air drag accordingly
        else {
            rb.gravityScale = gravity;
            rb.drag = drag * 0.15f;
            if(rb.velocity.y < 0) {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if(rb.velocity.y > 0) {
                rb.gravityScale = gravity * (fallMultiplier / 1.25f);
            }
            else {
                gravity = 1;
            }
        }
    }

    private void Jump() {
        // Add an impulse force for player jump
        FindObjectOfType<AudioController>().Play("Jump");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed * 2, ForceMode2D.Impulse);
    }
}

