using UnityEngine;
using InGameLogics.Skill;

[CreateAssetMenu(fileName = "skill_", menuName = "ScriptableObjects/skill", order = 1)]
public class SOSkill : ScriptableObject
{
    public SOSkillStat skillBaseStat;
    public SOSkillActions skillAction;
    public SOSkillAugment[] skillAugments;
}
