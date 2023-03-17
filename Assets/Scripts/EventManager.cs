using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //Open UI
    public static event UnityAction UIElementOpened;
    public static void OnUIElementOpened() => UIElementOpened?.Invoke();

    //Purchase Coins
    public static event UnityAction CoinPurchase;
    public static void OnCoinPurchase() => CoinPurchase?.Invoke();
}
