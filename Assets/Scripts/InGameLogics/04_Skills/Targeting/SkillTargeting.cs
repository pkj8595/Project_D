using InGameLogics.Skill;
using UnityEngine;

public interface ISkillTargetingCondition
{
    GameObject FindTarget(SkillInstance skillInstance);
    
}

public class ClosestEnemyInRange : ISkillTargetingCondition
{
    public GameObject FindTarget(SkillInstance instance)
    {
        float range = instance.SkillStat[ESkillStat.range];
        IPawnBase owner = instance.Owner;


        //Collider[] hitColliders = Physics2D.OverlapCircleAll(owner.FirePoint.position, range);
        return null;
    }
}