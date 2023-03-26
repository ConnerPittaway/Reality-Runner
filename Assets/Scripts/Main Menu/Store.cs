using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    //Reward Info
    public Button freeReward;
    public float timeToWaitMS = 3000;
    public TMP_Text rewardText;


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
        GlobalDataManager.Instance.AlterCoins(1000);
        EventManager.OnCoinPurchase();
    }

    public void On5000CoinPurchase()
    {
        GlobalDataManager.Instance.AlterCoins(5000);
        EventManager.OnCoinPurchase();
    }

    public void On10000CoinPurchase()
    {
        GlobalDataManager.Instance.AlterCoins(10000);
        EventManager.OnCoinPurchase();
    }

    public void On40000CoinPurchase()
    {
        GlobalDataManager.Instance.AlterCoins(40000);
        EventManager.OnCoinPurchase();
    }

    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}
