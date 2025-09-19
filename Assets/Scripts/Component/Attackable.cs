using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class Attackable : MonoBehaviour, IAttackable
{
    private UnitStats stats;

    void Awake()
    {
        stats = GetComponent<UnitStats>();
    }

    
    public void Attack()
    {

    }
}
