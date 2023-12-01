using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerGroundedState
{
    public IdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        Debug.Log("IdleState");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
            stateMachine.ChangeState(player.MoveState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}