using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateLeaderboardName : MonoBehaviour
{
    public TMP_InputField enteredUsername;
    public TMP_Text currentUsername;

    private void Awake()
    {
        currentUsername.text = "Current Username: " + GlobalStatsData.Instance.usernameLeaderboard;
    }

    public void UpdateUsername()
    {
        GlobalStatsData.Instance.usernameLeaderboard = enteredUsername.text;
        currentUsername.text = "Current Username: " + GlobalStatsData.Instance.usernameLeaderboard;
        GlobalStatsData.Instance.SaveData();
        FirebaseManager.Instance.UpdateUserName();
    }
}
