using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    public DashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("DashState");

        player.StartCoroutine(player.StartTimer(ChangeStateController, player.DashDuration));
    }

    public override void Update()
    {
        base.Update();
        Dash();
    }

    private void Dash()
    {
        int facingDirection = player.facingRight ? 1 : -1;
        player.SetVelocity(xInput + player.DashSpeed * facingDirection, player.RigidBody.velocity.y);
    }

    private void ChangeStateController()
    {
        stateMachine.ChangeState(player.MoveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

