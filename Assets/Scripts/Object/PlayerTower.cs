using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class PlayerTower : MonoBehaviour
{
    private UnitStats stats;

    void Start()
    {
        stats = GetComponent<UnitStats>();
        // stats.OnDeath += () =>
        // {
        //     Destroy(gameObject);
        // };
    }

    public void Init(Vector2 initPos)
    {
        transform.position = initPos;
        Managers.Time.OnUpdate += UpdateProcess;
    }

    private Transform target;
    private void UpdateProcess()
    {
        // 가장 가까운 적 찾기
        var gridObjects = GridExtension.GetGridObjectsRange(GridExtension.WorldToCell(Managers.Map.Grid, transform.position), range:1);
        var targetObject = gridObjects.Where(x => x.TryGetComponent<Enemy>(out var enemy)).OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();

        // 적이 있으면 타겟팅 후, 공격
        if (targetObject != null)
        {
            target = targetObject.transform;
            // TODO: 공격 (투사체 생성 후, 타겟 위치로 추적)
        }
    }

    private void OnDestroy()
    {
        if (!Managers.Time.IsDestroyed)
            Managers.Time.OnUpdate -= UpdateProcess;
    }
}
