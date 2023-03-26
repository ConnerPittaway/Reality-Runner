using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStatsData : MonoBehaviour
{
    //Instance
    public static GlobalStatsData Instance;

    //Data Handler
    private JsonDataHandler statsDataHandler;

    //Data
    public int totalRuns = 0;
    public int totalShieldsCollected = 0;
    public int totalObstaclesHit = 0;
    public int totalRealitiesExplored = 0;
    public int totalDistance = 0;
    public int highestCoinsEarned = 0;
    public int totalCoinsEarned = 0;

    private void Awake()
    {
        //Create Singleton
        if (Instance == null)
        {
            Instance = this;

            //Load Stats Data
            this.statsDataHandler = new JsonDataHandler(Application.persistentDataPath, "StatsData");
            StatsData statsData = statsDataHandler.LoadData<StatsData>();

            if (statsData == null)
            {
                statsData = new StatsData();
            }
            totalRuns = statsData.totalRuns;
            totalShieldsCollected = statsData.totalShieldsCollected;
            totalObstaclesHit = statsData.totalObstaclesHit;
            totalRealitiesExplored = statsData.totalRealitiesExplored;
            totalDistance = statsData.totalDistance;
            highestCoinsEarned = statsData.highestCoinsEarned;
            totalCoinsEarned = statsData.totalCoinsEarned;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        statsDataHandler.SaveData<StatsData>();
    }
}
