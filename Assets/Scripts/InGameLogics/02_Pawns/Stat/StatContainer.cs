using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

public class StatContainer 
{
    public FinalStat Stat { get; private set; } = new FinalStat();
    public IPawnStat BaseStat { get; private set; }
    private List<StatModifier> _statModifiers = new ();
    public IList<StatModifier> StatModifiers => _statModifiers;

    public event System.Action<StatContainer> OnStatChanged;

    public StatContainer(IPawnStat unitStat, MonoBehaviour owner)
    {
        BaseStat = unitStat;
        _statModifiers.Clear();
        Stat.CalculateFinalStats(BaseStat, _statModifiers);
        Stat.Hp = Stat[EStatType.MaxHP];
    }

    public void AddModifier(StatModifier statModifier)
    {
        if (statModifier == null)
            return;

        _statModifiers.Add(statModifier);
        OnChangedStat();
    }

    public void AddRangeModifier(List<StatModifier> statModifiers)
    {
        if (statModifiers == null || statModifiers.Count == 0)
            return;

        _statModifiers.AddRange(statModifiers);
        OnChangedStat();
    }

    public void RemoveModifier(StatModifier statModifier)
    {
        if (statModifier == null || !_statModifiers.Contains(statModifier))
            return;

        _statModifiers.Remove(statModifier);
        OnChangedStat();
    }


    private void OnChangedStat()
    {
        float hpRatio = ComputeCurHpRatio();
        Stat.CalculateFinalStats(BaseStat, _statModifiers);
        Stat.Hp = Stat[EStatType.MaxHP] * hpRatio;
        OnStatChanged?.Invoke(this);
    }

    private float ComputeCurHpRatio()
    {
        if (Stat.Hp <= 0)
            return 0;

        float ratio = Stat.Hp / Stat[EStatType.MaxHP];
        if (ratio < 0)
            ratio = 0;
        else if (ratio > 1)
            ratio = 1;

        return ratio;
    }
        
}
