using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrades : MonoBehaviour
{
    public SerializableDictionary<string, int> shieldLevelUpgradeCost;
    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}
