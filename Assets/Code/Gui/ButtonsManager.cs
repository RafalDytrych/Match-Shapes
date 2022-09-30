using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private CustomButton[] _buttons;
    [SerializeField] private GameManager _gameManager;
    public void Clicked(CustomButton customButton)
    {
        int length = _buttons.Length;
        for (int i = 0; i < length; i++)
        {
            if(!_buttons[i].Equals(customButton))
                _buttons[i].HideMe();
        }
    }

    public void Show()
    {
        if (!_gameManager.IsRoundStarted)
            return;
        int length = _buttons.Length;
        for (int i = 0; i < length; i++)
        {
            _buttons[i].ShowMe();
        }
    }
}
