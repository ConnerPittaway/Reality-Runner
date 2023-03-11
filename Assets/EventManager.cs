using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction UIElementOpened;
    public static void OnUIElementOpened() => UIElementOpened?.Invoke();
}
