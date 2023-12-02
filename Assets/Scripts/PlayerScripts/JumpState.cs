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
        base.Enter();
        player.RigidBody.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
    }

    public override void Update()
    {
        base.Update();



        player.SetVelocity(xInput * player.MoveSpeed, player.RigidBody.velocity.y);

        ChangeStateController();
    }

    private void ChangeStateController()
    {
        if (player.IsGroundDetected() && player.RigidBody.velocity.y <= 0.01f)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}