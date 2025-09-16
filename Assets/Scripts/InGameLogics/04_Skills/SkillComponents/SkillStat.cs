using System.Collections.Generic;
using System;
using System.Linq;


namespace InGameLogics.Skill
{

    [System.Serializable]
    public class SkillStat 
    {
        private float[] _finalStat = new float[(int)ESkillStat.Count];
        private float[] _baseStat = new float[(int)ESkillStat.Count];//원본 스킬 스탯

        private float _pawnAttack;
        private IPawnBase _owner;


        private List<SkillStatModifier> _statModifiers = new();
        public IReadOnlyList<SkillStatModifier> StatModifierList => _statModifiers;
        private Action<StatContainer> OnPawnStatChangedHandler;
        public float this[ESkillStat statType]
        {
            get => _finalStat[(int)statType];
            set => _finalStat[(int)statType] = value;
        }

        public SkillStat(SOSkillStat skillStat, IPawnBase owner)
        {
            _owner = owner;
            OnPawnStatChangedHandler = (statContainer) =>
            {
                _pawnAttack = statContainer.Stat[EStatType.Attack];
                CalculateFinalStats();
            };
            owner.StatContainer.OnStatChanged -= OnPawnStatChangedHandler;
            owner.StatContainer.OnStatChanged += OnPawnStatChangedHandler;

            for (int i = 0; i < (int)ESkillStat.Count; i++)
            {
                _baseStat[i] = skillStat.GetStat((ESkillStat)i);
            }
            CalculateFinalStats();
        }

        public void AddModifier(SkillStatModifier modifier)
        {
            _statModifiers.Add(modifier);
            CalculateFinalStats();
        }

        public void AddRangeModifier(List<SkillStatModifier> modifiers)
        {
            if (modifiers == null || modifiers.Count == 0) return;

            _statModifiers.AddRange(modifiers);
            CalculateFinalStats();
        }

        public void RemoveModifier(SkillStatModifier modifier)
        {
            _statModifiers.Remove(modifier);
            CalculateFinalStats();
        }

        public void ClearModifiers()
        {
            _statModifiers.Clear();
            CalculateFinalStats();
        }

        private void CalculateFinalStats()
        {
            for (int i = 0; i < (int)ESkillStat.Count; i++)
            {
                _finalStat[i] = _baseStat[i];
            }

            Span<float> addStatSpan = stackalloc float[(int)ESkillStat.Count];
            Span<float> multiplySpan = stackalloc float[(int)ESkillStat.Count];
            Span<float> overrideSpan = stackalloc float[(int)ESkillStat.Count];

            foreach (var modifier in _statModifiers)
            {
                int index = (int)modifier.StatType;

                switch (modifier.ModifierType)
                {
                case ESkillStatModifierType.Add:
                    addStatSpan[index] += modifier.Value;
                    break;

                case ESkillStatModifierType.Multiply:
                    multiplySpan[index] += modifier.Value;
                    break;

                case ESkillStatModifierType.Override:
                    overrideSpan[index] = modifier.Value;
                    break;
                }
            }

            for (int i = 0; i < (int)ESkillStat.Count; i++)
            {
                if (overrideSpan[i] != 0)
                {
                    _finalStat[i] = overrideSpan[i];
                }
                else
                {
                    _finalStat[i] += addStatSpan[i];
                    if (multiplySpan[i] != 0)
                    {
                        _finalStat[i] *= multiplySpan[i];
                    }
                }
            }

        }


    }
}