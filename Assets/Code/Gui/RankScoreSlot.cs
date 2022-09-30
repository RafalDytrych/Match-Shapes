using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankScoreSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _date;
    [SerializeField] private TMP_Text _value;
    [SerializeField] private TMP_Text _roundTime;

    public void UpdateSlot(string date, string value, string roundTime)
    {
        _date.text = date;
        _value.text = value;
        _roundTime.text = roundTime;
    }
}
