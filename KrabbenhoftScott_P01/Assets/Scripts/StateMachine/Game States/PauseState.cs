using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : State
{
    GameSM _stateMachine;
    GameController _controller;

    public PauseState(GameSM sm, GameController controller)
    {
        _stateMachine = sm;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered PauseState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();
    }
    
    public override void FixedTick()
    {
        base.FixedTick();
    }
}
