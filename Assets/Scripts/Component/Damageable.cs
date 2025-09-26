using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    public HealthSystem HealthSystem { get; set; } = new();    

    public void OnHit(float attackPower)
    {
    }
}
