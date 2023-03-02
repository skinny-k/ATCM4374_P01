using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHoldState : State
{
    GameSM _stateMachine;
    GameController _controller;

    public DiceHoldState(GameSM sm, GameController controller)
    {
        _stateMachine = sm;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered DiceHoldState");

        _controller.Capital_Die.GetComponent<Rigidbody>().useGravity = false;
        _controller.Department_Die.GetComponent<Rigidbody>().useGravity = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();

        _controller.Resource_Manager.DrainCapital();
    }
    
    public override void FixedTick()
    {
        base.FixedTick();
    }

    protected override void SubscribeToInput()
    {
        TouchManager.OnFingerMove += MoveDice;
        TouchManager.OnFingerUp += _controller.RollDice;
    }

    protected override void UnsubscribeToInput()
    {
        TouchManager.OnFingerMove -= MoveDice;
        TouchManager.OnFingerUp -= _controller.RollDice;
    }

    protected void MoveDice(Vector2 touchPos)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 15f));

        _controller.Capital_Die.transform.position = worldPos + Vector3.left;
        _controller.Department_Die.transform.position = worldPos - Vector3.left;

        _controller.Capital_Die.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 90f);
        _controller.Department_Die.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 90f);
    }
}
