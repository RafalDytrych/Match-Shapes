using UnityEngine;
using MatchLine;

public class PlayerDataManager : MonoBehaviour
{
    [Header("References to other scripts")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ShapesManager _shapesManager;
    [SerializeField] private LanguagesManager _languagesManager;
    [Space(5f),Header("Members")]
    [SerializeField] private PlayerData _defaultPlayerData;
    [SerializeField] private Scores _scores;
    [field: SerializeField] public PlayerData PlayerData { get; private set; }


    public void SetPlayerData(PlayerData data, bool isDefault = false)
    {
        PlayerData = isDefault ? _defaultPlayerData : data;
        _gameManager.DataInitialized(PlayerData);
    }

    public void CollectData()
    { 
        PlayerData.GameTime += Time.realtimeSinceStartup;
        PlayerData.SquaresSpeedOffset = _shapesManager.SquaresSpeedOffset;
        PlayerData.LanguageIndex = _languagesManager.LocalesIndex;
        PlayerData.Records = _scores.Records;   
    }
}
