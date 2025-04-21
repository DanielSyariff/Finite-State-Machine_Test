using UnityEngine;

public class AttackState : State
{
    private float attackTime = 0.5f;
    private float timer;

    public AttackState(NinjaController ninja, Animator animator) : base(ninja, animator) { }

    public override void EnterState()
    {
        animator.Play("Attack");
        timer = 0f;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= attackTime)
        {
            ninja.TransitionToState(ninja.idleState);
        }
    }
}

