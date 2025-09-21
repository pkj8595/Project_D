using UnityEngine;
using InGameLogics.Skill;

[CreateAssetMenu(fileName = "skill_", menuName = "ScriptableObjects/skill", order = 1)]
public class SOSkill : SOBase
{
    public SOSkillStat skillBaseStat;
    public SOTargetFinder skillAction;
    public SOSkillAugment[] skillAugments;
}
