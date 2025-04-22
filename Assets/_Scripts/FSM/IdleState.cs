using UnityEngine;

public class IdleState : State
{
    public IdleState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.Play("Idle");
        animator.SetBool("Dead", false);
    }

    public override void UpdateState()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
            ninja.TransitionToState(ninja.runState);
        else if (Input.GetButtonDown("Jump"))
            ninja.TransitionToState(ninja.jumpState);
        else if (Input.GetButtonDown("Fire1"))
            ninja.TransitionToState(ninja.attackState);
    }
}

