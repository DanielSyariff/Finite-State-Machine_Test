using UnityEngine;

public class HurtState : State
{
    public HurtState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.SetTrigger("Hurt");
        ninja.StopHorizontalMovement();
    }

    public override void UpdateState()
    {
        if (!ninja.isHurting)
        {
            if (ninja.IsAlive)
                ninja.TransitionToState(ninja.idleState);
            else
                ninja.TransitionToState(ninja.dieState);
        }
    }
}
