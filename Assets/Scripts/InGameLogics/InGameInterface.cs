using InGameLogics.Skill;
using UnityEngine;

namespace InGameLogics.Interface
{

    public interface ISkillMotion
    {
        void StartMotion(SkillInstance skill);
        void EndMotion();
    }

    public interface ISkillTargetingCondition
    {
        GameObject FindTarget(SkillInstance skill);
    }
}
