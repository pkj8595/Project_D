using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


internal class UnitStats : MonoBehaviour
{
    public int maxHP = 100;
    public int attackDamage = 10;
    public float attackRange = 1f;
    public float moveSpeed = 5f;

    private int currentHP;

    public System.Action<int> OnHPChanged;
    public System.Action OnDeath;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        OnHPChanged?.Invoke(currentHP);

        if (currentHP <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        OnHPChanged?.Invoke(currentHP);
    }

    public int GetCurrentHP() => currentHP;
}
