using System;
using UnityEngine;

public static class GameEvents
{
    public static Action<int> OnGoldChanged;
    public static Action<int> OnPlayerHPChanged;
    public static Action<int> OnWaveChanged;
    public static Action OnWaveStart;
    public static Action OnPlayerDeath;

}
