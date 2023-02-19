using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollState : State
{
    GameSM _stateMachine;
    GameController _controller;
    int _capitalRolled = 0;
    int _departmentRolled = 0;
    bool _capitalLanded = false;
    bool _departmentLanded = false;

    public DiceRollState(GameSM sm, GameController controller)
    {
        _stateMachine = sm;
        _controller = controller;
    }

    public override void Enter()
    {
        base.Enter();

        _capitalRolled = 0;
        _departmentRolled = 0;
        _capitalLanded = false;
        _departmentLanded = false;

        Debug.Log("Entered DiceRollState");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();

        if (_capitalLanded && _departmentLanded)
        {
            GiveCapitalToDepartment(_capitalRolled, _departmentRolled);
            _stateMachine.ChangeState<PlayState>();
        }
    }
    
    public override void FixedTick()
    {
        base.FixedTick();
    }

    protected override void SubscribeToInput()
    {
        _controller.Capital_Die.OnLand += CapitalDieFall;
        _controller.Department_Die.OnLand += DepartmentDieFall;
    }

    protected override void UnsubscribeToInput()
    {
        _controller.Capital_Die.OnLand -= CapitalDieFall;
        _controller.Department_Die.OnLand -= DepartmentDieFall;
    }

    void CapitalDieFall(int result)
    {
        _capitalLanded = true;
        _capitalRolled = result;
    }

    void DepartmentDieFall(int result)
    {
        _departmentLanded = true;
        _departmentRolled = result;
    }

    void GiveCapitalToDepartment(int capital, int department)
    {
        //
    }
}
