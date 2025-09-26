using UnityEngine;

public class BuildingAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackInterval = 1f;
    public LayerMask targetLayer;

    private float timer;

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
