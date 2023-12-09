using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseState
{
    protected StateMachine<EnemyState> stateMachine;
    protected Enemy enemyBase;
    private string animBoolName;

    public EnemyState(StateMachine<EnemyState> stateMachine, Enemy enemy, string animBoolName)
    {
        this.enemyBase = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        enemyBase.Animator.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
    }

    public virtual void LateUpdate()
    {
    }

    public override void Exit()
    {
        enemyBase.Animator.SetBool(animBoolName, false);
    }
}
