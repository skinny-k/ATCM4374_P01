using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] ResourceManager _resources;
    [SerializeField] CapitalDie _capitalDie;
    [SerializeField] DepartmentDie _departmentDie;
    [SerializeField] DiceTray _tray;
    [SerializeField] string _gameSceneName = "GameplayScene";
    [SerializeField] string _mainMenuSceneName = "MainMenuScene";

    public ResourceManager Resource_Manager { get => _resources; }
    public CapitalDie Capital_Die { get => _capitalDie; }
    public DepartmentDie Department_Die { get => _departmentDie; }
    public DiceTray Dice_Tray { get => _tray; }
    
    public void RollDice()
    {
        if (_capitalDie != null && _departmentDie != null)
        {
            _capitalDie.Roll();
            _departmentDie.Roll();

            GetComponent<GameSM>().ChangeState<DiceRollState>();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
