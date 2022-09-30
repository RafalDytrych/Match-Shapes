using UnityEngine;

public class PanelExtra : Panel //turn on and off other panels with me
{
    [SerializeField] private Panel[] _turnOffPanels;
    [SerializeField] private Panel[] _turnOnPanels;
    public override void TurnOff()
    {
        base.TurnOff();
        for (int i = 0; i < _turnOffPanels.Length; i++)
        {
            if(_turnOffPanels[i].IsActive)
                _turnOffPanels[i].TurnOff();
        }
    }

    public override void TurnOn()
    {
        base.TurnOn();
        for (int i = 0; i < _turnOnPanels.Length; i++)
        {
            if(!_turnOnPanels[i].IsActive)
                _turnOnPanels[i].TurnOn();
        }        
    }
}
