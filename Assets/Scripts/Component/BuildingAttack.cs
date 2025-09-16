using UnityEngine;

public class BuildingAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackInterval = 1f;
    public LayerMask targetLayer;

    private UnitStats stats;
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
            Attack();
        }
    }

    void Attack()
    {
        
    }
}
