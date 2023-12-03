using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.RigidBody.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        ChangeStateController();
    }

    private void ChangeStateController()
    {

        if (xInput == player.FacingDirection && player.IsWallDetected())
            return;

        if (xInput != 0)
            stateMachine.ChangeState(player.MoveState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}