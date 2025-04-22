using UnityEngine;

public class RunState : State
{
    public RunState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.Play("Run");
    }

    public override void UpdateState()
    {
        float move = Input.GetAxisRaw("Horizontal");
        if (move == 0)
            ninja.TransitionToState(ninja.idleState);

        ninja.Move(move);
        if (Input.GetButtonDown("Jump"))
            ninja.TransitionToState(ninja.jumpState);
        else if (Input.GetButtonDown("Fire1"))
            ninja.TransitionToState(ninja.attackState);
    }
}

