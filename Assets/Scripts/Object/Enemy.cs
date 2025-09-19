using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(UnitStats))]
public class Enemy : MonoBehaviour
{
    private Transform target;
    private UnitStats stats;
    private Moveable moveable;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();
        // stats.OnDeath += () =>
        // {
        //     // ResourceManager.AddGold(10);
        //     Destroy(gameObject);
        // };

        moveable = GetComponent<Moveable>();
        Managers.Time.OnUpdate += UpdateProcess;
    }

    public void Init(Vector2 initPos)
    {
        transform.position = initPos;
        target = Managers.Map.PlayerController.transform;        
    }

    private void UpdateProcess()
    {
        if (moveable != null && target != null)
        {
            moveable.TargetPos = target.position;
        }
    }

    private void OnDestroy()
    {
        if (!Managers.Time.IsDestroyed)
            Managers.Time.OnUpdate -= UpdateProcess;
    }
}