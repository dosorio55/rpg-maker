using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public StateMachine<PlayerState> StateMachine { get; private set; }

    protected void Awake()
    {
        StateMachine = new StateMachine<PlayerState>();

    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }
}
