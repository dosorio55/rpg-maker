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

        player.StartCoroutine(player.StartTimer(ChangeStateController, player.DashDuration));
    }

    public override void Update()
    {
        base.Update();
        Dash();
    }

    private void Dash()
    {
        // player.RigidBody.AddForce(Vector2.right * player.DashSpeed * player.DashDirection, ForceMode2D.Impulse);
        player.SetVelocity(xInput + player.DashSpeed * player.DashDirection, 0);
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

