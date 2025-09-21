using InGameLogics.Skill;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSkillStat", menuName = "ScriptableObjects/SOSkillStat", order = 1)]
public class SOSkillStat : SOBase
{
    [SerializeField] private float[] baseStat = new float[(int)ESkillStat.Count];//원본 스킬 스탯
    public float GetStat(ESkillStat statType)
    {
        return baseStat[(int)statType];
    }
}
