using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    Coroutine dashCoroutine;
    public DashState(Player _player, StateMachine<PlayerState> _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

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

    public override void ExitState()
    {
        base.ExitState();
    }
}

