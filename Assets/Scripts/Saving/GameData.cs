using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Data
    public int totalCoins = 0;
    public int highScore = 0;
    public SerializableDictionary<GlobalDataManager.Characters, bool> boughtCharacters = new SerializableDictionary<GlobalDataManager.Characters, bool>();
    public GlobalDataManager.Characters currentlySelectedCharacter;

    //Retrieve Data
    public GameData()
    {
        totalCoins = GlobalDataManager.Instance.GetCoins();
        highScore = GlobalDataManager.Instance.GetHighScore();
        boughtCharacters = GlobalDataManager.Instance.GetBoughtItems();
        currentlySelectedCharacter = GlobalDataManager.Instance.currentlySelectedCharacter;
    }
}
