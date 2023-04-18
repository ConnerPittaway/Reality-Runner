using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using TMPro;

public class Store : MonoBehaviour
{
    //Reward Info
    public Button freeReward;
    public float timeToWaitMS = 3000;
    public TMP_Text rewardText;

    //Store Pop-Up
    public TMP_Text itemToPurchase;
    public GameObject popUp;
    
    //Purchaseables
    public enum Purchases
    {
        COINS1000,
        COINS5000,
        COINS10000,
        COINS40000,
        ALLCHARACTERS,
        PREMIUM
    }
    Purchases activePurchase;

    // Start is called before the first frame update
    void Start()
    {
        //lastRewardOpened = ulong.Parse(PlayerPrefs.GetString("LastRewardObtained"));
        if (!CheckRewardAvaliable())
        {
            freeReward.interactable = false;
        }
    }

    private void Update()
    {
        if(!freeReward.interactable)
        {
            if(CheckRewardAvaliable())
            {
                freeReward.interactable = true;
                return;
            }
            else
            {
                //Time To Reward
                ulong timeDifference = (ulong)DateTime.Now.Ticks - GlobalDataManager.Instance.timeRewardOpened;
                ulong m = timeDifference / TimeSpan.TicksPerMillisecond;
                float secondsLeft = (float)(timeToWaitMS - m) / 1000;

                //Timer Text
                string timeLeft = "";

                //Extract Hours
                timeLeft += ((int)secondsLeft / 3600).ToString() + "h ";
                secondsLeft -= ((int)secondsLeft / 3600) * 3600;

                //Extract Minutes
                timeLeft += ((int)secondsLeft / 60).ToString("00") + "m ";

                //Seconds
                timeLeft += (secondsLeft % 60).ToString("00") + "s ";

                rewardText.text = timeLeft;
            }

        }
    }

    private bool CheckRewardAvaliable()
    {
        //Time To Reward
        ulong timeDifference = (ulong)DateTime.Now.Ticks - GlobalDataManager.Instance.timeRewardOpened;
        ulong m = timeDifference / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(timeToWaitMS - m) / 1000;


        if (secondsLeft < 0)
        {
            rewardText.text = "Claim";
            freeReward.interactable = true;
            return true;
        }
        return false;
    }

    public void FreeReward()
    {
        GlobalDataManager.Instance.timeRewardOpened = (ulong)DateTime.Now.Ticks;
        freeReward.interactable = false;
        GlobalDataManager.Instance.SaveData();
    }

    public void On1000CoinPurchase()
    {
        if (IAPs.Instance.m_StoreController != null)
        {
            IAPs.Instance.Coins1000();
        }
        else
        {
            //Internal Pop-Up (Pre-Google-Play Integration)
            activePurchase = Purchases.COINS1000;
            itemToPurchase.text = "Coins 1000 - $0.99";
            popUp.SetActive(true);
        }
    }

    public void On5000CoinPurchase()
    {
        Debug.Log("Here");
        //Internal Pop-Up (Pre-Google-Play Integration)
        activePurchase = Purchases.COINS5000;
        itemToPurchase.text = "Coins 5000 - $2.99";
        popUp.SetActive(true);
    }

    public void On10000CoinPurchase()
    {
        //Internal Pop-Up (Pre-Google-Play Integration)
        activePurchase = Purchases.COINS10000;
        itemToPurchase.text = "Coins 10000 - $4.99";
        popUp.SetActive(true);
    }

    public void On40000CoinPurchase()
    {
        //Internal Pop-Up (Pre-Google-Play Integration)
        activePurchase = Purchases.COINS40000;
        itemToPurchase.text = "Coins 40000 - $9.99";
        popUp.SetActive(true);
    }

    public void ConfirmPurchase()
    {
        switch(activePurchase)
        {
            case Purchases.COINS1000:
                GlobalDataManager.Instance.AlterCoins(1000);
                EventManager.OnCoinPurchase();
                popUp.SetActive(false);
                break;
            case Purchases.COINS5000:
                GlobalDataManager.Instance.AlterCoins(5000);
                EventManager.OnCoinPurchase();
                popUp.SetActive(false);
                break;
            case Purchases.COINS10000:
                GlobalDataManager.Instance.AlterCoins(10000);
                EventManager.OnCoinPurchase();
                popUp.SetActive(false);
                break;
            case Purchases.COINS40000:
                GlobalDataManager.Instance.AlterCoins(40000);
                EventManager.OnCoinPurchase();
                popUp.SetActive(false);
                break;
            case Purchases.ALLCHARACTERS:
                break;
            case Purchases.PREMIUM:
                break;
        }    
    }

    public void DenyPurchase()
    {
        popUp.SetActive(false);
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}
