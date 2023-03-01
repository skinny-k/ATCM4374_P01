using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashOutButton : MonoBehaviour
{
    [SerializeField] GameController _controller;
    
    public void CashOut()
    {
        _controller.GetComponent<GameSM>().ChangeState<WinState>();
    }
}
