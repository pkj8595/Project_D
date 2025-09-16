using InGameLogics.Skill;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "skill_", menuName = "ScriptableObjects/skill", order = 1)]
public class SOSkillAugment : ScriptableObject
{
    public string SkillName;
    public string SkillDescription;
    public Sprite SkillIcon;

    public List<SkillStatModifier> SkillStatModifiers = new List<SkillStatModifier>();
    public List<SOSkillActionModule> SkillActionModules = new List<SOSkillActionModule>();
}
