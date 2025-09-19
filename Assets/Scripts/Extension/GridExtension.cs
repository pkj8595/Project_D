using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 그리드 관련 확장 메서드
/// </summary>
public static class GridExtension
{
    private static Vector2 cellOffset = new Vector2(0.5f, 0.5f);

    /// <summary>
    /// 스크린 좌표에서 셀 좌표로 변환
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    public static Vector2Int ScreenToCell(this Grid grid, Vector2 screenPosition)
    {
        var cell = WorldToCell(grid, Camera.main.ScreenToWorldPoint(screenPosition));
        return cell;
    }

    /// <summary>
    /// 스크린 좌표에서 월드 좌표로 변환
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    public static Vector2 ScreenToWorld(this Grid grid, Vector2 screenPosition)
    {
        var cell = ScreenToCell(grid, screenPosition);
        var world = CellToWorld(grid, cell);
        return world;
    }

    /// <summary>
    /// 월드 좌표에서 셀 좌표로 변환
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public static Vector2Int WorldToCell(this Grid grid, Vector3 worldPosition)
    {
        if (grid == null)
        {
            Debug.LogError("Grid is not assigned");
            return Vector2Int.zero;
        }

        var cell = grid.WorldToCell(worldPosition);
        return new Vector2Int(cell.x, cell.y);
    }

    /// <summary>
    /// 셀 좌표에서 월드 좌표로 변환
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    public static Vector2 CellToWorld(this Grid grid, Vector2Int cell)
    {
        if (grid == null)
        {
            Debug.LogError("Grid is not assigned");
            return Vector2.zero;
        }

        var world = grid.CellToWorld(new Vector3Int(cell.x, cell.y, 0));
        return new Vector2(world.x+cellOffset.x, world.y+cellOffset.y);
    }

    /// <summary>
    /// 셀 좌표에 해당하는 그리드 오브젝트들을 가져오기
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="gridObjects"></param>
    /// <returns></returns>
    public static List<GridObject> GetGridObjects(Vector2Int cell)
    {
        var gridObjects = new List<GridObject>();

        if (GridSubject.GridObservers.TryGetValue(cell, out var observers))
        {
            var addGridObjects = observers.Values.ToList();
            gridObjects.AddRange(addGridObjects);
        }

        return gridObjects;
    }

    /// <summary>
    /// 셀 좌표에 해당하는 그리드 오브젝트들을 가져오기 (범위)
    /// </summary>
    /// <param name="center"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static List<GridObject> GetGridObjectsRange(Vector2Int center, int range=0)
    {
        var gridObjects = new List<GridObject>();

        range = Mathf.Max(range, 0);
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                gridObjects.AddRange(GetGridObjects(center + new Vector2Int(x, y)));
            }
        }

        return gridObjects;
    }
}

/// <summary>
/// 그리드 오브젝트 데이터들을 관리하기 위해 만든 클래스
/// </summary>
public static class GridSubject
{
    /// <summary>
    /// Key: 셀 좌표, Value: Key: 오브젝트 인스턴스 ID, Value: 오브젝트
    /// </summary>
    private static Dictionary<Vector2Int, Dictionary<int, GridObject>> gridObservers = new();
    public static IReadOnlyDictionary<Vector2Int, Dictionary<int, GridObject>> GridObservers => gridObservers;

    public static void Add(Vector2Int cell, GridObject gridObject)
    {
        gridObservers.TryAdd(cell, new Dictionary<int, GridObject>());
        gridObservers[cell].TryAdd(gridObject.GetInstanceID(), gridObject);
    }

    public static void Remove(Vector2Int cell, GridObject gridObject)
    {
        if (gridObservers.TryGetValue(cell, out var observers))
        {
            observers.Remove(gridObject.GetInstanceID());
        }
    }
}