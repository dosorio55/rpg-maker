using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<EState> where EState : BaseState
{
    public EState CurrentState { get; private set; }

    public void Initialize(EState _startingState)
    {
        CurrentState = _startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(EState _newState)
    {
        CurrentState.ExitState();
        CurrentState = _newState;
        Debug.Log(CurrentState);
        CurrentState.EnterState();
    }
}
