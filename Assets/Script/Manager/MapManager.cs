using UnityEngine;

public class MapManager : LocalSingleton<MapManager>
{
    [SerializeField] private Grid grid;
    private Vector2 cellOffset = new Vector2(0.5f, 0.5f);

    public Vector2Int ScreenToCell(Vector2 screenPosition)
    {
        return WorldToCell(Camera.main.ScreenToWorldPoint(screenPosition));
    }

    public Vector2Int WorldToCell(Vector3 worldPosition)
    {
        if (grid == null)
        {
            Debug.LogError("Grid is not assigned");
            return Vector2Int.zero;
        }

        var cell = grid.WorldToCell(worldPosition);
        return new Vector2Int(cell.x, cell.y);
    }

    public Vector2 CellToWorld(Vector2Int cell)
    {
        if (grid == null)
        {
            Debug.LogError("Grid is not assigned");
            return Vector2.zero;
        }

        var world = grid.CellToWorld(new Vector3Int(cell.x, cell.y, 0));
        return new Vector2(world.x+cellOffset.x, world.y+cellOffset.y);
    }
}
