using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Scores _scores;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _displayScore;
    [SerializeField] private float _updateScoreMaxTime;
    private long _currentScore;
    private long _targetScore;
    private long _oneSecUpdateValue;
    private float _timeCounter;
    private float _startTime;
    Coroutine updateScore; 

    private void Awake()
    {
        Reset();
        _scores.OnScoreChanged += UpdateScore;
        _gameManager.OnRoundStarted += OnGameStarted;
        _gameManager.OnRoundRestarted += OnGameRestarted;
    }

    private void OnGameRestarted()
    {
        Reset();
        if (updateScore != null)
        {
            StopCoroutine(updateScore);
            updateScore = null;
        }
    }

    private void OnGameStarted(bool isGameStarted)
    {
        Reset();
        if (updateScore != null)
        {
            StopCoroutine(updateScore);
            updateScore = null; 
        }
    }

    private void UpdateScore(long previousScore, long newScore)
    {
        _targetScore = newScore;
        _currentScore = previousScore;
        _timeCounter = 0;
        _startTime = Time.realtimeSinceStartup;
        _oneSecUpdateValue = _targetScore - _currentScore;

        if (updateScore == null && gameObject.activeInHierarchy)
        {
            updateScore = StartCoroutine(UpdateDisplay());
        }       
    }

    
    internal void Reset()
    {
        _targetScore = 0;
        _displayScore.text = "0";
    }
        
    private IEnumerator UpdateDisplay() //animate display value
    {      
        while(_targetScore != _currentScore)
        {
            yield return null;
            _oneSecUpdateValue = _targetScore - _currentScore;
            _timeCounter = Time.realtimeSinceStartup - _startTime;
            _currentScore += (long)(_timeCounter * _oneSecUpdateValue / _updateScoreMaxTime);
            if (_timeCounter >= _updateScoreMaxTime || _currentScore >= _targetScore)
            {
                _currentScore = _targetScore;
                _displayScore.text = _currentScore.ToString("### ### ### ###");
                updateScore = null;
                yield break;
            }            
            _displayScore.text = _currentScore.ToString("### ### ### ###");
        }        
    }

   


}
