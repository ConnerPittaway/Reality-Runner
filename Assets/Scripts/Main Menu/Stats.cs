using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public TMP_Text statsText, highScoreText;
    public GameObject rightArrow, leftArrow;

    public void RightArrow()
    {
        highScoreText.text = "<u>High Scores</u>\n";
        //Update Scores
        Debug.Log(FirebaseManager.Instance.scoreLeaderboard.Count);
        foreach (User user in FirebaseManager.Instance.scoreLeaderboard)
        {
            highScoreText.text += user.name + " : " + user.score + "\n";
        }
        //Display High Scores
        leftArrow.SetActive(true);
        rightArrow.SetActive(false);
        statsText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(true);
    }

    public void LeftArrow()
    {
        rightArrow.SetActive(true);
        leftArrow.SetActive(false);
        statsText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        //Update Stats
        statsText.text = "<u>Game Stats</u>" + 
                        "\nTotal Runs: " + GlobalStatsData.Instance.totalRuns.ToString() + 
                       "\nTotal Shields Collected: " + GlobalStatsData.Instance.totalShieldsCollected.ToString() +
                       "\nTotal Obstacles Hit: " + GlobalStatsData.Instance.totalObstaclesHit.ToString() +
                       "\nTotal Realities Explored: " + GlobalStatsData.Instance.totalRealitiesExplored.ToString() +
                       "\nTotal Distance: " + GlobalStatsData.Instance.totalDistance.ToString() + "m" +
                       "\nHighest Coins Earned: " + GlobalStatsData.Instance.highestCoinsEarned.ToString() +
                       "\nTotal Coins Earned: " + GlobalStatsData.Instance.totalCoinsEarned.ToString();

        //Update Leaderboard
        FirebaseManager.Instance.GetHighScores();

        EventManager.OnUIElementOpened();
    }

    void OnDisable()
    {
        rightArrow.SetActive(true);
        leftArrow.SetActive(false);
        statsText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(false);
    }
}
