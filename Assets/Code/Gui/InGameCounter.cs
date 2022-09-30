using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCounter : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Panel _counterPanel;

    private void Awake()
    {
        _gameManager.OnRoundPaused += OnGamePaused;
    }

    private void OnGamePaused(bool isPaused)
    {
        if (isPaused)
            _counterPanel.TurnOff();
        else
            _counterPanel.TurnOn();
    }
}
