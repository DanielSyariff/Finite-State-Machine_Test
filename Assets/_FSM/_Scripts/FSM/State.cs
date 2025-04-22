using UnityEngine;

public abstract class State
{
    protected NinjaController ninja;
    protected Animator animator;

    public State(NinjaController ninja, Animator animator)
    {
        this.ninja = ninja;
        this.animator = animator;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}

