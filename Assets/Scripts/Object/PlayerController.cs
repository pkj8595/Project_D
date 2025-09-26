using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GridObject
{
    private Moveable moveable;

    private void Awake()
    {
        moveable = GetComponent<Moveable>();
        Managers.Input.OnRightClickUp += OnRightClickUp;
    }

    public override void Init(List<Stat> baseStats, Vector2 initPos)
    {
        base.Init(baseStats, initPos);

        if (moveable != null)
            moveable.TargetPos = initPos;
    }
    /// <summary>
    /// 마우스 오른쪽 버튼을 클릭하고 뗐을 때 호출되는 이벤트 함수
    /// </summary>
    private void OnRightClickUp()
    {
        if (moveable != null)
            moveable.TargetPos = Managers.Map.Grid.ScreenToWorld(Input.mousePosition);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        if (!Managers.Input.IsDestroyed)
            Managers.Input.OnRightClickUp -= OnRightClickUp;
    }
}