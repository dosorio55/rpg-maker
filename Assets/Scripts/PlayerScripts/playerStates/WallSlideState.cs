using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : PlayerState
{
    public WallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.IsWallSliding = true;
    }

    public override void Update()
    {
        base.Update();

        WallFallingSpeed();

        ChangeStateController();
    }

    private void WallFallingSpeed()
    {
        if (xInput == player.FacingDirection)
            player.SetVelocity(player.RigidBody.velocity.x, player.RigidBody.velocity.y * player.WallSlideSpeed);
        else if (xInput == -player.FacingDirection)
            player.SetVelocity(xInput * player.MoveSpeed, player.RigidBody.velocity.y);
        else
            player.SetVelocity(player.RigidBody.velocity.x, player.RigidBody.velocity.y * 0.97f);
    }

    private void ChangeStateController()
    {
        if ((player.IsGroundDetected() && player.RigidBody.velocity.y <= 0.01f) || !player.IsWallDetected())
            stateMachine.ChangeState(player.IdleState);

        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(player.AirState);

    }
    public override void Exit()
    {
        player.IsWallSliding = false;
        base.Exit();
    }
}
