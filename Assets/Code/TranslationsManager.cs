using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class TranslationsManager : MonoBehaviour
{
    /// <summary>
    /// Put all needed translations he
    /// </summary>
    [SerializeField] private LocalizedString _otherGamesMessage;
    [SerializeField] private PopUpPanel _popUpPanel;

    public void ShowNoOtherGamesMessage()
    {
        _otherGamesMessage.GetLocalizedStringAsync().Completed += (x) => _popUpPanel.ShowPopup(x.Result);
    }
}
