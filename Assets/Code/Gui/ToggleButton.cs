using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public bool IsOn { get; private set;}
    [SerializeField] private Image _reference;
    [SerializeField] private Sprite[] _bg;
    [SerializeField] private Transform _handleTransform;
    public Action<bool> OnToggleChanged;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Set(!IsOn);
    }

    public void Set(bool status)
    {
        IsOn = status;
        OnToggleChanged ?.Invoke(IsOn);
        _reference.sprite = IsOn ? _bg[0] : _bg[1];
        _handleTransform.localPosition = IsOn ? new Vector3(50, 0, 0) : new Vector3(-50, 0, 0);
    }
}
