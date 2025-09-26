using UnityEngine;

[RequireComponent(typeof(GridObject))]
public class Moveable : MonoBehaviour, IMoveable
{
    private GridObject gridObject;
    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
    }
    private void Start()
    {
        Managers.Time.OnUpdate -= Move;
        Managers.Time.OnUpdate += Move;
    }

    public Vector2 TargetPos { get; set; }
    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, gridObject.StatContainer.Stat[EStatType.MoveSpeed] * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (!Managers.Time.IsDestroyed)
            Managers.Time.OnUpdate -= Move;
    }
}
