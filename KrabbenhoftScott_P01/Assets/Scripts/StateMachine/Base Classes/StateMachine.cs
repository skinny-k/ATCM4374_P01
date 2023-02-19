using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; protected set; }
    protected State _previousState;
    protected List<State> _states = new List<State>();

    protected bool _inTransition = false;

    protected virtual void Update()
    {
        if (CurrentState != null && !_inTransition)
        {
            CurrentState.Tick();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (CurrentState != null && !_inTransition)
        {
            CurrentState.FixedTick();
        }
    }

    protected virtual void OnDestroy()
    {
        CurrentState?.Exit();
    }
    
    public void ChangeState(State newState)
    {
        if (newState == CurrentState || _inTransition)
        {
            return;
        }
        else
        {
            ChangeStateSequence(newState);
        }
    }

    public void ChangeState<T>() where T : State
    {
        if (_inTransition)
        {
            return;
        }
        else
        {
            foreach(State state in _states)
            {
                if (state is T)
                {
                    ChangeStateSequence(state);
                }
            }
        }
    }

    protected void ChangeStateSequence(State newState)
    {
        _inTransition = true;
        CurrentState?.Exit();
        StoreCurrentStateAsPrevious(newState);

        CurrentState = newState;
        CurrentState.Enter();
        _inTransition = false;
    }

    protected void StoreCurrentStateAsPrevious(State newState)
    {
        if (_previousState == null && newState != null)
        {
            _previousState = newState;
        }
        else if (_previousState != null && CurrentState != null)
        {
            _previousState = CurrentState;
        }
    }

    public void ChangeStateToPrevious()
    {
        if (_previousState != null)
        {
            ChangeState(_previousState);
        }
    }
}
