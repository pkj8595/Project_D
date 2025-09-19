using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class MapManager : LocalSingleton<MapManager>
{
    #region Grid
    [SerializeField] private Grid grid;
    public Grid Grid => grid;
    #endregion

    #region Map
    public PlayerController PlayerController { get; private set; }
    public async UniTask Init()
    {
        // 플레이어 타워
        var playerTower = await Managers.Pool.Get("PlayerTower");
        var playerTowerController = playerTower.GetComponent<PlayerTower>();
        playerTowerController.Init(grid.CellToWorld(new Vector2Int(0, 0)));

        // 플레이어
        var player = await Managers.Pool.Get("Player");
        PlayerController = player.GetComponent<PlayerController>();
        PlayerController.Init(grid.CellToWorld(new Vector2Int(0, 0)));

        // 자원 생성 - GoldTower
        Vector2Int[] resourceCellPositions = new Vector2Int[] {
            new Vector2Int(-2, 0),
        };
        foreach (var position in resourceCellPositions)
        {
            var resource = await Managers.Pool.Get("GoldTower");
            var resourceController = resource.GetComponent<GoldTower>();
            resourceController.Init(grid.CellToWorld(position));
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
    #endregion
}
