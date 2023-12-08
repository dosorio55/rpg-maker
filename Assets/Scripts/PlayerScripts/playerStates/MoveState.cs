using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public MoveState(Player _player, StateMachine<PlayerState> _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void Update()
    {

        player.SetVelocity(xInput * player.MoveSpeed, player.RigidBody.velocity.y);
        ChangeStateController();
        
        base.Update();
    }

    private void ChangeStateController()
    {
        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.IdleState);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}