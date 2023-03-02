using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameController))]
public class GameSM : StateMachine
{
    public SetupState Setup { get; private set; }
    public PlayState Play { get; private set; }
    public DiceHoldState DiceHold { get; private set; }
    public DiceRollState DiceRoll { get; private set; }
    public WinState Win { get; private set; }
    public LoseState Lose { get; private set; }
    
    GameController _controller;

    void Awake()
    {
        _controller = GetComponent<GameController>();

        Setup = new SetupState(this, _controller);
        Play = new PlayState(this, _controller);
        DiceHold = new DiceHoldState(this, _controller);
        DiceRoll = new DiceRollState(this, _controller);
        Win = new WinState(this, _controller);
        Lose = new LoseState(this, _controller);

        _states.Add(Setup);
        _states.Add(Play);
        _states.Add(DiceHold);
        _states.Add(DiceRoll);
        _states.Add(Win);
        _states.Add(Lose);
    }

    void Start()
    {
        ChangeState<SetupState>();
    }
}
