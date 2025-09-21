using Const;
using InGameLogics.Skill;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISkillTargetingCondition
{
    IEnumerable<GameObject> FindTarget(SkillInstance skillInstance);
    
}

public class ClosestEnemyInRange : SOTargetFinder, ISkillTargetingCondition
{
    public IEnumerable<GameObject> FindTarget(SkillInstance instance)
    {
        float range = instance.SkillStat[ESkillStat.range];
        IPawnBase owner = instance.Owner;
        Vector2 origin = new (owner.GetTransform.position.x, owner.GetTransform.position.y);

        int layer = (int)ELayer.Unit;
        Collider[] results = new Collider[10];
        Physics.OverlapSphereNonAlloc(owner.GetTransform.position, range, results, layer);

        return results.Where(x => x != null).Select(x => x.gameObject);
    }
}