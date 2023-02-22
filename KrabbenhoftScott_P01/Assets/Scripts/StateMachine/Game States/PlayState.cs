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

        _controller.Resource_Manager.DecrementResources();
    }
    
    public override void FixedTick()
    {
        base.FixedTick();
    }

    protected override void SubscribeToInput()
    {
        TouchManager.OnFingerDown += TouchPressed;
    }

    protected override void UnsubscribeToInput()
    {
        TouchManager.OnFingerDown -= TouchPressed;
    }

    void TouchPressed(Vector2 touchPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(touchPos.x, touchPos.y, 0));
        RaycastHit info;

        if (Physics.Raycast(ray, out info) && info.collider.gameObject.layer == LayerMask.NameToLayer("Dice"))
        {
            _controller.RollDice();
        }
    }
}
