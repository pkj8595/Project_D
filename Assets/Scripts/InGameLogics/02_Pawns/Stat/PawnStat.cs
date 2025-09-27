using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어, 적 등 스탯 데이터 클래스
/// </summary>
public class PawnStat : IPawnStat
{
    public float[] Stats { get; set; } = new float[(int)EStatType.Count];

    public PawnStat(List<Stat> baseStats)
    {
        foreach (var stat in baseStats)
        {
            Stats[(int)stat.type] = stat.value;
        }
    }

    public void AddStat(EStatType type, float value)
    {
        Stats[(int)type] += value;
    }
}
