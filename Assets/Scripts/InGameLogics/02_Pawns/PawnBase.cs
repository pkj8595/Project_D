using InGameLogics.Skill;
using InGameLogics;
using UnityEngine;
using System;

public interface IPawnBase 
{
    public EElementType ElementType { get; }
    public StatContainer StatContainer { get; }
    public SkillContainer SkillContainer { get; }
    public bool IsDead { get; }

    public Transform FirePoint { get;}
    public Transform GetTransform { get; }

    event Action OnDeadAction;

    public void TakeDamage(float damage);
}
