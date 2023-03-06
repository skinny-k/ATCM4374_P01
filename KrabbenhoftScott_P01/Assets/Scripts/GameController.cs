using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] TouchManager _input;
    [SerializeField] ResourceManager _resources;
    [SerializeField] GameDie _capitalDie;
    [SerializeField] GameDie _departmentDie;
    [SerializeField] DiceTray _tray;
    [SerializeField] string _gameSceneName = "GameplayScene";
    [SerializeField] string _mainMenuSceneName = "MainMenuScene";
    [SerializeField] GameObject _pausePanel;

    [Header("Win/Lose Feedback")]
    [SerializeField] GameObject _winPanel;
    [SerializeField] TMP_Text _winText;
    [SerializeField] GameObject _losePanel;
    [SerializeField] TMP_Text _loseText;
    [SerializeField] AudioClip _winSFX;
    [SerializeField] AudioClip _loseSFX;

    public ResourceManager Resource_Manager { get => _resources; }
    public GameDie Capital_Die { get => _capitalDie; }
    public GameDie Department_Die { get => _departmentDie; }
    public DiceTray Dice_Tray { get => _tray; }

    public UnityEvent<float> OnWin;
    public UnityEvent<Department> OnLose;
    
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

    public void PauseGame()
    {
        _capitalDie.Freeze();
        _departmentDie.Freeze();
        DisplayPausePanel(true);
        GetComponent<GameSM>().ChangeState<PauseState>();
    }

    public void UnpauseGame()
    {
        _capitalDie.Unfreeze();
        _departmentDie.Unfreeze();
        DisplayPausePanel(false);
        GetComponent<GameSM>().ChangeStateToPrevious();
    }

    public void DisplayPausePanel(bool state)
    {
        _pausePanel.SetActive(state);
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
    }

    public void DisplayWinPanel(bool state)
    {
        _winPanel.SetActive(state);
        if (state)
        {
            _pausePanel.SetActive(false);
            _losePanel.SetActive(false);
            AudioHelper.PlayClip2D(_winSFX, 1);
            OnWin?.Invoke(_resources.TotalCapital);
            _winText.text = "You successfully declared bankruptcy and collected $" + _resources.TotalCapital + "!";
        }
    }

    public void DisplayLosePanel(bool state)
    {
        _losePanel.SetActive(state);
        if (state)
        {
            _pausePanel.SetActive(false);
            _winPanel.SetActive(false);
            AudioHelper.PlayClip2D(_loseSFX, 1);
            OnLose?.Invoke(_resources.BankruptDepartment);
            _loseText.text = _resources.BankruptDepartment.name + " ran out of money! The board has removed you as CEO!";
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
