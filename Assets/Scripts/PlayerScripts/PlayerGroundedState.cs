using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    private bool ableToDash = true;
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.JumpState);

        if (Input.GetKeyDown(KeyCode.LeftShift) && player.IsGroundDetected() && ableToDash)
        {
            ableToDash = false;
            player.StartCoroutine(player.StartTimer(() => ableToDash = true, player.DashCooldown));
            stateMachine.ChangeState(player.DashState);

        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}