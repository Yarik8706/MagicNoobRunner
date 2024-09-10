using UnityEngine.Events;

public static class GameEvents
{
    public static readonly UnityEvent ResetLevelEvent = new ();
    public static readonly UnityEvent DefeatEvent = new ();
    public static readonly UnityEvent WinEvent = new ();
    public static readonly UnityEvent UpdateMoneyCount = new ();
    public static readonly UnityEvent StartGameEvent = new ();
}