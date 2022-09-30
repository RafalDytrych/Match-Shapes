using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [field: SerializeField] public Panel MainMenu { get; private set; } 
    [field: SerializeField] public Panel Settings { get; private set; }
    [field: SerializeField] public Panel Records { get; private set; }
    [field: SerializeField] public Panel PauseButton { get; private set; }
    [field: SerializeField] public Panel PausePanel { get; private set; }
    [field: SerializeField] public Panel WonPanel { get; private set; }
    [field: SerializeField] public Panel LostPanel { get; private set; }
    [field: SerializeField] public Panel ExtraButtonsOnRoundEnded { get; private set; }
    [field: SerializeField] public StartGamePanel MainAnimationPanel { get; private set; }
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Panel _mainBackground;
    [SerializeField] private ButtonsManager _buttonsManager;

    private List<Panel> _openedPanels;
    [SerializeField] private Panel[] _closePanels;

    private void Awake()
    {
        InitializeAndClosePanelsOnGameStart();
        
        _gameManager.OnRoundStarted += (x) =>
        {
            if (!_gameManager.IsRestarting)
            {
                if (x)
                    _mainBackground.TurnOff();
                else
                {
                    _mainBackground.TurnOn();
                    
                    if(PausePanel.IsActive)
                        PausePanel.TurnOff();
                }
                MainMenu.TogglePanel(!x);
            }
        };
        _gameManager.OnRoundPaused += (isPaused) =>
        {
            if(isPaused)
            {
                PauseButton.TurnOff();
                PausePanel.TurnOn();
            }
            else
            {
                PauseButton.TurnOn();
                PausePanel.TurnOff();
            }
        };        
    }
    
    private void InitializeAndClosePanelsOnGameStart()
    {
        _openedPanels = new List<Panel>();
        _mainBackground.TurnOn();
        foreach (var panel in _closePanels)
        {
            panel.TurnOff();
        }
    }

    public void ToggleMainMenu()
    {
        if (_gameManager.IsRoundStarted)
            return;
        _buttonsManager.Show();
        MainMenu.TogglePanel();

    }

    public void ToggleSettings()
    {
        Settings.TogglePanel();
    }

    public void ToggleRecords()
    {
        Records.TogglePanel();
    }

    public void CloseSettingsPanel()
    {
        Settings.TurnOff();
        ToggleMainMenu();
    }

    public void CloseRecordsPanel()
    {
        Records.TurnOff();
        ToggleMainMenu();

        MainAnimationPanel.TurnOff();
        //_mainBackground.TurnOn();
    }

    public void OpenPanel(Panel panel)
    {
        if (!_openedPanels.Contains(panel))
        {
            _openedPanels.Add(panel);            
        }
        panel.TurnOn();
    }

    public void ClosePanel(Panel panel)
    {
        if (_openedPanels.Contains(panel))
        {
            _openedPanels.Remove(panel);            
        }
        panel.TurnOff();
    }

    public void CloseOpennedPanels()
    {
        foreach (var panel in _openedPanels)
        {
            panel.TurnOff();
        }
    }

    public void CloseRoundEndedPanel()
    {
        if (WonPanel.IsActive)
            WonPanel.TurnOff();
        if (LostPanel.IsActive)
            LostPanel.TurnOff();
        _gameManager.AbandonROund();
        ExtraButtonsOnRoundEnded.TurnOff();
    }
}
