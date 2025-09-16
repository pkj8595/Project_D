using System;
using UnityEngine;

public class PawnStat : IPawnStat
{
    public float[] Stats { get; set; }

    public void AddStat(EStatType type, float value)
    {
        Stats[(int)type] += value;
    }

    internal void Init()
    {
        if (Stats == null || Stats.Length == 0)
        {
            Stats = new float[(int)EStatType.Count];
            Stats[(int)EStatType.Attack] = 10f;
            Stats[(int)EStatType.MaxHP] = 100f;
            Stats[(int)EStatType.MoveSpeed] = 3f;
        }
    }
}
