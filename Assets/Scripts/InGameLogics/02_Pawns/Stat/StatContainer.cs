using ObservableCollections;
using R3;
using System.Collections.Generic;
using UnityEngine;


public class StatContainer : MonoBehaviour
{
    public FinalStat Stat { get; private set; } = new FinalStat();
    public IPawnStat BaseStat { get; private set; }
    //private List<StatModifier> _statModifiers = new();
    private ObservableList<StatModifier> _statModifiers = new();
    public IList<StatModifier> StatModifiers => _statModifiers;

    public event System.Action<StatContainer> OnStatChanged;

    void Awake()
    {
        BaseStat = GetComponent<IPawnStat>();
        _statModifiers
            .ObserveAdd()
            .Subscribe(x => OnChangedStat())
            .AddTo(this);

        _statModifiers
            .ObserveRemove()
            .Subscribe(x => OnChangedStat())
            .AddTo(this);

        _statModifiers
            .ObserveReset()
            .Subscribe(x => OnChangedStat())
            .AddTo(this);

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
