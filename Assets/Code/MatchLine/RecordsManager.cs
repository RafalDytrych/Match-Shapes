using System.Collections;
using TMPro;
using UnityEngine;

public class RecordsManager : Panel
{
    /// <summary>
    /// Script assigned on records panel,
    /// 
    /// </summary>
    [SerializeField] private RankScoreSlot[] _slots;
    [SerializeField] private RankScoreSlot _highlightedSlot;
    [SerializeField] private TMP_Text _highlightedSlotPlaceTMP;
    [SerializeField] private Scores _scores;
    [SerializeField] private GameManager _gameManager;
    private void Awake()
    {
        _gameManager.OnRoundEnded += UpdateMe;
        for (int i = 0; i < _scores.Records.Length; i++)
        {
            _slots[i].UpdateSlot(_scores.Records[i].date,_scores.Records[i].value.ToString("### ### ###"), _scores.Records[i].GetConvertedTime());
        }
       
        gameObject.SetActive(false);
    }
    private void Start()
    {
        UpdateRecords();
    }

    private void UpdateMe()
    {
        UpdateRecords();
    }
    private void UpdateRecords() //Refresh slots
    {
        for (int i = 0; i < _scores.Records.Length; i++)
        {
            _slots[i].UpdateSlot(_scores.Records[i].date
                ,_scores.Records[i].value.ToString("### ### ###")
                , _scores.Records[i].GetConvertedTime()
                );
        }
        int index = _scores.RecordIndex;
        if (index < 0)
            index = 0;
        _highlightedSlot.UpdateSlot(_scores.Records[index].date,_scores.Records[index].value.ToString("### ### ###"), _scores.Records[index].GetConvertedTime());
        _highlightedSlotPlaceTMP.text = (index + 1).ToString();
    }
}
