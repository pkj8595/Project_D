using UnityEngine;
using InGameLogics.Skill;
using InGameLogics;
using System;

public class Pawn : MonoBehaviour, IPawnBase
{
    public bool IsDead { get; private set; }
    public SkillContainer SkillContainer { get; private set; }
    public StatContainer StatContainer { get; private set; }
    public Transform GetTransform => transform;
    [field : SerializeField]public Transform FirePoint { get; private set; }

    public EElementType ElementType { get; set; }

    [SerializeField] private int _id;
    //[SerializeField] private SkeletonAnimation _spine;

    private PawnStat _pawnStat = new();

    public event Action OnDeadAction;

    void Awake()
    {
        _pawnStat.Init();
        StatContainer = new StatContainer(_pawnStat, this);
        SkillContainer = new SkillContainer(this);
    }

    private void Update()
    {
        if (IsDead) 
            return;
        SkillContainer.OnUpdate(Time.deltaTime);
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
