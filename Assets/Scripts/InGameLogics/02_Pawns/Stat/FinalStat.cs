using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FinalStat
{
    private readonly float[] FinalStats = new float[(int)EStatType.Count];
    public float Hp { get; set; }

    public float this[int index]
    {
        get => FinalStats[index];
        set => FinalStats[index] = value;
    }
    public float this[EStatType type]
    {
        get => FinalStats[(int)type];
        set => FinalStats[(int)type] = value;
    }

    public void CalculateFinalStats(IList<Stat> baseStats, IList<StatModifier> statModifiers)
    {
        for (int i = 0; i < FinalStats.Length; i++)
            FinalStats[i] = 0;

        foreach (var baseStat in baseStats)
            FinalStats[(int)baseStat.type] += baseStat.value;

        foreach (var modifier in statModifiers)
        {
            switch (modifier.ModType)
            {
                case ModifierType.Add:
                    FinalStats[(int)modifier.Type] += modifier.Value;
                    break;
                case ModifierType.Multiply:
                    FinalStats[(int)modifier.Type] *= modifier.Value;
                    break;
                case ModifierType.Override:
                    FinalStats[(int)modifier.Type] = modifier.Value;
                    break;
            }
        }
    }
}
