using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeaderScreen : MonoBehaviour
{
    public MainMenuUIManager mainMenu;
    public TMP_Text coins;

    void Start()
    {
        EventManager.CoinPurchase += EventManager_OnCoinPurchase;
    }

    public void openMainMenu()
    {
        //Disable Current Screen and Headers
        mainMenu.activeScreen.SetActive(false);
        gameObject.SetActive(false);

        //Open Main Screen and Headers
        mainMenu.gameObject.SetActive(true);
        mainMenu.mainScreenHeaders.SetActive(true);
    }

    private void OnEnable()
    {
        coins.text = "Coins:\n" + GlobalDataManager.Instance.GetCoins().ToString();
    }

    private void EventManager_OnCoinPurchase()
    {
        coins.text = "Coins:\n" + GlobalDataManager.Instance.GetCoins().ToString();
    }

    private void OnDestroy()
    {
        EventManager.CoinPurchase -= EventManager_OnCoinPurchase;
    }
}
