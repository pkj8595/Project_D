using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;
using System;
using Random = UnityEngine.Random;

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


    public PlayerController PlayerController { get; private set; }
    public async UniTask Init()
    {
        // 플레이어 타워
        var playerTower = await Managers.Pool.Get("PlayerTower");
        var playerTowerController = playerTower.GetComponent<PlayerTower>();
        playerTowerController.Init(CellToWorld(new Vector2Int(0, 0)));

        // 플레이어
        var player = await Managers.Pool.Get("Player");
        PlayerController = player.GetComponent<PlayerController>();
        PlayerController.Init(CellToWorld(new Vector2Int(0, 0)));

        // 자원 생성 - GoldTower
        Vector2Int[] resourceCellPositions = new Vector2Int[] {
            new Vector2Int(-2, 0),
        };
        foreach (var position in resourceCellPositions)
        {
            var resource = await Managers.Pool.Get("GoldTower");
            var resourceController = resource.GetComponent<GoldTower>();
            resourceController.Init(CellToWorld(position));
        }

        // 적 생성
        SpawnEnemy().Forget(Debug.LogError);
    }
    /// <summary>
    /// 적 생성 (테스트용)
    /// </summary>
    /// <returns></returns>
    private async UniTask SpawnEnemy()
    {
        while (true)
        // for (int i = 0; i < 1000; i++)
        {
            var enemy = await Managers.Pool.Get("Enemy");
            var enemyController = enemy.GetComponent<Enemy>();
            
            var viewportPosY = Random.Range(0f-0.1f, 1f+0.1f);
            var viewportPosX = viewportPosY >= 0f && viewportPosY <= 1f ? (Random.Range(0, 1+1) == 0 ? 0f-0.1f : 1f+0.1f) : Random.Range(0f-0.1f, 1f+0.1f);
            var initPos = Camera.main.ViewportToWorldPoint(new Vector2(viewportPosX, viewportPosY));
            enemyController.Init(initPos);

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
    }
}
