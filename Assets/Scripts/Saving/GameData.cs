using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Time of Save (Use to Compare If Data in Cloud is More Recent)
    public ulong timeOfLastSave = 0;

    //Game Data
    public int totalCoins = 0;
    public int highScore = 0;
    public GlobalDataManager.Characters currentlySelectedCharacter;

    //Purchases Data
    public SerializableDictionary<GlobalDataManager.Characters, bool> boughtCharacters = new SerializableDictionary<GlobalDataManager.Characters, bool>();
    public ulong timeRewardOpened = 0;

    //Retrieve Data
    public GameData()
    {
        timeOfLastSave = GlobalDataManager.Instance.timeOfLastSave;
        totalCoins = GlobalDataManager.Instance.GetCoins();
        highScore = GlobalDataManager.Instance.GetHighScore();
        boughtCharacters = GlobalDataManager.Instance.GetBoughtItems();
        currentlySelectedCharacter = GlobalDataManager.Instance.currentlySelectedCharacter;
        timeRewardOpened = GlobalDataManager.Instance.timeRewardOpened;
    }
}
