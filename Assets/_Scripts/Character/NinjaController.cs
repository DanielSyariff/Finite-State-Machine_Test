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

    public bool IsAlive { get; private set; } = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    public void TransitionToState(State newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void Move(float direction)
    {
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
        if (direction != 0)
            transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    public bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        return grounded;
    }


    public void TakeDamage()
    {
        TransitionToState(hurtState);
    }

    public void Die()
    {
        IsAlive = false;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }


}
