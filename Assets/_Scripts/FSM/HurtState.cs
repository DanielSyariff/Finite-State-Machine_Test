using UnityEngine;

public class HurtState : State
{
    public HurtState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.Play("Hurt");
    }

    public override void UpdateState()
    {
        if (ninja.IsAlive)
            ninja.TransitionToState(ninja.idleState);
        else
            ninja.TransitionToState(ninja.dieState);
    }
}

