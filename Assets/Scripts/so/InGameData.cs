using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 인 게임 셋팅 데이터
/// </summary>
[CreateAssetMenu(fileName = "New InGame Data", menuName = "Game Data/InGame")]
public class InGameData : SOBase
{
    [Header("인 게임 셋팅의 기본이 되는 waveData")]
    public WaveEventData defaultWave;
    [Header("wave 중 발생하는 이벤트 데이터")]
    public List<WaveEventData> waves;

    [Header("플레이어가 초기에 소유한 카드 풀")]
    public CardPoolData startingCardPool;

    [Header("게임 중 획득/선택 가능한 모든 카드 풀")]
    //[Tooltip("플레이어는 이 목록에 있는 카드 풀 중에서 선택하여 카드를 뽑게 됩니다.")]
    public List<CardPoolData> availableCardPools;

}
