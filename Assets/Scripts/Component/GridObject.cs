using System.Collections.Generic;
using UnityEngine;
using R3;

public class GridObject : MonoBehaviour
{
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

    private void Start()
    {
        Managers.Time.OnUpdate += UpdateProcess;
    }
    private void OnDestroy()
    {
        if (!Managers.Time.IsDestroyed)
            Managers.Time.OnUpdate -= UpdateProcess;
    }

    private void UpdateProcess()
    {
        if (Managers.Map == null)
            return;

        Cell = GridExtension.WorldToCell(Managers.Map.Grid, transform.position);
    }
}