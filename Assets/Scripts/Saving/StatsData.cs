using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    //Time of Save (Use to Compare If Data in Cloud is More Recent)
    public ulong timeOfLastSave = 0;

    //Data
    public int totalRuns = 0;
    public int totalShieldsCollected = 0;
    public int totalObstaclesHit = 0;
    public int totalRealitiesExplored = 0;
    public int totalDistance = 0;
    public int highestCoinsEarned = 0;
    public int totalCoinsEarned = 0;

    //Retrieve Data
    public StatsData()
    {
        timeOfLastSave = GlobalStatsData.Instance.timeOfLastSave;
        totalRuns = GlobalStatsData.Instance.totalRuns;
        totalShieldsCollected = GlobalStatsData.Instance.totalShieldsCollected;
        totalObstaclesHit = GlobalStatsData.Instance.totalObstaclesHit;
        totalRealitiesExplored = GlobalStatsData.Instance.totalRealitiesExplored;
        totalDistance = GlobalStatsData.Instance.totalDistance;
        highestCoinsEarned = GlobalStatsData.Instance.highestCoinsEarned;
        totalCoinsEarned = GlobalStatsData.Instance.totalCoinsEarned;
    }
}
