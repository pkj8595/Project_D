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

    #region EnemySpawner
    [SerializeField] private EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner => enemySpawner;
    #endregion

    #region Map
    public PlayerController PlayerController { get; private set; }
    public async UniTask Init()
    {
        // 플레이어 타워
        var playerTower = await Managers.Pool.Get("PlayerTower");
        var playerTowerController = playerTower.GetComponent<PlayerTower>();

        var baseStats = new List<Stat>();
        // TODO: 플레이어 타워 스탯 추가
        playerTowerController.Init(baseStats, grid.CellToWorld(new Vector2Int(0, 0)));

        // 플레이어
        var player = await Managers.Pool.Get("Player");
        PlayerController = player.GetComponent<PlayerController>();

        baseStats = new List<Stat>();
        baseStats.Add(new Stat { type = EStatType.MoveSpeed, value = 2 });  // 이동속도 추가
        PlayerController.Init(baseStats, grid.CellToWorld(new Vector2Int(0, 0)));

        // 자원 생성 - GoldTower
        Vector2Int[] resourceCellPositions = new Vector2Int[] {
            new Vector2Int(-2, 0),
        };
        foreach (var position in resourceCellPositions)
        {
            var resource = await Managers.Pool.Get("GoldTower");
            var resourceController = resource.GetComponent<GoldTower>();

            baseStats = new List<Stat>();
            // TODO: 자원 타워 스탯 추가
            resourceController.Init(baseStats, grid.CellToWorld(position));
        }

        // 적 생성
        enemySpawner.Init();
    }
    
    #endregion
}
