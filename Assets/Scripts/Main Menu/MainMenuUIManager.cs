using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIManager : MonoBehaviour
{
    //Screens
    public GameObject stats, devCredits, characterStore, itemUpgrades, store, musicCredits, settings;

    //Active Screen
    public GameObject activeScreen;

    //Headers
    public GameObject mainScreenHeaders, gameScreenHeaders;

    //Text Fields
    public TMP_Text coins;
    public TMP_Text highScore;
    public TMP_Text characterText;

    public void startGame()
    {
        AudioManager.Instance.StartGameAudio();
        SceneManager.LoadScene("RealityRunnerGame");
    }

    public void openStats()
    {
        openScreen(stats);
    }

    public void openDevCredits()
    {
        openScreen(devCredits);
    }

    public void openMusicCredits()
    {
        openScreen(musicCredits);
    }

    public void openCharacterStore()
    {
        openScreen(characterStore);
    }

    public void openItemUpgrades()
    {
        openScreen(itemUpgrades);
    }

    public void openStore()
    {
        openScreen(store);
    }

    public void openSettings()
    {
        openScreen(settings);
    }

    public void openScreen(GameObject screen)
    {
        screen.SetActive(true);
        gameObject.SetActive(false);
        mainScreenHeaders.SetActive(false);
        gameScreenHeaders.SetActive(true);
        activeScreen = screen;
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }

    private void Update()
    {
        Debug.Log("Updating UI");

        coins.text = "Coins:\n" + GlobalDataManager.Instance.GetCoins().ToString();
        highScore.text = "High Score:\n" + GlobalDataManager.Instance.GetHighScore().ToString() + "M";

        //Character Name
        string name = "";
        switch (GlobalDataManager.Instance.currentlySelectedCharacter)
        {
            case GlobalDataManager.Characters.SHROUD:
                name = "Shroud";
                break;
            case GlobalDataManager.Characters.BOXY:
                name = "Boxy";
                break;
            case GlobalDataManager.Characters.SHROUD3:
                name = "Shroud 3";
                break;
        }
        characterText.text = name;
    }
}
