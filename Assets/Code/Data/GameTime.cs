using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameTime : MonoBehaviour
{
    public UnityEvent<float> OnTimeChanged;
    [SerializeField] private GameManager _gameManager;

    [field: SerializeField] public float RoundTime { get; private set; }
    private bool _canCount;
    private Coroutine _countTime;
    private void Awake()
    {
        _gameManager.OnRoundStarted += GameStatusChanged;
        _gameManager.OnRoundPaused += PauseTimer;
        _gameManager.OnRoundRestarted += RestartTimer;
    }

    private void RestartTimer()
    {
        if (_countTime != null)
            StopCoroutine(_countTime);
        RoundTime = 0;
        _countTime = StartCoroutine(CountTime());
    }

    private void PauseTimer(bool isPaused)
    {        
        if (isPaused)
            StopCoroutine(_countTime);
        else
            _countTime = StartCoroutine(CountTime());        
    }

    private void GameStatusChanged(bool obj)
    {
        if (_countTime != null)
            StopAllCoroutines();
        _canCount = obj;
        if (obj)
        {
            RoundTime = 0;
            OnTimeChanged?.Invoke(0);
            _countTime = StartCoroutine(CountTime());
        }               
    }
    private IEnumerator CountTime()
    {        
        yield return new WaitForSeconds(2f);
        while (_canCount)
        {
            yield return new WaitForSeconds(1f);
            RoundTime += 1f;
            OnTimeChanged?.Invoke(RoundTime);
        }
    }
   
    public int GetTimeValue()
    {
        if (RoundTime > 650)
            return 1;
        if (RoundTime > 500)
            return 2;
        if (RoundTime > 400)
            return 3;
        if (RoundTime > 350)
            return 4;
        if (RoundTime > 300)
            return 5;
        if (RoundTime > 250)
            return 6;
        if (RoundTime > 200)
            return 7;
        if (RoundTime > 150)
            return 8;
        if (RoundTime > 100)
            return 9;
        if (RoundTime > 50)
            return 10;
        return 11;
    }
}
