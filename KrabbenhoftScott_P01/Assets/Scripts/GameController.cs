using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] ResourceManager _resources;
    [SerializeField] GameDie _capitalDie;
    [SerializeField] GameDie _departmentDie;
    [SerializeField] DiceTray _tray;
    [SerializeField] string _gameSceneName = "GameplayScene";
    [SerializeField] string _mainMenuSceneName = "MainMenuScene";

    [Header("Win/Lose Feedback")]
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _losePanel;
    [SerializeField] AudioClip _winSFX;
    [SerializeField] AudioClip _loseSFX;

    public ResourceManager Resource_Manager { get => _resources; }
    public GameDie Capital_Die { get => _capitalDie; }
    public GameDie Department_Die { get => _departmentDie; }
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

    public void DisplayWinPanel(bool state)
    {
        _winPanel.SetActive(state);
        if (state)
        {
            AudioHelper.PlayClip2D(_winSFX, 1);
        }
    }

    public void DisplayLosePanel(bool state)
    {
        _losePanel.SetActive(state);
        if (state)
        {
            AudioHelper.PlayClip2D(_loseSFX, 1);
        }
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
