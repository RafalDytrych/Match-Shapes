using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] private PanelsManager _panelsManager;
    [field: SerializeField] public Panel OpennedPanel { get; private set; }
    [field: SerializeField] public Panel ExtraButtonsPanel { get; private set; }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //escape is back button on android
        {
            if (_gameManager.IsRoundStarted && !_gameManager.IsPaused && OpennedPanel == null)
            {
                _gameManager.PauseGame(true);
            }
            else if(_gameManager.IsRoundStarted && _gameManager.IsPaused)
            {
                _gameManager.PauseGame(false);
                if(OpennedPanel != null)
                {
                    OpennedPanel.TurnOff();
                    OpennedPanel = null;
                }
            }
            else if(ExtraButtonsPanel.IsActive)
            {
                _panelsManager.CloseRoundEndedPanel();
            }
            else if (OpennedPanel != null)
            {
                OpennedPanel.TurnOff();
                _panelsManager.ToggleMainMenu();
                OpennedPanel = null;
            }
    }
}

    public void OpenPanel(Panel panel)
    {
        OpennedPanel = panel;
    }

    public void OnPanelClosed()
    {
        OpennedPanel = null;
    }
}
