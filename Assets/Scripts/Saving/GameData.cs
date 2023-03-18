using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Data
    public int totalCoins = 0;
    public int highScore = 0;
    public Dictionary<string, bool> test = new Dictionary<string, bool>{ { "Test", true } };
    //Retrieve Data
    public GameData()
    {
        totalCoins = GlobalDataManager.Instance.GetCoins();
        highScore = GlobalDataManager.Instance.GetHighScore();
        test = new Dictionary<string, bool> { { "Test", true} };
    }
}
