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
                       "\nTotal Obstacles Hit: " + GlobalStatsData.Instance.totalObstaclesHit.ToString() +
                       "\nTotal Realities Explored: " + GlobalStatsData.Instance.totalRealitiesExplored.ToString() +
                       "\nTotal Distance: " + GlobalStatsData.Instance.totalDistance.ToString() + "m" +
                       "\nHighest Coins Earned: " + GlobalStatsData.Instance.highestCoinsEarned.ToString() +
                       "\nTotal Coins Earned: " + GlobalStatsData.Instance.totalCoinsEarned.ToString();
        EventManager.OnUIElementOpened();
    }
}
