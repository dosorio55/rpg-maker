using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private float comboCooldown = 0.5f;
    private float lastAttackTime = 0f;
    private int comboCounter = 0;
    public AttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        if (Time.time - lastAttackTime > comboCooldown || comboCounter > 2)
            comboCounter = 0;
        
        player.Animator.SetInteger("comboCounter", comboCounter);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
        comboCounter++;
    }



}
