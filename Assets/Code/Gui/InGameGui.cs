using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameGui : MonoBehaviour
{
    [SerializeField] private Panel _pauseButton;
    [SerializeField] private GameManager _gameManager;
    private void Awake()
    {
        _gameManager.OnRoundStarted += b =>
        {
            _pauseButton.TogglePanel(b);
        };
        _gameManager.OnRoundEnded += () => _pauseButton.TogglePanel(false);
    }
}
