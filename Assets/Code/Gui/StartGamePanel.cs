using System;
using System.Collections;
using UnityEngine;

public class StartGamePanel : Panel
{
    [SerializeField] private float _outOffsetTime;
    [SerializeField] private GameManager _gameManager;
    private Coroutine _coroutine;
    private void Awake()
    {
        _gameManager.OnRoundStarted += b =>
        {            
            TurnOn();
            _coroutine = StartCoroutine(ActionAfterTime(TurnOff, _outOffsetTime));
            
        };
        _animator = GetComponent<Animator>();
        _gameManager.OnRoundRestarted += () =>
        {
            TurnOn();
            _coroutine = StartCoroutine(ActionAfterTime(TurnOff, _outOffsetTime));
        };
        _gameManager.OnRoundEnded += TurnOn;
        gameObject.SetActive(false);
    }

    public override void TurnOn()
    {
        if (IsActive)
            return;
        _animator.SetBool(OUT, false);
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        if (_coroutine != null)
            StopCoroutine(_coroutine);
            _animator.SetTrigger(IN);
            IsActive = true;
    }

    public override void TurnOff()
    {
        if (!IsActive)
            return;
        _animator.SetBool(OUT, true);
        _coroutine = StartCoroutine(ActionAfterTime(() => { 
            IsActive = false;
        }, 0.5f));
    }

    private IEnumerator ActionAfterTime(Action action, float offsetTime)
    {
        yield return new WaitForSeconds(offsetTime);
        action?.Invoke();
    }
}