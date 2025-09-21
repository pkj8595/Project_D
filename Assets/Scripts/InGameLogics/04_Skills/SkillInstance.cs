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
        public SOSkill SkillBaseData => _skillBaseData;
        public SkillStat SkillStat => _skillStat;
        public SkillAction SkillAction => _skillAction;

        public float Cool { get; set; }
        public float CoolTime => _skillStat[ESkillStat.coolTime];

        public List<SOSkillAugment> Augments { get; private set; } = new List<SOSkillAugment>();

        public SkillInstance(SOSkill skillData, IPawnBase owner)
        {
            _owner = owner;
            _skillBaseData = skillData;
            _skillStat = new SkillStat(skillData.skillBaseStat, owner);
            _skillAction = new SkillAction(skillData.skillAction.modules, null);
            FirePoint = owner.FirePoint;
        }

        public bool CanExecute => !_owner.IsDead && CoolTime <= Cool;

        public void AddAugment(SOSkillAugment augment)
        {
            if (augment == null) return;
            Augments.Add(augment);
            _skillStat.AddRangeModifier(augment.SkillStatModifiers);
            _skillAction.AddRangeModules(augment.SkillActionModules);
        }

        public void OnUpdate(float deltaTime)
        {
            Cool += deltaTime;
        }

        public bool TryExecute()
        {
            if (!CanExecute) return false;
            Cool = 0;
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

        public void OnHit(GameObject target)
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

        public void OnKill(GameObject target)
        {
            foreach (var onKillModules in _skillAction.OnKill)
            {
                onKillModules.OnKill(this, target);
            }
        }

        public void OnCrit(GameObject target)
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
    }
}
