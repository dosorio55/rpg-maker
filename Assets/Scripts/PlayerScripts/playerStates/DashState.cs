using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    Coroutine dashCoroutine;
    public DashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dashCoroutine = player.StartCoroutine(player.StartTimer(ChangeStateController, player.DashDuration));
    }

    public override void Update()
    {
        base.Update();
        Dash();

        if (player.IsWallDetected() && !player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
            player.StopTimer(dashCoroutine);
        }
        else if (player.IsWallDetected() && player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }

    private void Dash()
    {
        player.SetVelocity(xInput + player.DashSpeed * player.DashDirection, 0);
    }

    private void ChangeStateController()
    {
        stateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}

