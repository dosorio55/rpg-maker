using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public MoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.MoveSpeed, player.RigidBody.velocity.y);

        ChangeStateController();
    }

    private void ChangeStateController()
    {
        if (xInput == 0)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}