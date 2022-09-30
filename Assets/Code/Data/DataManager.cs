using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerDataManager _playerDataManager;
    [SerializeField] private string _savePath;
    private string _path;

    private void Awake()
    {
        _gameManager.OnRoundEnded += SaveGame;
    }

    private void SaveGame()
    {
        Save();
    }

    private IEnumerator Start()
    {
        _path = Application.persistentDataPath + "/" + _savePath;
        yield return null;
        LoadData();
    }

    private void LoadData()
    {
        if (File.Exists(_path))
        {
            Load();     
        }
        else
        {
            _playerDataManager.SetPlayerData(null, true);
            Save();
        }
    }

    public void Save()
    {
        _playerDataManager.CollectData(); //update data values
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(_path, FileMode.OpenOrCreate);
        bf.Serialize(file, _playerDataManager.PlayerData);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(_path, FileMode.Open);
        PlayerData loadedPlayerData = (PlayerData)bf.Deserialize(file);
        file.Close();
        _playerDataManager.SetPlayerData(loadedPlayerData);
    }
}