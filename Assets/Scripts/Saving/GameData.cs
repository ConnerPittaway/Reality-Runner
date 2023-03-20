using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Data
    public int totalCoins = 0;
    public int highScore = 0;
    public SerializedDictionary<string, bool> boughtCharacters = new SerializedDictionary<string, bool>();
    //Retrieve Data
    public GameData()
    {
        totalCoins = GlobalDataManager.Instance.GetCoins();
        highScore = GlobalDataManager.Instance.GetHighScore();
        boughtCharacters = GlobalDataManager.Instance.GetBoughtItems();
    }
}
