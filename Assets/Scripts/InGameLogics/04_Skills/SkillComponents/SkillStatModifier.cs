using NUnit.Framework;
using System;
using UnityEngine;


namespace InGameLogics.Skill
{
    public enum ESkillStat
    {
        damage, // 데미지
        coolTime, // 쿨타임
        range, // 사거리

        penetration, // 관통
        repeatCount, //반복 횟수
        projectileCount, // 투사체 개수
        projectileSpeed, // 투사체 속도

        expireTime, // 지속시간
        explosionRadius, // 폭발 반경
        explosionDamage, // 폭발 데미지
        explosionDamagePer, // 폭발 데미지 비율
        Count // 스탯 개수
    }

    public enum ESkillStatModifierType
    {
        Add,
        Multiply,
        Override
    }

    [System.Serializable]
    public class SkillStatModifier
    {
        [SerializeField] private ESkillStat _statType;
        [SerializeField] private float _value;
        [SerializeField] private ESkillStatModifierType _modifierType;

        public ESkillStat StatType => _statType;
        public float Value => _value;
        public ESkillStatModifierType ModifierType => _modifierType;

        public SkillStatModifier(ESkillStat statType, float value, ESkillStatModifierType modifierType)
        {
            this._statType = statType;
            this._value = value;
            this._modifierType = modifierType;
        }

        public override string ToString()
        {
            return $"{_statType} {_modifierType} {_value}";
        }
    }

    [System.Serializable]
    public class SkillStatProperty
    {
        [SerializeField] public ESkillStat StatType;
        [SerializeField] public float Value;
    }
}
