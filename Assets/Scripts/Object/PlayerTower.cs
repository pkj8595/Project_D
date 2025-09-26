using System.Linq;
using UnityEngine;

public class PlayerTower : GridObject
{
    private Transform target;
    protected override void UpdateProcess()
    {
        base.UpdateProcess();

        // 가장 가까운 적 찾기
        var gridObjects = GridExtension.GetGridObjectsRange(GridExtension.WorldToCell(Managers.Map.Grid, transform.position), range:1);
        var targetObject = gridObjects.Where(x => x.TryGetComponent<Enemy>(out var enemy)).OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();

        // 적이 있으면 타겟팅 후, 공격
        if (targetObject != null)
        {
            // Debug.Log($"Target: {targetObject.name}");
            target = targetObject.transform;
            // TODO: 공격 (투사체 생성 후, 타겟 위치로 추적)
        }
    }
}
