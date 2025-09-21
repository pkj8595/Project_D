using InGameLogics;
using InGameLogics.Skill;
using System;
using System.Threading;
using TMPro;
using UnityEngine;

public class Monster : MonoBehaviour, IPawnBase
{
    private MonsterStat _monsterStat = new MonsterStat();

    public EElementType ElementType { get; set; }
    public StatContainer StatContainer { get; private set; }
    public SkillContainer SkillContainer { get; private set; }

    public bool IsDead { get; set; } = true;
    public int InstanceId { get; private set; } = -1;

    [SerializeField] private Transform _firePoint;
    public Transform GetTransform => this.transform;
    public Transform FirePoint => _firePoint;


    public event Action OnDeadAction;

    void Update()
    {
        if (IsDead)
            return;

        transform.position += StatContainer.Stat[EStatType.MoveSpeed] * Time.deltaTime * Vector3.down;
        SkillContainer.OnUpdate(Time.deltaTime);
    }

    public void Init(int monsterID, int instanceId)
    {
        InstanceId = instanceId;
        ElementType = EElementType.Fire;
        gameObject.name = $"Monster_{monsterID}_{instanceId}";

        _monsterStat.Init();
        StatContainer = new StatContainer(_monsterStat, this);
        SkillContainer = new SkillContainer(this);

        IsDead = false;
    }

    public void TakeDamage(float damage)
    {
        StatContainer.Stat.Hp -= damage;
        if (StatContainer.Stat.Hp <= 0 && !IsDead)
        {
            IsDead = true;
            OnDeadAction?.Invoke();
            Destroy(gameObject);
        }
    }
}
