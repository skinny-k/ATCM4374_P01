using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : State
{
    GameSM _stateMachine;
    GameController _controller;

    public PlayState(GameSM sm, GameController controller)
    {
        _stateMachine = sm;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered PlayState");
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

    protected override void SubscribeToInput()
    {
        TouchManager.OnTouchPress += TouchPressed;
    }

    protected override void UnsubscribeToInput()
    {
        TouchManager.OnTouchPress -= TouchPressed;
    }

    void TouchPressed(Vector2 touchPos)
    {
        _controller.RollDice();
        _stateMachine.ChangeState<DiceRollState>();
    }
}
