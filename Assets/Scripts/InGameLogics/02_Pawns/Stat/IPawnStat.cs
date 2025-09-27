using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
