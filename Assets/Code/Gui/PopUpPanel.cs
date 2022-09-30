using System.Collections;
using UnityEngine;
using TMPro;

public class PopUpPanel : Panel
{
    [SerializeField] private  TMP_Text _displayText;
    private bool _isBusy;
    public void ShowPopup(string text)
    {
        if (_isBusy)
            return;
        _isBusy = true;
        _displayText.text = text;

        TurnOn();
        StartCoroutine(DelayTurnOff());
    }

    private IEnumerator DelayTurnOff()
    {
        yield return new WaitForSeconds(_turnOffTIme);
        TurnOff();
        yield return new WaitForSeconds(1f);
        _isBusy = false;
    }
}
