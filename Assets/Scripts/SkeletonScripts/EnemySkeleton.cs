using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States
    public StateMachine<EnemyState> StateMachine { get; private set; }

    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }
    #endregion

    protected void Awake()
    {
        StateMachine = new StateMachine<EnemyState>();
        IdleState = new SkeletonIdleState(StateMachine, "isIdle", this);
        MoveState = new SkeletonMoveState(StateMachine, "isMoving", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.Update();
    }

    protected override void LateUpdate()
    {
        StateMachine.CurrentState.LateUpdate();
    }
}
