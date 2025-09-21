using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public enum StatType
{
    Hp,
    Attack,
    Defense,
    MoveSpeed,
    AttackSpeed,
    AttackRange,
    Count
}


[System.Serializable]
public class Stat
{
    public StatType type;
    public float value;
}


[CreateAssetMenu(fileName = "New Building Data", menuName = "Game Data/Building")]
public class BuildingData : ScriptableObject
{
    [Header("기본 정보")]
    public string buildingName;
    public string buildingDescription;
    public Sprite icon;
    public GameObject prefab;

    [Header("기본 스탯")]
    public List<Stat> baseStats;

    [Header("업그레이드 정보")]
    public UpgradeData nextUpgrade; // 다음 단계 업그레이드 타워 데이터 (없으면 null)
}


[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Game Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("기본 정보")]
    public string enemyName;
    public GameObject prefab;

    [Header("기본 스탯")]
    public List<Stat> baseStats;

}


[CreateAssetMenu(fileName = "New Upgrade Data", menuName = "Game Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public enum UpgradeType { UpgradeBuilding, UpgradeStat, }
    [Header("기본 정보")]
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public UpgradeType upgradeType;

    [Header("UpgradeStat 업그레이드 효과")]
    public List<StatModifier> modifiers;

    [Header("UpgradeBuilding 업그레이드 효과")]
    public List<BuildingData> upgradeBuilding;
}


[CreateAssetMenu(fileName = "New Card Data", menuName = "Game Data/Card")]
public class CardData : ScriptableObject
{
    public enum CardType { Building, Upgrade }

    [Header("카드 정보")]
    public string cardName;
    [TextArea] public string description;
    public Sprite cardArt;
    public CardType type;

    [Header("카드 내용물")]
    [Tooltip("카드가 Building 타입일 경우 연결")]
    public BuildingData buildingData;

    [Tooltip("카드가 Upgrade 타입일 경우 연결")]
    public UpgradeData upgradeData;
}


[System.Serializable]
public class CardChance
{
    public CardData card;
    [Tooltip("가중치가 높을수록 뽑힐 확률이 높아집니다.")]
    public float weight = 1f;
}

[CreateAssetMenu(fileName = "New Deck Data", menuName = "Game Data/Deck")]
public class CardPoolData : ScriptableObject
{
    [Header("이 덱에 포함될 카드 목록과 가중치")]
    public List<CardChance> cardPool;

    public CardData DrawCard()
    {
        if (cardPool == null || cardPool.Count == 0) return null;

        float totalWeight = 0;
        foreach (var chance in cardPool)
        {
            totalWeight += chance.weight;
        }

        float randomPoint = Random.Range(0, totalWeight);

        foreach (var chance in cardPool)
        {
            if (randomPoint < chance.weight)
            {
                return chance.card;
            }
            else
            {
                randomPoint -= chance.weight;
            }
        }
        return cardPool.Last().card;
    }
}


public enum ResourceType { Wood, Mineral } // 자원 종류 Enum

[System.Serializable]
public class ResourceCost
{
    public ResourceType type;
    public int amount;
}


/// <summary>
/// 인 게임 셋팅 데이터
/// </summary>
[CreateAssetMenu(fileName = "New InGame Data", menuName = "Game Data/InGame")]
public class InGameData : ScriptableObject
{
    [Header("인 게임 셋팅의 기본이 되는 waveData")]
    public WaveEventData defultWave;
    [Header("wave 중 발생하는 이벤트 데이터")]
    public List<WaveEventData> waves;

    [Header("플레이어가 초기에 소유한 카드 풀")]
    public CardPoolData startingPool;

    [Header("게임 중 획득/선택 가능한 모든 카드 풀")]
    [Tooltip("플레이어는 이 목록에 있는 카드 풀 중에서 선택하여 카드를 뽑게 됩니다.")]
    public List<CardPoolData> availablePools;

}

/// <summary>
/// 인 게임 시작시 발생하는 이벤트 데이터
/// 일단 몬스터의 스폰이 추가되는 방식
/// </summary>
[CreateAssetMenu(fileName = "New Wave Data", menuName = "Game Data/Wave")]
public class WaveEventData : ScriptableObject
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
