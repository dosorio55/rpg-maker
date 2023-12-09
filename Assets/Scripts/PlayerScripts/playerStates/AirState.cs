using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerState
{
    public AirState(Player _player, StateMachine<PlayerState> _stateMachine, string _animBoolName) 
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player.IsGroundDetected())
            player.RigidBody.AddForce(Vector2.up * player.JumpForce, ForceMode2D.Impulse);
        else if (player.IsWallDetected())
            player.SetVelocity(player.MoveSpeed * 0.5f * -player.FacingDirection, player.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        float movingXVelocity = xInput != 0 ? xInput * player.MoveSpeed * 0.8f : player.RigidBody.velocity.x;

        player.SetVelocity(movingXVelocity, player.RigidBody.velocity.y);

        Debug.Log(player.IsWallDetected());

        ChangeStateController();
    }

    private void ChangeStateController()
    {
        if (player.IsGroundDetected() && player.RigidBody.velocity.y <= 0.01f)
            stateMachine.ChangeState(player.IdleState);

        if (player.IsWallDetected() && player.RigidBody.velocity.y <= 0.01f)
            stateMachine.ChangeState(player.WallSlideState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}