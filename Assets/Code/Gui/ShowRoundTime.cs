using System;
using TMPro;
using UnityEngine;

public class ShowRoundTime : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeTMP;

    public void UpdateTime(float seconds)
    {       
        TimeSpan time = TimeSpan.FromSeconds(seconds);
       _timeTMP.text = time.ToString(@"mm\:ss");
    }
}
