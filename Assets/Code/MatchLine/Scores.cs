using System;
using MatchLine;
using TMPro;
using UnityEngine;


public class Scores : MonoBehaviour
{
    /// <summary>
    /// Game result.
    /// 
    /// </summary>
   [SerializeField] private GameManager _gameManager;
   [SerializeField] private GameTime _gameTime;
   public event Action<long, long> OnScoreChanged;
   [field: SerializeField] public Score[] Records { get; private set; }
   [field: SerializeField] public int BaseSquarePoints { get; private set; }
   [field: SerializeField] public int RecordIndex { get; private set; }
   [SerializeField] private long _score;
   [SerializeField] private Panel _wonPanel;
   [SerializeField] private Panel _lostPanel;
   [SerializeField] private Panel _extraButtons;
   [SerializeField] private StartShapeSlotsManager _startShapeSlotsManager;
   [SerializeField] private ScoreDisplay _scoreDisplay;
   [SerializeField] private TMP_Text _lostPanelScoreTMP;
   [SerializeField] private TMP_Text _wonPanelScoreTMP;
   public long Score
   {
      get => _score;
      set
      {
            OnScoreChanged?.Invoke(_score, value);
            _score = value;        
      }
   }

   private void Awake()
   {
      _gameManager.OnRoundEnded += UpdateRecord;
      _gameManager.OnPlayerDataInitialized += player =>
      {
         Records = player.Records;
      };
      _gameManager.OnRoundStarted += x =>
      {
          if (x)
          {
              Score = 0;
          }
      };
      _gameManager.OnRoundRestarted += () => { 
          Score = 0;         
      };
   }

   public void CalculateIncomingPoints(int columns, int rows, int squares)
   {
      Score += GetValue(columns, rows, squares);
   }

   private long GetValue(int columns, int rows, int squares) //recipe of new score
   {
      int mult = (BaseSquarePoints * squares + (_startShapeSlotsManager.MadeFills/4))  * Board.Width * Board.Height;
      float formula = mult / 3 + mult / _gameTime.GetTimeValue() * (1 + (columns + rows) / 2);
      return (int)formula;
   }

   private void UpdateRecord()
   {
      RecordIndex = -1;
      for (int i = 0; i < Records.Length; i++) //find place on the scoreboard
      {
         if (Score > Records[i].value)
         {
            RecordIndex = i;
            break;
         }
      }

      if (RecordIndex < 0) //if place is <0 we did not update scoreboard and return from method
      {
         //Didnt break record
         _lostPanelScoreTMP.text = Score.ToString("### ### ###");
         _lostPanel.TurnOn(); //if player score is not in 10 top scores it will show "lost" panel, 
         _extraButtons.TurnOn(); //Show buttons after round ended
         return;
      }
      //Breaked some rekord
      _wonPanelScoreTMP.text = Score.ToString("### ### ###");
      _wonPanel.TurnOn(); //turn on won panel, if player is in top 10 records
      _extraButtons.TurnOn(); //Show buttons after round ended
      for (int i = Records.Length-1; i > RecordIndex; i--) //move down records
      {
         if (i - 1 >= 0)
         {
            Records[i].value = Records[i - 1].value;
            Records[i].date = Records[i - 1].date;
            Records[i].roundTime = Records[i - 1].roundTime;
         }
      }
      //Assign new record
      Records[RecordIndex].value = Score;
      Records[RecordIndex].date = DateTime.UtcNow.ToString("dd.MM.yyyy");
      Records[RecordIndex].roundTime = _gameTime.RoundTime;
   }
}

[System.Serializable]
public class Score
{
    public long value;
    public string date;
    public float roundTime;

    public string GetConvertedTime() => 
        TimeSpan.FromSeconds(roundTime).ToString(@"mm\:ss");    
}
