using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    private EnemySkeleton enemySkeleton;
    public SkeletonIdleState(StateMachine<EnemyState> stateMachine, string animBoolName, EnemySkeleton enemySkeleton)
        : base(stateMachine, enemySkeleton, animBoolName)
    {
        this.enemySkeleton = enemySkeleton;
    }

    public override void Enter()
    {
        enemySkeleton.SetTimer(ChangeState, 1f);

        enemySkeleton.StopMoving();
    }

    public override void Update()
    {
        base.Update();
    }

    private void ChangeState()
    {
        stateMachine.ChangeState(enemySkeleton.MoveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
