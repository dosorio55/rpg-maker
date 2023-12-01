using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerState
{
    public JumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        Debug.Log("JumpState");
        base.Enter();
        player.RigidBody.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.MoveSpeed, player.RigidBody.velocity.y);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}