using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<EState> where EState : BaseState
{
    public EState CurrentState { get; private set; }

    public void Initialize(EState _startingState)
    {
        Debug.Log(_startingState);
        CurrentState = _startingState;
        CurrentState.Enter();
    }

    public void ChangeState(EState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        Debug.Log(CurrentState);
        CurrentState.Enter();
    }
}
