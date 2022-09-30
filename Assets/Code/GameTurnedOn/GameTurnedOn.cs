using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTurnedOn : MonoBehaviour
{
    [SerializeField] private float _loadingTime;
    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private TMP_Text _loadingTextTMP;
    [SerializeField] private Button _playButton;
    [SerializeField] private float _switchScenesOffset;
    
    private AsyncOperation _ao;
    private float _timeCounter;
    private IEnumerator Start()
    {
        
        Application.targetFrameRate = 120;
        _loadingSlider.value = 0;
        _playButton.onClick.RemoveAllListeners();
        _playButton.onClick.AddListener( ()=>
        {
            _ao.allowSceneActivation = true;
            StartCoroutine(UnloadLoader());
        });
        LoadGameScene();
        do
        {
            yield return null;
        } while (_ao.progress < 0.9f);
        
        _loadingTime = 0.5f;
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(UnloadLoader());
        // _playButton.gameObject.SetActive(true);
    }

    private IEnumerator UnloadLoader()
    {
        yield return new WaitForSeconds(1.2f);
        yield return null;
        SceneManager.UnloadSceneAsync(0);
    }

    private void Update()
    {
        UpdateLoadingSlider();
    }

    private void UpdateLoadingSlider()
    {
        _timeCounter += Time.deltaTime;
        _loadingSlider.value = _timeCounter / _loadingTime;
    }

    private void LoadGameScene()
    {
        _ao = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        _ao.allowSceneActivation = true;
        // _ao.allowSceneActivation = false;
        _ao.completed += (x) => SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        
    }

   

}
