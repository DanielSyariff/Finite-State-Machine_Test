using UnityEngine;

public class DieState : State
{
    public DieState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.SetBool("Dead", true);
    }
}

