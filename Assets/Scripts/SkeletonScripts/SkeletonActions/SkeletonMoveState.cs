using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    private EnemySkeleton enemySkeleton;

    public SkeletonMoveState(StateMachine<EnemyState> stateMachine, string animBoolName, EnemySkeleton enemySkeleton)
        : base(stateMachine, enemySkeleton, animBoolName)
    {
        this.enemySkeleton = enemySkeleton;
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemySkeleton.SetVelocity(2 * enemySkeleton.FacingDirection, enemySkeleton.RigidBody.velocity.y);
    }

    public override void LateUpdate()
    {
        if (enemySkeleton.IsWallDetected() || !enemySkeleton.IsGroundDetected())
        {
            enemySkeleton.Flip();
            stateMachine.ChangeState(enemySkeleton.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}