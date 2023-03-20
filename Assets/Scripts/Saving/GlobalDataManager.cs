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
    public Characters currentlySelectedCharacter;

    //Save and Load System
    private JsonDataHandler dataHandler;

    //Data
    private int totalCoins = 0;
    private int highScore = 0;

    //Add in Editor
    public SerializableDictionary<Characters, bool> boughtCharacters;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;
            this.dataHandler = new JsonDataHandler(Application.persistentDataPath, "GameData");
            GameData data = dataHandler.LoadData();
            if(data == null)
            {
                data = new GameData();
            }

            //Load Data
            totalCoins = data.totalCoins;
            highScore = data.highScore;
            foreach(var character in data.boughtCharacters)
            {
                if(boughtCharacters.ContainsKey(character.Key))
                {
                    boughtCharacters[character.Key] = character.Value;
                }
            }


            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    //Coins
    //Add or Subtract Coins
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

    public void SaveData()
    {
        dataHandler.SaveData();
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

}
