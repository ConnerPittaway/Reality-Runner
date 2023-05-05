using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStatsData : MonoBehaviour
{
    //Instance
    public static GlobalStatsData Instance;

    //Data Handler
    private JsonDataHandler statsDataHandler;

    //Time Of Save
    public ulong timeOfLastSave = 0;

    //Data
    public int totalRuns = 0;
    public int totalShieldsCollected = 0;
    public int totalObstaclesHit = 0;
    public int totalRealitiesExplored = 0;
    public int totalDistance = 0;
    public int highestCoinsEarned = 0;
    public int totalCoinsEarned = 0;

    //Username Leaderboard
    public string usernameLeaderboard = "Anonymous";

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
        //Load Stats Data
        this.statsDataHandler = new JsonDataHandler(Application.persistentDataPath, "StatsData");
        StatsData statsData = statsDataHandler.LoadData<StatsData>();

        if (statsData == null)
        {
            Debug.Log("Default Stats Data");

            //No Data Found Ask For Cloud
            if (GlobalDataManager.Instance.checkCloud)
            {
                while (!FirebaseManager.Instance.fireBaseReady)
                {
                    yield return new WaitForEndOfFrame();
                }

                Debug.Log("Loading Stats Cloud");
                statsData = statsDataHandler.LoadCloudData<StatsData>();
            }
        }
        else
        {
            Debug.Log("Local Stats Data");
        }

        //No Data Found
        if (statsData == null)
        {
            statsData = new StatsData();
        }
        
        //Time of Save
        timeOfLastSave = statsData.timeOfLastSave;

        totalRuns = statsData.totalRuns;
        totalShieldsCollected = statsData.totalShieldsCollected;
        totalObstaclesHit = statsData.totalObstaclesHit;
        totalRealitiesExplored = statsData.totalRealitiesExplored;
        totalDistance = statsData.totalDistance;
        highestCoinsEarned = statsData.highestCoinsEarned;
        totalCoinsEarned = statsData.totalCoinsEarned;

        //Username
        usernameLeaderboard = statsData.usernameLeaderboard;

        yield return null;
    }

    public void UpdateData()
    {
        StatsData statsData = statsDataHandler.LoadData<StatsData>();

        //Time of Save
        timeOfLastSave = statsData.timeOfLastSave;

        totalRuns = statsData.totalRuns;
        totalShieldsCollected = statsData.totalShieldsCollected;
        totalObstaclesHit = statsData.totalObstaclesHit;
        totalRealitiesExplored = statsData.totalRealitiesExplored;
        totalDistance = statsData.totalDistance;
        highestCoinsEarned = statsData.highestCoinsEarned;
        totalCoinsEarned = statsData.totalCoinsEarned;

        //Username
        usernameLeaderboard = statsData.usernameLeaderboard;
    }

    public void LoadCloudData()
    {
        //Try Loading from cloud
        statsDataHandler.LoadCloudData<StatsData>();
    }

    public void SaveData()
    {
        //Update Time of Save
        timeOfLastSave = (ulong)DateTime.Now.Ticks;
        statsDataHandler.SaveData<StatsData>();
    }
}
