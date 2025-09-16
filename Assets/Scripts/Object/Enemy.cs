using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(UnitStats))]
public class Enemy : MonoBehaviour
{
    private Transform target;
    private UnitStats stats;

    void Start()
    {
        stats = GetComponent<UnitStats>();
        target = GameManager.Instance.Player.transform;
        stats.OnDeath += () =>
        {
            ResourceManager.AddGold(10);
            Destroy(gameObject);
        };
    }

    void Update()
    {
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.position, stats[StatType.MoveSpeed] * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<UnitStats>().TakeDamage(stats[StatType.Attack]);
            Destroy(gameObject);
        }
    }
}