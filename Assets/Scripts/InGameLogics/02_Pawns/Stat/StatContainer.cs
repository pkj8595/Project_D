using ObservableCollections;
using R3;
using System.Collections.Generic;
using UnityEngine;


public class StatContainer : MonoBehaviour
{
    public FinalStat Stat { get; private set; } = new FinalStat();
    //[SerializeReference] public IPawnStat BaseStat;
    //private List<StatModifier> _statModifiers = new();
    [SerializeField] private List<Stat> _baseStats = new();

    private ObservableList<StatModifier> _statModifiers = new();
    public IList<StatModifier> StatModifiers => _statModifiers;

    public event System.Action<StatContainer> OnStatChanged;

    void Awake()
    {
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

    public void SetBaseStats(IList<Stat> baseStats)
    {
        _baseStats.Clear();
        _baseStats.AddRange(baseStats);
        Stat.Hp = Stat[EStatType.MaxHP];
        OnChangedStat();
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
        Stat.CalculateFinalStats(_baseStats, _statModifiers);
        Stat.Hp = Stat[EStatType.MaxHP] * hpRatio;
        OnStatChanged?.Invoke(this);
    }

    private float ComputeCurHpRatio()
    {
        if (Stat.Hp <= 0)
            return 0;

        return Mathf.Clamp01(Stat.Hp / Stat[EStatType.MaxHP]);
    }

}
