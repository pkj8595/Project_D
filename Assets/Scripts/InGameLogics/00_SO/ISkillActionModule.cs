using System.Collections.Generic;
using UnityEngine;
using InGameLogics.Skill;
using InGameLogics;
using Consts;
using System.Linq;

/// <summary>
/// 스킬 액션 모듈 이 페이지에서 행동의 정의
/// </summary>
public interface ISkillActionModule
{
    public int Priority { get; set; }
}


[System.Serializable]
public class DamageModule : ISkillActionModule, IOnHit
{
    [field: SerializeField] public int Priority { get; set; }

    public void OnHit(SkillInstance skill, IPawnBase target)
    {
        var targetPawn = target;
        var damage = skill.SkillStat[ESkillStat.damage];
        var attackDamage = skill.Owner.StatContainer.Stat[EStatType.Attack] * damage;
        CombatSystem.ApplyDamage(skill.Owner, targetPawn, attackDamage);
    }
}

[System.Serializable]
public class SingleTargetModule : ISkillActionModule, IOnExecute
{
    [field: SerializeField] public int Priority { get; set; }
    [field: SerializeField] public ESkillTarget TargetType { get; set; }

    public void OnExecute(SkillInstance skill)
    {
        int layerMask = (int)ELayer.Unit;
        var targets = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(skill.Owner.GetTransform.position,
                                            skill.SkillStat[ESkillStat.range],
                                            targets,
                                            layerMask);
        if (count == 0) return;

        EUnitFlags ownerUnitFlags = skill.Owner.UnitFlags;

        Vector3 ownerPos = skill.Owner.GetTransform.position;
        var target = targets
                    .Select(t => t.gameObject.TryGetComponent(out IPawnBase pawnBase) ? pawnBase : null)
                    .Where(t => ownerUnitFlags.GetSkillTarget(t.UnitFlags) == TargetType)
                    .Aggregate((a, b) => Vector3.Distance(ownerPos, a.GetTransform.position) < Vector3.Distance(ownerPos, b.GetTransform.position) ? a : b);

        skill.OnHit(target);
    }
}

