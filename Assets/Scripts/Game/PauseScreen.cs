using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public PlayerController player;
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject options;

    public void Resume()
    {
        player.playerAnimator.enabled = !player.playerAnimator.enabled;
        player.isPaused = !player.isPaused;
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
        LeanTween.resumeAll();
    }

    public void OpenSettings()
    {
        options.SetActive(false);
        settings.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
        options.SetActive(true);
    }
}
