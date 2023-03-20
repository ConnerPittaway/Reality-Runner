using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    //Instance
    public static GlobalDataManager Instance;

    //Save and Load System
    private JsonDataHandler dataHandler;

    //Data
    private int totalCoins = 0;
    private int highScore = 0;
    public SerializedDictionary<string, bool> boughtCharacters = new SerializedDictionary<string, bool> { { "Shroud", true } };
    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;
            //GameData data = SaveSystem.LoadData();
            this.dataHandler = new JsonDataHandler(Application.persistentDataPath, "GameData");
            GameData data = dataHandler.LoadData();
            if(data == null)
            {
                data = new GameData();
            }
            totalCoins = data.totalCoins;
            highScore = data.highScore;
            boughtCharacters = data.boughtCharacters;
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
        boughtCharacters["Shroud"] = false;
        dataHandler.SaveData();
        //SaveSystem.SaveData();
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
            dataHandler.SaveData();
            //SaveSystem.SaveData();
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public SerializedDictionary<string, bool> GetBoughtItems()
    {
        return boughtCharacters;
    }

    //Unlocks

}
