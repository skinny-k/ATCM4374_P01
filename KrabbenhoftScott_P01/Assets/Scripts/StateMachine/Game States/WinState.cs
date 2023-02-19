using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    GameSM _stateMachine;
    GameController _controller;

    public WinState(GameSM sm, GameController controller)
    {
        _stateMachine = sm;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Entered WinState");
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
