using UnityEngine;

public class AttackState : State
{
    public AttackState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        if (ninja.IsGrounded())
            ninja.StopHorizontalMovement();
        animator.Play("Attack");
    }

    public override void UpdateState()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            if (ninja.IsGrounded())
                ninja.TransitionToState(ninja.idleState);
            else
                ninja.TransitionToState(ninja.jumpState);
        }
    }
}
