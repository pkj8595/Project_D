using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class UnitCombat : MonoBehaviour
{
    private UnitStats stats;

    public LayerMask targetLayer;
    public float attackInterval = 1f;

    private float timer;

    void Awake()
    {
        stats = GetComponent<UnitStats>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= attackInterval)
        {
            timer = 0f;
            AttackNearest();
        }
    }

    void AttackNearest()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, stats[StatType.AttackRange], targetLayer);
        if (targets.Length > 0)
        {
            var targetStats = targets[0].GetComponent<UnitStats>();
            if (targetStats != null)
            {
                targetStats.TakeDamage(stats[StatType.Attack]);
            }
        }
    }
}
