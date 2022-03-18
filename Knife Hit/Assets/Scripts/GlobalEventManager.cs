using System;

public static class GlobalEventManager
{
    public static Action OnKnifeAttached;
    public static Action OnAppleHit;

    public static void KnifeAttached() 
    {
        OnKnifeAttached?.Invoke();
    }

    public static void AppleHit() 
    {
        OnAppleHit?.Invoke();
    }
}
