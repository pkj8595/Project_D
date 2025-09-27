using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading;

public class EnemySpawner : MonoBehaviour
{
    /// <summary>
    /// 웨이브 컨테이너
    /// </summary>
    internal class Wave : IDisposable
    {
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// 웨이브 이벤트 데이터
        /// </summary>
        private WaveEventData waveEventData;
        private int spawnCount;

        /// <summary>
        /// 웨이브 컨테이너 생성 및 몬스터 스폰 시작
        /// </summary>
        /// <param name="waveEventData"></param>
        public Wave(WaveEventData waveEventData)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = new CancellationTokenSource();

            this.waveEventData = waveEventData;
            spawnCount = 0;

            StartSpawn().Forget(Debug.LogError);
        }

        /// <summary>
        /// 몬스터 스폰 프로세스 시작
        /// </summary>
        private async UniTask StartSpawn()
        {
            // 이벤트 시작 시간까지 대기
            float currentTime = Managers.Time.Timer;
            await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Max(0f, waveEventData.startEventTime - currentTime)), cancellationToken: cancellationTokenSource.Token);

            while (true)
            {
                currentTime = Managers.Time.Timer;
                // 이벤트 종료 시간 지나면 해당 웨이브 컨테이너 삭제
                if (currentTime > waveEventData.endEventTime)
                {
                    Dispose();
                    break;
                }

                SpawnEnemy().Forget(Debug.LogError);
                await UniTask.Delay(TimeSpan.FromSeconds(waveEventData.waveIntervalConst), cancellationToken: cancellationTokenSource.Token); // 웨이브 간격 대기
            }
        }
        /// <summary>
        /// 실제 몬스터 스폰
        /// </summary>
        private async UniTask SpawnEnemy()
        {
            foreach (var enemy in waveEventData.enemies)
            {
                var enemyObject = await Managers.Pool.Get(enemy.prefab.name);
                var enemyController = enemyObject.GetComponent<Enemy>();

                // 랜덤 위치 생성 (카메라 밖에서 생성)
                var viewportPosY = Random.Range(0f - 0.1f, 1f + 0.1f);
                var viewportPosX = viewportPosY >= 0f && viewportPosY <= 1f ? (Random.Range(0, 1 + 1) == 0 ? 0f - 0.1f : 1f + 0.1f) : Random.Range(0f - 0.1f, 1f + 0.1f);
                var initPos = Camera.main.ViewportToWorldPoint(new Vector2(viewportPosX, viewportPosY));

                // 적 스탯 가중치 적용
                var FinalBaseStats = new List<Stat>();
                foreach (var stat in enemy.baseStats)
                {
                    float finalValue = stat.value * (waveEventData.waveStatConst + waveEventData.waveStatMod * spawnCount);
                    // Debug.Log($"{(EStatType)stat.type} : {finalValue}");
                    FinalBaseStats.Add(new Stat { type = stat.type, value = finalValue });
                }

                enemyController.Init(FinalBaseStats, initPos);
            }

            spawnCount++;
        }

        /// <summary>
        /// 몬스터 스폰 종료
        /// 토큰 취소 후, 해당 클래스 삭제
        /// </summary>
        public void Dispose()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();

            waveEventData = null;
        }
    }

    public void Init()
    {
        var ingameData = Managers.Data.InGame["InGame_1"];

        CreateWave(ingameData.defaultWave.startEventTime, ingameData.defaultWave).Forget(Debug.LogError);
        // 추가 이벤트 웨이브 생성
        foreach (var wave in ingameData.waves)
        {
            CreateWave(wave.startEventTime, wave).Forget(Debug.LogError);
        }
    }

    /// <summary>
    /// 웨이브 생성
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="waveEventData"></param>
    /// <returns></returns>
    private async UniTask CreateWave(float delay, WaveEventData waveEventData)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: destroyCancellationToken);
        var wave = new Wave(waveEventData);
    }
}