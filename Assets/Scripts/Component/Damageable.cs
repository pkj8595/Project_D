using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class Damageable : MonoBehaviour, IDamageable
{
    private UnitStats stats;
    public HealthSystem HealthSystem { get; set; } = new();    

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        HealthSystem.Init(stats[StatType.Hp]);
    }

    public void OnHit(float attackPower)
    {
    }
}
