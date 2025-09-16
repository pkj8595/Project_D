using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class Moveable : MonoBehaviour, IMoveable
{
    private UnitStats stats;
    private void Awake()
    {
        stats = GetComponent<UnitStats>();
    }
    private void Start()
    {
        Managers.Time.OnUpdate -= Move;
        Managers.Time.OnUpdate += Move;
    }

    public Vector2 TargetPos { get; set; }
    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, stats[StatType.MoveSpeed] * Time.deltaTime);
    }
}
