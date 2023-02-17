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
    public AudioManager audioManager;
    public bool isPaused;

    private void Awake()
    {
        endScreen.SetActive(false);
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
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
            distanceEnd.text = distance.ToString() + "m";
            endScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        audioManager.RestartMusic();
        SceneManager.LoadScene("RealityRunnerGame");
    }

    public void Pause()
    {
        player.playerAnimator.enabled = !player.playerAnimator.enabled;
        isPaused = !isPaused;
        player.isPaused = isPaused;
    }
}
