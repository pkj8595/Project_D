using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consts;
using InGameLogics.Skill;
using UnityEngine;


/// <summary>
/// 스킬 조건을 정의하는 인터페이스
/// 이 페이지에서 실행 조건을 정의
/// </summary>
public interface ISkillCondition
{
    bool CanExecute(SkillInstance skillInstance);
}




[System.Serializable]
public class TargetDistanceCondition : ISkillCondition
{
    public float maxDistance = 10.0f;
    public EUnitFlags targetUnitFlags = EUnitFlags.EnemyTeam;
    public LayerMask targetLayer = (int)ELayer.Unit;

    public bool CanExecute(SkillInstance skillInstance)
    {
        int limit = 10;
        Collider[] targets = new Collider[limit];
        int count = Physics.OverlapSphereNonAlloc(skillInstance.Owner.GetTransform.position,
                                                maxDistance,
                                                targets,
                                                targetLayer);
        if (count == 0) return false;
        else return true;
    }
}
