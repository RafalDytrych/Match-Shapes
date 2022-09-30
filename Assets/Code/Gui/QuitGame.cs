using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;
    
    public void CloseGame() //Assigned in editor -> Quit Button
    {
        _dataManager.Save();
        Application.Quit();
    }
}
