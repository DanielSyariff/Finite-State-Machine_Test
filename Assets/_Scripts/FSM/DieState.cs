using UnityEngine;

public class DieState : State
{
    public DieState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.SetTrigger("Die");
        ninja.Die();
    }
}

