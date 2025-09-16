using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class PlayerTower : MonoBehaviour
{
    private Transform target;
    private UnitStats stats;

    void Start()
    {
        stats = GetComponent<UnitStats>();
        stats.OnDeath += () =>
        {
            Destroy(gameObject);
        };
    }

    public void Init(Vector2 initPos)
    {
        transform.position = initPos;
    }

    // void Update()
    // {
    //     if (target != null)
    //         Attack();
    // }

    // void Attack()
    // {
    //     if (target != null)
    //         target.GetComponent<UnitStats>().TakeDamage(stats[StatType.Attack]);
    // }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.transform.CompareTag("Enemy"))
    //     {
    //         collision.transform.GetComponent<UnitStats>().TakeDamage(stats[StatType.Attack]);
    //     }
    // }
}
