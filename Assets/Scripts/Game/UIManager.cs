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
    public GameObject endScreen;
    public GameObject mainUI;
    public bool isPaused;

    private void Awake()
    {
        endScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.distance);
        distanceGame.text = distance.ToString() + "m";

        if (player.isDead)
        {
            distanceEnd.text = "Distance Ran:\n" + distance.ToString() + "M";
            endScreen.SetActive(true);
        }
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
        player.isPaused = isPaused;
    }

    public void MainMenu()
    {
        //AudioManager.Instance.StopSongs();
        AudioManager.Instance.ReturnToMainMenu();
        SceneManager.LoadScene("MainMenu");
    }

}
