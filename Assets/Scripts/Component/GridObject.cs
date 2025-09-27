using System.Collections.Generic;
using Consts;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    /// <summary>
    /// 임시로 추가
    /// </summary>
    [field: SerializeField] public EUnitFlags UnitFlags { get; private set; }
    /// <summary>
    /// 오브젝트 스탯
    /// </summary>
    public StatContainer StatContainer { get; private set; }
    public virtual void Init(List<Stat> baseStats, Vector2 initPos)
    {
        if (TryGetComponent(out StatContainer statContainer))
        {
            StatContainer = statContainer;
            StatContainer.SetBaseStats(baseStats);
        }

        transform.position = initPos;
    }

    /// <summary>
    /// 오브젝트 셀 좌표
    /// </summary>
    private Vector2Int cell;
    public Vector2Int Cell
    {
        get => cell;
        set
        {
            if (cell != value)
            {
                GridSubject.Remove(cell, this);
                cell = value;
                GridSubject.Add(cell, this);
            }
        }
    }

    /// <summary>
    /// 오브젝트 셀 좌표 업데이트
    /// </summary>
    protected virtual void Start()
    {
        Managers.Time.OnUpdate += UpdateProcess;
    }
    protected virtual void OnDestroy()
    {
        if (!Managers.Time.IsDestroyed)
            Managers.Time.OnUpdate -= UpdateProcess;
    }
    protected virtual void UpdateProcess()
    {
        if (Managers.Map == null)
            return;

        Cell = GridExtension.WorldToCell(Managers.Map.Grid, transform.position);
    }
}