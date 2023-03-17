using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
