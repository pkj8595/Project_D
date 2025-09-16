using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class Moveable : MonoBehaviour, IMoveable
{
    private UnitStats stats;

    void Awake()
    {
        stats = GetComponent<UnitStats>();
    }

    public void Move(Vector2 targetPos)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, stats[StatType.MoveSpeed] * Time.deltaTime);
    }
}
