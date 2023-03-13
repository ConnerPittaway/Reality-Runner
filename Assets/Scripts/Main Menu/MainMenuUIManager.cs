using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    //Screens
    public GameObject stats, credits, characterStore, itemUpgrades, store;

    //Active Screen
    public GameObject activeScreen;

    //Headers
    public GameObject mainScreenHeaders, gameScreenHeaders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void openStats()
    {
        stats.SetActive(true);
        gameObject.SetActive(false);
        activeScreen = stats;
    }

    public void openCredits()
    {
        credits.SetActive(true);
        gameObject.SetActive(false);
        activeScreen = credits;
    }

    public void openCharacterStore()
    {
        characterStore.SetActive(true);
        gameObject.SetActive(false);
        activeScreen = characterStore;
    }

    public void openItemUpgrades()
    {
        openScreen(itemUpgrades);
        /*itemUpgrades.SetActive(true);
        gameObject.SetActive(false);
        activeScreen = itemUpgrades;*/
    }

    public void openStore()
    {
        store.SetActive(true);
        gameObject.SetActive(false);
        activeScreen = store;
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
}
