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
    public float startEventTime = 0f;
    public float endEventTime = 0f;
    public List<EnemyData> enemies;

    /// <summary>
    /// 몬스터 스탯 고정 증가 값
    /// </summary>
    public float waveStatConst = 1f;
    /// <summary>
    /// 스폰당 몬스터 스탯 추가 증가 값
    /// </summary>
    public float waveStatMod = 0f;    

    /// <summary>
    /// 몬스터 스폰 간격
    /// </summary>
    public float waveIntervalConst = 1f;
}
