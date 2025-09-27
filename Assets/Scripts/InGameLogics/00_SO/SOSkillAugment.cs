using InGameLogics.Skill;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "skillAugment_", menuName = "ScriptableObjects/skillAugment", order = 1)]
public class SOSkillAugment : SOBase
{
    public List<SkillStatModifier> SkillStatModifiers = new();

    [SerializeReference]
    public List<ISkillActionModule> SkillActionModules = new();
}
