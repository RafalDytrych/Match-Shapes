using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<PlayerData> OnPlayerDataInitialized;
    public event Action<bool> OnRoundStarted; //When user press play  = true, when go to menu = false;
    public event Action<bool> OnRoundPaused; //When game is paused = true, when game is unpaused = false
    public event Action OnRoundEnded; //When round ends
    public event Action OnRoundRestarted; //When player restart round
    [field: SerializeField] public bool IsPaused { get; private set; }
    [field: SerializeField] public bool IsRestarting { get; private set; }
    [field: SerializeField] public bool IsRoundStarted { get; private set; }
    
    
    private IEnumerator Start()
    {
        yield return null;
        Application.targetFrameRate = 60;
    }

    public void DataInitialized(PlayerData playerData)
    {
        OnPlayerDataInitialized?.Invoke(playerData);
    }
    public void StartRound()
    {
        IsRoundStarted = true;
        OnRoundStarted?.Invoke(true);
    }

    public void PauseGame(bool status)
    {
        if (!IsRoundStarted)
            return;
        IsPaused = status;
        OnRoundPaused?.Invoke(status);
    }

    public void PauseGameToggle()
    {
        IsPaused = !IsPaused;
        OnRoundPaused?.Invoke(IsPaused);
    }

    public void RoundEnded()
    {
        OnRoundEnded?.Invoke();
        IsRoundStarted = false;
    }

    public void AbandonROund()
    {
        IsPaused = false;
        IsRoundStarted = false;
        OnRoundStarted?.Invoke(false);        
    }

    public void RestartRound()
    {
        IsPaused = false;
        OnRoundRestarted?.Invoke();
    }
}
