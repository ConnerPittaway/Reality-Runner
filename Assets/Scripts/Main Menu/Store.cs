using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using TMPro;
using System.Linq;

public class Store : MonoBehaviour
{
    //Reward Info
    public Button freeReward;
    public float timeToWaitMS = 3000;
    public TMP_Text rewardText;

    //Store Pop-Up
    public TMP_Text itemToPurchase;
    public GameObject popUp;

    //Premium and Character Buttons
    public Button premiumButton;
    public Button characterButton;
    public TMP_Text premiumText;
    public TMP_Text characterText;

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

        EventManager.PremiumPurchase += UpdatePremiumButton;
        EventManager.AllCharactersPurchase += UpdateCharacterButton;
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

                //Extract Seconds
                timeLeft += (secondsLeft % 60).ToString("00") + "s";

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
        GlobalDataManager.Instance.AlterCoins(+100);
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
        if (IAPs.Instance.m_StoreController != null)
        {
            IAPs.Instance.Coins5000();
        }
        else
        {
            activePurchase = Purchases.COINS5000;
            itemToPurchase.text = "Coins 5000 - $2.99";
            popUp.SetActive(true);
        }
    }

    public void On10000CoinPurchase()
    {
        if (IAPs.Instance.m_StoreController != null)
        {
            IAPs.Instance.Coins10000();
        }
        else
        {
            //Internal Pop-Up (Pre-Google-Play Integration)
            activePurchase = Purchases.COINS10000;
            itemToPurchase.text = "Coins 10000 - $4.99";
            popUp.SetActive(true);
        }
    }

    public void On40000CoinPurchase()
    {
        if (IAPs.Instance.m_StoreController != null)
        {
            IAPs.Instance.Coins40000();
        }
        else
        {
            //Internal Pop-Up (Pre-Google-Play Integration)
            activePurchase = Purchases.COINS40000;
            itemToPurchase.text = "Coins 40000 - $9.99";
            popUp.SetActive(true);
        }
    }

    public void OnAllCharactersPurchase()
    {
        if (IAPs.Instance.m_StoreController != null)
        {
            IAPs.Instance.AllCharacters();
        }
        else
        {
            //Internal Pop-Up (Pre-Google-Play Integration)
            activePurchase = Purchases.ALLCHARACTERS;
            itemToPurchase.text = "All Characters - $9.99";
            popUp.SetActive(true);
        }
    }

    public void OnPremiumPurchase()
    {
        if (IAPs.Instance.m_StoreController != null)
        {
            IAPs.Instance.Premium();
        }
        else
        {
            //Internal Pop-Up (Pre-Google-Play Integration)
            activePurchase = Purchases.PREMIUM;
            itemToPurchase.text = "Premium - $4.99";
            popUp.SetActive(true);
        }
    }

    public void ConfirmPurchase()
    {
        switch(activePurchase)
        {
            case Purchases.COINS1000:
                GlobalDataManager.Instance.AlterCoins(1000);
                EventManager.OnCoinPurchase();
                break;
            case Purchases.COINS5000:
                GlobalDataManager.Instance.AlterCoins(5000);
                EventManager.OnCoinPurchase();
                break;
            case Purchases.COINS10000:
                GlobalDataManager.Instance.AlterCoins(10000);
                EventManager.OnCoinPurchase();
                break;
            case Purchases.COINS40000:
                GlobalDataManager.Instance.AlterCoins(40000);
                EventManager.OnCoinPurchase();
                break;
            case Purchases.ALLCHARACTERS:
                GlobalDataManager.Instance.UnlockAllCharacters();
                UpdateCharacterButton();
                break;
            case Purchases.PREMIUM:
                GlobalDataManager.Instance.SetPremiumStatus(true);
                UpdatePremiumButton();
                break;
        }
        popUp.SetActive(false);
    }

    public void DenyPurchase()
    {
        popUp.SetActive(false);
    }

    void OnEnable()
    {
        //Check For Premium and All Characters
        UpdatePremiumButton();
        UpdateCharacterButton();
        EventManager.OnUIElementOpened();
    }

    void UpdatePremiumButton()
    {
        if(GlobalDataManager.Instance.GetPremiumStatus())
        {
            premiumText.text = "Premium Owned";
            premiumButton.enabled = false;
        }
    }

    void UpdateCharacterButton()
    {
        if (!GlobalDataManager.Instance.boughtCharacters.Values.Contains(false))
        {
            characterText.text = "All Characters Owned";
            characterButton.enabled = false;
        }
    }

    void OnDisable()
    {
        popUp.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.PremiumPurchase -= UpdatePremiumButton;
        EventManager.AllCharactersPurchase -= UpdateCharacterButton;
    }
}
