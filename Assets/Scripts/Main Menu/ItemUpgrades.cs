using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrades : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.OnUIElementOpened();
    }
}
