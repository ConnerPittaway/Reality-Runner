using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    //Instance
    public static GlobalDataManager Instance;

    //Selected Character
    public enum Characters
    {
        SHROUD,
        SHROUD2,
        SHROUD3
    }
    
    //Save and Load System
    private JsonDataHandler dataHandler;

    //Game Data
    private int totalCoins = 0;
    private int highScore = 0;
    public Characters currentlySelectedCharacter;

    //Add in Editor -> Has to be public
    public SerializableDictionary<Characters, bool> boughtCharacters;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;

            //Load Game Data
            this.dataHandler = new JsonDataHandler(Application.persistentDataPath, "GameData");
            GameData data = dataHandler.LoadData<GameData>();

            if(data == null)
            {
                data = new GameData();
            }

            totalCoins = data.totalCoins;
            highScore = data.highScore;
            foreach(var character in data.boughtCharacters)
            {
                if(boughtCharacters.ContainsKey(character.Key))
                {
                    boughtCharacters[character.Key] = character.Value;
                }
            }
            currentlySelectedCharacter = data.currentlySelectedCharacter;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region GameData
    //Coins
    public void AlterCoins(int value)
    {
        totalCoins += value;
        SaveData();
    }
    public int GetCoins()
    {
        return totalCoins;
    }

    //High Score
    public void UpdateHighScore(int score)
    {
        if(score > highScore)
        {
            highScore = score;
            SaveData();
        }
    }
    public int GetHighScore()
    {
        return highScore;
    }

    public SerializableDictionary<Characters, bool> GetBoughtItems()
    {
        var charactersBought = new SerializableDictionary<Characters, bool>();
        foreach (var x in boughtCharacters)
        {
            charactersBought.Add(x.Key, x.Value);
        }
        return charactersBought;
    }
    //Unlocks


    public void SaveData()
    {
        dataHandler.SaveData<GameData>();
    }
    #endregion
}
