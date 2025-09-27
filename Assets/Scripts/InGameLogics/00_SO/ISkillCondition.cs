using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InGameLogics.Skill;
using UnityEngine;


// 2. 스킬 조건을 정의하는 인터페이스
public interface ISkillCondition
{
    bool CanExecute(SkillInstance skillInstance);
}



// 타겟과의 거리를 체크하는 조건
[System.Serializable]
public class TargetDistanceCondition : ISkillCondition
{
    public float maxDistance = 10.0f;
    public string targetTag = "Enemy";

    public bool CanExecute(SkillInstance skillInstance)
    {
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if (target == null) return false;

        float distance = Vector3.Distance(skillInstance.Owner.GetTransform.position, target.transform.position);
        return distance <= maxDistance;
    }
}
