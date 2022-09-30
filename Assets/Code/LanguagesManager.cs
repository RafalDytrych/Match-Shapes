using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguagesManager : MonoBehaviour
{ 

    [field: SerializeField] public int LocalesCount { get; private set; }
    [field: SerializeField] public int LocalesIndex { get; private set; }
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button[] _languageButtons;
    private Button _currentButton;
    private bool _dataInitialized;
    private bool _languageUpdated;

    public void Awake()
    {
        LocalesIndex = 0;
        _gameManager.OnPlayerDataInitialized += DataInitialized;
    }

    private void DataInitialized(PlayerData player)
    {
        LocalesIndex = player.LanguageIndex;
        if (_dataInitialized && !_languageUpdated) //if data is initialized but language is not updated
        {
            ChangeLocale(LocalesIndex);
            _languageUpdated = true;
        }
    }

    IEnumerator Start() //Wait untill localization is initialized
    {
        // Wait for the localization system to initialize, loading Locales, preloading etc.
        yield return LocalizationSettings.InitializationOperation;
        _dataInitialized = true;
        if (!_languageUpdated) //
        {
            ChangeLocale(LocalesIndex);
            _languageUpdated = true;
        }
        LocalesCount = LocalizationSettings.AvailableLocales.Locales.Count;
    }

    public void ChangeLocale(int index)
    {
        if (index == 0 || index == 1) //limited on 0 or 1 because only english and polish languages
            //are setted up
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
            if (_currentButton != null)
                _currentButton.interactable = true;
            _languageButtons[index].interactable = false;
            _currentButton = _languageButtons[index];
            LocalesIndex = index;
        }
    }
}
