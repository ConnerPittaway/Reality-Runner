using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    //Open UI
    public static event UnityAction UIElementOpened;
    public static void OnUIElementOpened() => UIElementOpened?.Invoke();

    //Purchases
    public static event UnityAction CoinPurchase;
    public static void OnCoinPurchase() => CoinPurchase?.Invoke();

    public static event UnityAction PremiumPurchase;
    public static void OnPremiumPurchase() => PremiumPurchase?.Invoke();

    public static event UnityAction AllCharactersPurchase;
    public static void OnAllCharactersPurchase() => AllCharactersPurchase?.Invoke();

    //In-Game
    public static event UnityAction Death;
    public static void OnDeath() => Death?.Invoke();

    public static event UnityAction PortalOpened;
    public static void OnPortalOpened() => PortalOpened?.Invoke();

    public static event UnityAction WorldChanged;
    public static void OnWorldChanged() => WorldChanged?.Invoke();

    public static event UnityAction GamePaused;
    public static void OnGamePaused() => GamePaused.Invoke();

    public static event UnityAction GameResumed;
    public static void OnGameResumed() => GameResumed.Invoke();

    //Settings
    public static event UnityAction<float> MusicAudioChanged;
    public static void OnMusicAudioChanged(float audioValue) => MusicAudioChanged?.Invoke(audioValue);

    public static event UnityAction<float> SFXAudioChanged;
    public static void OnSFXAudioChanged(float audioValue) => SFXAudioChanged?.Invoke(audioValue);

    public static event UnityAction<bool> FrameShowChanged;
    public static void OnFrameShowChanged(bool frameShow) => FrameShowChanged?.Invoke(frameShow);
}
