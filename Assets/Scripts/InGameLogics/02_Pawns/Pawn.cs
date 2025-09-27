using UnityEngine;
using InGameLogics.Skill;
using InGameLogics;
using System;
using Consts;

public class Pawn : MonoBehaviour, IPawnBase
{
    [field: SerializeField] public EUnitFlags UnitFlags { get; private set; }
    public bool IsDead { get; private set; }
    public SkillContainer SkillContainer { get; private set; }
    public StatContainer StatContainer { get; private set; }
    public Transform GetTransform => transform;
    [field: SerializeField] public Transform FirePoint { get; private set; }



    public event Action OnDeadAction;

    void Awake()
    {
        StatContainer = TryGetComponent(out StatContainer statContainer) ? statContainer : null;
        SkillContainer = TryGetComponent(out SkillContainer skillContainer) ? skillContainer : null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkillContainer.ExecuteSkill_Test();
            Debug.Log(SkillContainer.ToString());
        }

    }

    public void Dead()
    {
        IsDead = true;
        OnDeadAction?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        StatContainer.Stat.Hp -= damage;
        if (StatContainer.Stat.Hp <= 0)
        {
            StatContainer.Stat.Hp = 0;
            Dead();
        }
    }
}
