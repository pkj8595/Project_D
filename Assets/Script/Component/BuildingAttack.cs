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
        Collider[] targets = Physics.OverlapSphere(transform.position, stats.attackRange, targetLayer);
        if (targets.Length > 0)
        {
            var target = targets[0];
            if (projectilePrefab != null)
            {
                GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Projectile p = proj.GetComponent<Projectile>();
                p.Init(target.transform, stats.attackDamage);
            }
        }
    }
}
