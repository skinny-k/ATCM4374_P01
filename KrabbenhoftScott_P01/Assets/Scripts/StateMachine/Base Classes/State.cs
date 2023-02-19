using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    
    public float StateDuration { get; protected set; } = 0;
    
    public virtual void Enter()
    {
        StateDuration = 0;

        SubscribeToInput();
    }

    public virtual void Exit()
    {
        UnsubscribeToInput();
    }

    public virtual void Tick()
    {
        StateDuration += Time.deltaTime;
    }

    public virtual void FixedTick()
    {
        //
    }

    protected virtual void SubscribeToInput()
    {
        //
    }

    protected virtual void UnsubscribeToInput()
    {
        //
    }
}
