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
        Debug.Log("WallSlideState");
    }

    public override void Update()
    {
        base.Update();
        
        player.SetVelocity(xInput * player.MoveSpeed * 0.8f, player.RigidBody.velocity.y);

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
