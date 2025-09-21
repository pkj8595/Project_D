using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// 인 게임 시작시 발생하는 이벤트 데이터
/// 일단 몬스터의 스폰이 추가되는 방식
/// </summary>
[CreateAssetMenu(fileName = "New Wave Data", menuName = "Game Data/Wave")]
public class WaveEventData : SOBase
{
    public float startEventTime;
    public float endEventTime;
    public List<EnemyData> enemies;
    public float waveStatMod;
    public float waveStatConst;

    /// <summary>
    /// 시간당 몬스터 스폰 간격
    /// </summary>
    public float waveIntervalConst;

    /// <summary>
    /// 시간당 몬스터 스폰 간격 증가 값
    /// </summary>
    public float waveIntervalMod;
}
