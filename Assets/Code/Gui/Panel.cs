using System.Collections;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public static readonly int OUT = Animator.StringToHash("OUT");
    public static readonly int IN = Animator.StringToHash("IN");
    [field: SerializeField] public bool IsActive { get; set; }
    [SerializeField] protected Animator _animator;
    [SerializeField] internal float _turnOffTIme = 0.3f;
    public void TogglePanel()
    {
        if(IsActive)
            TurnOff();
        else
            TurnOn();
    }
    
    public void TogglePanel(bool status)
    {
        if (status == IsActive)
            return;
        if(!status)
            TurnOff();
        else
            TurnOn();
    }

    public virtual void TurnOn()
    {
        if (IsActive)
            return;
        StopAllCoroutines();
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(IN);
        }
        else
            _animator.SetTrigger(IN);
        IsActive = true;
    }

    public virtual void TurnOff()        
    {
        if (!IsActive)
            return;
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        _animator.SetTrigger(OUT);
        IsActive = false;

        StartCoroutine(TurnOffAfterTime());
    }

    private void OnEnable()
    {       
        IsActive = true;
    }

    private void OnDisable()
    {
        IsActive = false;
    }

    internal IEnumerator TurnOffAfterTime()
    {
       // _animator.SetTrigger(OUT);
        yield return new WaitForSeconds(_turnOffTIme);
        gameObject.SetActive(false);
    }
}