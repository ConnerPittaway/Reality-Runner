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


    //In-Game
    public static event UnityAction Death;
    public static void OnDeath() => Death?.Invoke();

    public static event UnityAction PortalOpened;
    public static void OnPortalOpened() => PortalOpened?.Invoke();


    //Settings
    public static event UnityAction<float> AudioChanged;
    public static void OnAudioChanged(float audioValue) => AudioChanged?.Invoke(audioValue);
}
