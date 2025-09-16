using UnityEngine;

public class MonsterStat : IPawnStat
{
    public float[] Stats { get ; set; }

    public void AddStat(EStatType type, float value)
    {
        Stats[(int)type] += value;
    }

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

    public void Init()
    {
        Stats = new float[(int)EStatType.Count];
        Stats[(int)EStatType.Attack] = 10f; 
        Stats[(int)EStatType.MaxHP] = 100f; 
        Stats[(int)EStatType.MoveSpeed] = 3f;
    }

}
