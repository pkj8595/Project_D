using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Consts;
using Unity.VisualScripting;

internal class UnitStats : MonoBehaviour
{
    public float[] stats = new float[(int)StatType.Count];
    public float maxHP => stats[(int)StatType.Hp];

    public float this[StatType t]
    {
        get => stats[(int)t];
        set => stats[(int)t] = value;
    }

    private float currentHP;

    public System.Action<float> OnHPChanged;
    public System.Action OnDeath;

    void Awake()
    {
        
    }

    public void Init()
    {
        currentHP = stats[(int)StatType.Hp];
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        OnHPChanged?.Invoke(currentHP);

        if (currentHP <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        OnHPChanged?.Invoke(currentHP);
    }

    public float GetCurrentHP() => currentHP;
}
