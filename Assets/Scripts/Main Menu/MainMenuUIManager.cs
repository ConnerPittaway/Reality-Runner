using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = "High Score:\n" + GlobalDataManager.Instance.GetHighScore().ToString() + "M";
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
        coins.text = "Currency:\n" + GlobalDataManager.Instance.GetCoins().ToString();
    }
}
