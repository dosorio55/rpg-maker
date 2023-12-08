using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : BaseState
{
    protected StateMachine<PlayerState> stateMachine;
    protected Player player;

    protected float xInput;
    protected string animBoolName;
    protected bool triggerCalled;

    protected PlayerState(Player _player, StateMachine<PlayerState> _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public override void EnterState()
    {
        player.Animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.Animator.SetFloat("yVelocity", player.RigidBody.velocity.y);
    }

    

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

    public override void ExitState()
    {
        player.Animator.SetBool(animBoolName, false);
    }

}
