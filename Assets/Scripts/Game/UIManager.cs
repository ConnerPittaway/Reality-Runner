using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Include Facebook namespace
using Facebook.Unity;

public class UIManager : MonoBehaviour
{
    public PlayerController player;
    public TMPro.TextMeshProUGUI distanceGame;
    public TMPro.TextMeshProUGUI distanceEnd;
    public TMPro.TextMeshProUGUI realitiesExplored;
    public TMPro.TextMeshProUGUI premiumBonus;
    public TMPro.TextMeshProUGUI coinsEarned;
    public GameObject endScreen;
    public GameObject mainUI;
    public GameObject pauseScreen;

    //Ads 
    public GameAds gameAdsManager;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Death += EventManager_OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.distance);
        distanceGame.text = distance.ToString() + "m";
    }

    public void Restart()
    {
        AudioManager.Instance.StopSongs();
        //Show Ads
        if(!GlobalDataManager.Instance.GetPremiumStatus())
        {
            gameAdsManager.ShowAd(false);
        }
        else
        {
            OnRestartCompleted();
        }
    }

    public static void OnRestartCompleted()
    {
        AudioManager.Instance.RestartMusic();
        SceneManager.LoadScene("RealityRunnerGame");
    }

    public void Pause()
    {
        player.playerAnimator.enabled = !player.playerAnimator.enabled;
        player.isPaused = !player.isPaused;
        pauseScreen.SetActive(true);
        gameObject.SetActive(false);
        LeanTween.pauseAll();
    }

    public void ShareToFacebook()
    {
        if (!FacebookManager.Instance.ShareRun())
        {
            FacebookManager.Instance.ActivateFacebook();

            if(FB.IsLoggedIn)
            {
                FacebookManager.Instance.ShareRun();
            }
        }
    }

    public void MainMenu()
    {
        AudioManager.Instance.StopSongs();
        if (!GlobalDataManager.Instance.GetPremiumStatus())
        {
            gameAdsManager.ShowAd(true);
        }
        else
        {
            OnMainMenuCompleted();
        }
    }

    public static void OnMainMenuCompleted()
    {
        AudioManager.Instance.ReturnToMainMenu();
        SceneManager.LoadScene("MainMenu");
    }

    public void EventManager_OnDeath()
    {
        int distance = Mathf.RoundToInt(player.distance);
        distanceEnd.text = "Distance Ran:\n" + distance.ToString() + "M";
        realitiesExplored.text = "Realities Explored:\n" + player.numberOfRealities.ToString();

        if(GlobalDataManager.Instance.GetPremiumStatus())
        {
            premiumBonus.text = "Premium Bonus (+50%):\n" + (((distance/100) + player.numberOfRealities) / 2).ToString();
        }

        Debug.Log("Total Coins UI " + player.coinsEarned);

        coinsEarned.text = "Total Coins Earned:\n" + player.coinsEarned.ToString();
        endScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.Death -= EventManager_OnDeath;
    }
}
