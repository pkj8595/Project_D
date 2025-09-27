using UnityEngine;
using InGameLogics.Skill;

[CreateAssetMenu(fileName = "skill_", menuName = "ScriptableObjects/skill", order = 1)]
public class SOSkill : SOBase
{
    public string SkillName;
    public string SkillDescription;
    public Sprite SkillIcon;

    /// <summary>
    /// 스킬 스탯
    /// </summary>
    public SOSkillStat skillBaseStat;

    /// <summary>
    /// 스킬 실행 조건
    /// </summary>
    [SerializeReference]
    public ISkillCondition[] skillConditions;

    /// <summary>
    /// 스킬 구성
    /// </summary>
    public SOSkillAugment[] skillAugments;

}
