using UnityEngine;
using Cysharp.Threading.Tasks;

public class MapManager : LocalSingleton<MapManager>
{
    #region Grid
    [SerializeField] private Grid grid;
    private Vector2 cellOffset = new Vector2(0.5f, 0.5f);

    /// <summary>
    /// 스크린 좌표에서 셀 좌표로 변환
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    public Vector2Int ScreenToCell(Vector2 screenPosition)
    {
        var cell = WorldToCell(Camera.main.ScreenToWorldPoint(screenPosition));
        return cell;
    }

    /// <summary>
    /// 스크린 좌표에서 월드 좌표로 변환
    /// </summary>
    /// <param name="screenPosition"></param>
    /// <returns></returns>
    public Vector2 ScreenToWorld(Vector2 screenPosition)
    {
        var cell = ScreenToCell(screenPosition);
        var world = CellToWorld(cell);
        return world;
    }

    /// <summary>
    /// 월드 좌표에서 셀 좌표로 변환
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 셀 좌표에서 월드 좌표로 변환
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
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
    #endregion


    public async UniTask Init()
    {
        // 플레이어 타워
        var playerTower = await Managers.Pool.Get(nameof(PlayerTower));
        playerTower.transform.position = CellToWorld(new Vector2Int(0, 0));

        // 플레이어
        var initPos = CellToWorld(new Vector2Int(0, 0));
        var player = await Managers.Pool.Get("Player");
        player.transform.position = initPos;
        var playerController = player.GetComponent<PlayerController>();
        playerController.Init(initPos);
    }
}
