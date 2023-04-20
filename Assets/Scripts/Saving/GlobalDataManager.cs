using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    //Instance
    public static GlobalDataManager Instance;

    //Time Of Save
    public ulong timeOfLastSave = 0;

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
    private bool hasPremium = false;

    //Rewards Timer
    public ulong timeRewardOpened = 0;

    //Cloud Data
    public bool checkCloud = false;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        StartCoroutine(AwakeInternal());
    }

    private IEnumerator AwakeInternal()
    {
        //Intialise Game Data Handler
        this.dataHandler = new JsonDataHandler(Application.persistentDataPath, "GameData");
        GameData data = dataHandler.LoadData<GameData>();
        if (data == null)
        {
            Debug.Log("Default Data");

            //No Data Found Ask For Cloud
            checkCloud = true;

            if (checkCloud)
            {
                while (!FirebaseManager.Instance.fireBaseReady)
                {
                    yield return new WaitForEndOfFrame();
                }

                Debug.Log("Loading Cloud");
                data = dataHandler.LoadCloudData<GameData>();
            }
        }
        else
        {
            Debug.Log("Local Data");
        }

        //No Data Found
        if(data == null)
        {
            data = new GameData();
        }

        //Time of Save
        timeOfLastSave = data.timeOfLastSave;

        //Game Data
        totalCoins = data.totalCoins;
        highScore = data.highScore;

        //Characters
        foreach (var character in data.boughtCharacters)
        {
            if (boughtCharacters.ContainsKey(character.Key))
            {
                boughtCharacters[character.Key] = character.Value;
            }
        }
        currentlySelectedCharacter = data.currentlySelectedCharacter;

        //Rewards
        timeRewardOpened = data.timeRewardOpened;

        //Premium
        hasPremium = data.hasPremium;

        yield return null;
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
    public bool GetPremiumStatus()
    {
        return hasPremium;
    }

    public void SetPremiumStatus(bool status)
    {
        hasPremium = status;
        SaveData();
    }

    public void UnlockAllCharacters()
    {
        foreach (var key in boughtCharacters.Keys.ToList())
        {
            boughtCharacters[key] = true;
        }
        SaveData();
    }

    public void SaveData()
    {
        //Update Time of Save
        timeOfLastSave = (ulong)DateTime.Now.Ticks;
        dataHandler.SaveData<GameData>();
    }

    public void UpdateData()
    {
        //Try Loading from cloud
        GameData data = dataHandler.LoadData<GameData>();

        //Time of Save
        timeOfLastSave = data.timeOfLastSave;

        //Game Data
        totalCoins = data.totalCoins;
        highScore = data.highScore;

        //Characters
        foreach (var character in data.boughtCharacters)
        {
            if (boughtCharacters.ContainsKey(character.Key))
            {
                boughtCharacters[character.Key] = character.Value;
            }
        }
        currentlySelectedCharacter = data.currentlySelectedCharacter;

        //Rewards
        timeRewardOpened = data.timeRewardOpened;
    }

    public void LoadCloudData()
    {
        //Try Loading from cloud
        dataHandler.LoadCloudData<GameData>();
    }

    #endregion
}
