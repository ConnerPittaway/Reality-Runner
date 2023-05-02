using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUpgrades : MonoBehaviour
{
    public SerializableDictionary<GlobalDataManager.ShieldLevel, int> shieldLevelUpgradeCosts;
    public TMP_Text shieldLevelText, shieldEffectText;
    private float shieldLevelEffect;
    public Button shieldUpgradeButton;
    public void UpgradeShield()
    {
        if(GlobalDataManager.Instance.GetCoins() >= shieldLevelUpgradeCosts[GlobalDataManager.Instance.currentShieldLevel])
        {
            GlobalDataManager.Instance.AlterCoins(-shieldLevelUpgradeCosts[GlobalDataManager.Instance.currentShieldLevel]);
            GlobalDataManager.Instance.currentShieldLevel++;
            EventManager.OnCoinPurchase();
            UpdateShield();
            GlobalDataManager.Instance.SaveData();
        }
    }    

    void OnEnable()
    {
        UpdateShield();
        EventManager.OnUIElementOpened();
    }

    public void UpdateShield()
    {
        //Update Level Text/Cost
        if (GlobalDataManager.Instance.currentShieldLevel == GlobalDataManager.ShieldLevel.LEVEL5)
        {
            shieldUpgradeButton.enabled = false;
            shieldLevelEffect = 3.5f;
            shieldLevelText.text = "Current Level 5:\nMax Level!";
        }
        else
        {
            switch (GlobalDataManager.Instance.currentShieldLevel)
            {
                case GlobalDataManager.ShieldLevel.LEVEL1:
                    shieldLevelEffect = 1.5f;
                    break;
                case GlobalDataManager.ShieldLevel.LEVEL2:
                    shieldLevelEffect = 2f;
                    break;
                case GlobalDataManager.ShieldLevel.LEVEL3:
                    shieldLevelEffect = 2.5f;
                    break;
                case GlobalDataManager.ShieldLevel.LEVEL4:
                    shieldLevelEffect = 3f;
                    break;
            }
            shieldLevelText.text = string.Format("Current Level {0}:\nCost to Upgrade:\n{1}C", (int)GlobalDataManager.Instance.currentShieldLevel + 1, shieldLevelUpgradeCosts[GlobalDataManager.Instance.currentShieldLevel]);
        }
        shieldEffectText.text = string.Format("Current Effect:\nGain a shield that lasts for {0}s that protects you from slowing down!", shieldLevelEffect);
    }
}
