using UnityEngine;

public class JumpState : State
{
    private bool hasLeftGround = false;

    public JumpState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.Play("Jump");
        ninja.Jump();
        hasLeftGround = false;
    }

    public override void UpdateState()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        ninja.Move(moveInput);

        if (Input.GetButtonDown("Fire1"))
        {
            ninja.TransitionToState(ninja.attackState);
            return;
        }

        if (!hasLeftGround && !ninja.IsGrounded())
        {
            hasLeftGround = true;
        }

        if (hasLeftGround && ninja.IsGrounded())
        {
            ninja.TransitionToState(ninja.idleState);
        }
    }
}
