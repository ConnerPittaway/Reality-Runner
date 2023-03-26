using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerController player;
    public TMPro.TextMeshProUGUI distanceGame;
    public TMPro.TextMeshProUGUI distanceEnd;
    public TMPro.TextMeshProUGUI realitiesExplored;
    public TMPro.TextMeshProUGUI coinsEarned;
    public GameObject endScreen;
    public GameObject mainUI;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
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
        AudioManager.Instance.RestartMusic();
        SceneManager.LoadScene("RealityRunnerGame");
    }

    public void Pause()
    {
        player.playerAnimator.enabled = !player.playerAnimator.enabled;
        isPaused = !isPaused;
        player.isPaused = !player.isPaused;
    }

    public void MainMenu()
    {
        AudioManager.Instance.ReturnToMainMenu();
        SceneManager.LoadScene("MainMenu");
    }

    public void EventManager_OnDeath()
    {
        EventManager.Death -= EventManager_OnDeath;
        int distance = Mathf.RoundToInt(player.distance);
        distanceEnd.text = "Distance Ran:\n" + distance.ToString() + "M";
        realitiesExplored.text = "Realities Explored:\n" + player.numberOfRealities.ToString();
        coinsEarned.text = "Coins Earned:\n" + player.coinsEarned.ToString();
        endScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
