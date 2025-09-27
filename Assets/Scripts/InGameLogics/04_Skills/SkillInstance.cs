using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InGameLogics.Skill
{
    public class SkillInstance
    {
        private IPawnBase _owner;
        private SOSkill _skillBaseData;
        private SkillStat _skillStat;
        private SkillAction _skillAction;

        public Transform FirePoint { get; set; }
        public IPawnBase Owner => _owner;

        /// <summary>
        /// 스킬의 기본 데이터
        /// </summary>
        public SOSkill SkillBaseData => _skillBaseData;

        /// <summary>
        /// 스킬의 스탯을 관리하는 클래스
        /// </summary>
        public SkillStat SkillStat => _skillStat;

        /// <summary>
        /// 스킬의 동작을 관리하는 클래스
        /// </summary>
        public SkillAction SkillAction => _skillAction;
        public List<SOSkillAugment> Augments { get; private set; } = new();

        public float LastExecuteTime { get; set; } = 0f;
        public float CoolTime => _skillStat[ESkillStat.coolTime];
        public float NormalizedCoolTime => CoolTime == 0 ? 0 : Mathf.Clamp01((TimeManager.Instance.Timer - LastExecuteTime) / CoolTime);
        public bool CanExecute
        {
            get
            {
                if (_owner == null || _skillBaseData == null)
                    return false;

                if (_owner.IsDead || TimeManager.Instance.Timer - LastExecuteTime >= CoolTime)
                    return false;

                foreach (var condition in _skillBaseData.skillConditions)
                {
                    if (!condition.CanExecute(this))
                        return false;
                }
                return true;
            }
        }



        public SkillInstance(SOSkill skillData, IPawnBase owner)
        {
            _owner = owner;
            _skillBaseData = skillData;
            _skillStat = new SkillStat(skillData.skillBaseStat);
            _skillAction = new SkillAction(skillData.skillAugments);
            FirePoint = owner.FirePoint;
        }


        public void AddAugment(SOSkillAugment augment)
        {
            if (augment == null) return;
            Augments.Add(augment);
            _skillStat.AddRangeModifier(augment.SkillStatModifiers);
            _skillAction.AddRangeModules(augment.SkillActionModules);
        }

        public void RemoveAugment(SOSkillAugment augment)
        {
            if (augment == null)
                return;
            Augments.Remove(augment);
            _skillStat.RemoveRangeModifier(augment.SkillStatModifiers);
            _skillAction.RemoveRangeModules(augment.SkillActionModules);
        }

        public bool TryExecute()
        {
            if (!CanExecute)
                return false;
            Debug.Log($"ExecuteSkill: {_skillBaseData.SkillName}");

            LastExecuteTime = TimeManager.Instance.Timer;
            Execute();
            return true;
        }

        public void Execute()
        {
            _skillAction.Ready();
            foreach (var onActionModules in _skillAction.OnExecute)
            {
                onActionModules.OnExecute(this);
            }
        }

        public void OnHit(IPawnBase target)
        {
            foreach (var onHitModules in _skillAction.OnHit)
            {
                onHitModules.OnHit(this, target);
            }
        }

        public void OnModuleUpdate(float deltaTime)
        {
            foreach (var onUpdateModules in _skillAction.OnUpdate)
            {
                onUpdateModules.OnUpdate(this, deltaTime);
            }
        }

        public void OnKill(IPawnBase target)
        {
            foreach (var onKillModules in _skillAction.OnKill)
            {
                onKillModules.OnKill(this, target);
            }
        }

        public void OnCrit(IPawnBase target)
        {
            foreach (var onCritModules in _skillAction.OnCrit)
            {
                onCritModules.OnCrit(this, target);
            }
        }

        public void EndSkill()
        {
            foreach (var onSkillEndModules in _skillAction.OnSkillEnd)
            {
                onSkillEndModules.OnSkillEnd(this);
            }
        }

        public override string ToString()
        {
            string str = $"{_skillBaseData}\nCoolTime: {CoolTime}\nNormalizedCoolTime: {NormalizedCoolTime}\nCanExecute: {CanExecute}";
            return str;
        }
    }
}
