using System;
using UnityEngine;

[RequireComponent(typeof(GridObject))]
internal class UnitStats : MonoBehaviour
{
    public float[] stats = new float[(int)StatType.Count];    

    public float this[StatType t]
    {
        get => stats[(int)t];
        set => stats[(int)t] = value;
    }
}
