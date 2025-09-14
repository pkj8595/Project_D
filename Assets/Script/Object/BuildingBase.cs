using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class BuildingBase : MonoBehaviour
{
    private UnitStats stats;

    void Awake()
    {
        stats = GetComponent<UnitStats>();
        stats.OnDeath += OnDestroyed;
    }

    void OnDestroyed()
    {
        Destroy(gameObject);
    }
}