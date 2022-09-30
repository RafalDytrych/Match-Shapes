using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static int Click = Animator.StringToHash("Pressed");
    public static int Normal = Animator.StringToHash("Normal");
    public static int Hide = Animator.StringToHash("Hide");
    public static int Highlighted = Animator.StringToHash("Highlighted");


    private ButtonsManager _buttonsManager;
    private Animator _animator;
    public UnityEvent OnClick;
    [field: SerializeField] public bool IsClicked { get; set; }
    [SerializeField] private bool _isOn;
    [SerializeField] private AudioManager _audioManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _buttonsManager = GetComponentInParent<ButtonsManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsClicked)
            return;
        _animator.SetBool(Normal, false);
        _animator.SetTrigger(Highlighted);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsClicked)
            return;
        _animator.SetBool(Normal, true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsClicked)
            return;
        if (!_isOn)
        {
            _animator.SetTrigger(Click);
            _buttonsManager.Clicked(this);
            IsClicked = true;
        }
        OnClick?.Invoke();
        _audioManager.PlayButtonClick();
    }

    private void OnEnable()
    {
        IsClicked = false;
    }

    public void HideMe()
    {
        IsClicked = true;
        _animator.SetTrigger(Hide);
    }

    public void ShowMe()
    {
        IsClicked = false;
        _animator.SetTrigger(Normal);
    }
}
