using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public interface IPawnStat
{
    public float[] Stats { get; set; }

    public float this[EStatType type]
    {
        get => Stats[(int)type];
        set => Stats[(int)type] = value;
    }
    public float this[int type]
    {
        get => Stats[type];
        set => Stats[type] = value;
    }
    public void AddStat(EStatType type, float value);
}


/// <summary>
/// 플레이어, 적 등 스탯 데이터 클래스
/// </summary>
[System.Serializable]
public class PawnStat : IPawnStat
{
    [field: SerializeField] public float[] Stats { get; set; } = new float[(int)EStatType.Count];
    public void AddStat(EStatType type, float value)
    {
        Stats[(int)type] += value;
    }
}

[System.Serializable]
public class MonsterStat : IPawnStat
{
    [field: SerializeField] public float[] Stats { get; set; } = new float[(int)EStatType.Count];
    public void AddStat(EStatType type, float value)
    {
        Stats[(int)type] += value;
    }
}