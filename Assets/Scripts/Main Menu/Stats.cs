using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public TMP_Text statsText;

    void OnEnable()
    {
        statsText.text = "Total Runs: " + GlobalStatsData.Instance.totalRuns.ToString() + 
                       "\nTotal Shields Collected: " + GlobalStatsData.Instance.totalShieldsCollected.ToString() +
                       "\n Total Obstacles Hit: " + GlobalStatsData.Instance.totalObstaclesHit.ToString() +
                       "\n Total Realities Explored: " + GlobalStatsData.Instance.totalRealitiesExplored.ToString() +
                       "\n Total Distance: " + GlobalStatsData.Instance.totalDistance.ToString() +
                       "\n Highest Coins Earned: " + GlobalStatsData.Instance.highestCoinsEarned.ToString() +
                       "\n Total Coins Earned: " + GlobalStatsData.Instance.totalCoinsEarned.ToString();
        EventManager.OnUIElementOpened();
    }
}
