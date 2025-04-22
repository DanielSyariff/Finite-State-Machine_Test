using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NinjaController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private State currentState;

    public IdleState idleState;
    public RunState runState;
    public JumpState jumpState;
    public AttackState attackState;
    public HurtState hurtState;
    public DieState dieState;

    public float hurtCooldown = 1f;
    private float lastHurtTime = -999f;
    private SpriteRenderer spriteRenderer;
    public bool isHurting = false;

    public int maxLives = 3;
    private int currentLives;
    public int CurrentLives => currentLives;

    private Vector3 spawnPosition;

    public bool IsAlive { get; private set; } = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentLives = maxLives;
        spawnPosition = transform.position;

        ActionEvents.OnLivesChanged?.Invoke(currentLives);

        idleState = new IdleState(this, animator);
        runState = new RunState(this, animator);
        jumpState = new JumpState(this, animator);
        attackState = new AttackState(this, animator);
        hurtState = new HurtState(this, animator);
        dieState = new DieState(this, animator);

        TransitionToState(idleState);
    }

    void Update()
    {
        currentState?.UpdateState();

        if (!IsAlive && Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    public void TransitionToState(State newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void Move(float direction)
    {
        if (isHurting) return;

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        if (direction != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }

    public void StopHorizontalMovement()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public void Jump()
    {
        if (isHurting) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        return grounded;
    }


    public void TakeDamage()
    {
        if (!IsAlive || isHurting) return;

        currentLives--;
        ActionEvents.OnLivesChanged?.Invoke(currentLives);

        if (currentLives <= 0)
        {
            Die();
            TransitionToState(dieState);
        }
        else
        {
            isHurting = true;
            StartCoroutine(BlinkCoroutine());
            TransitionToState(hurtState);
        }
    }

    public void Die()
    {
        IsAlive = false;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        rb.bodyType = RigidbodyType2D.Kinematic;
    }


    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsAlive || Time.time - lastHurtTime < hurtCooldown || isHurting) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            lastHurtTime = Time.time;
            isHurting = true;

            Vector2 knockback = (transform.position - collision.transform.position).normalized * 5f;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockback, ForceMode2D.Impulse);

            StartCoroutine(BlinkCoroutine());
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        float blinkTime = hurtCooldown;
        float blinkInterval = 0.1f;
        float timer = 0f;

        while (timer < blinkTime)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        isHurting = false;
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        IsAlive = true;
        currentLives = maxLives;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 1f;
        isHurting = false;

        ActionEvents.OnLivesChanged?.Invoke(currentLives);

        TransitionToState(idleState);
    }
}
