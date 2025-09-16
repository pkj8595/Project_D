using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/*
============================================================
* NEW: StatType.cs (Enum)
- 게임에서 사용될 모든 스탯의 종류를 열거형(enum)으로 정의합니다.
- 문자열 대신 이 enum을 사용하여 스탯을 관리함으로써 타입 안정성과 편의성을 확보합니다.
============================================================
*/
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

/*
============================================================
1. Stat.cs (MODIFIED)
- 기존의 string key를 StatType enum으로 변경했습니다.
============================================================
*/
[System.Serializable]
public class Stat
{
    public StatType type;
    public float value;
}

/*
============================================================
2. BuildingComponentData.cs (추상 클래스)
- (변경 없음)
============================================================
*/
public abstract class BuildingComponentData : ScriptableObject
{
    public string componentName;
}

/*
============================================================
3. BuildingData.cs (MODIFIED)
- 건물의 기본 스탯(체력, 비용 등)을 새로운 Stat 시스템으로 관리하도록 변경했습니다.
============================================================
*/
[CreateAssetMenu(fileName = "New Building Data", menuName = "Game Data/Building")]
public class BuildingData : ScriptableObject
{
    [Header("기본 정보")]
    public string buildingName;
    public Sprite icon;
    public GameObject prefab;

    [Header("기본 스탯")]
    public List<Stat> baseStats;

    [Header("조립할 부품 데이터 목록")]
    public List<BuildingComponentData> components;

    // 특정 타입의 컴포넌트 데이터를 쉽게 찾기 위한 헬퍼 함수
    public T GetComponentData<T>() where T : BuildingComponentData
    {
        return components.OfType<T>().FirstOrDefault();
    }

    // 특정 스탯의 기본값을 쉽게 찾기 위한 헬퍼 함수
    public float GetBaseStatValue(StatType type)
    {
        var stat = baseStats.FirstOrDefault(s => s.type == type);
        return stat != null ? stat.value : 0f;
    }
}

/*
============================================================
4. EnemyData.cs (MODIFIED)
- 적의 스탯을 새로운 Stat 시스템으로 관리하도록 변경했습니다.
============================================================
*/
[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Game Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [Header("기본 정보")]
    public string enemyName;
    public GameObject prefab;

    [Header("기본 스탯")]
    public List<Stat> baseStats;

    // 특정 스탯의 기본값을 쉽게 찾기 위한 헬퍼 함수
    public float GetBaseStatValue(StatType type)
    {
        var stat = baseStats.FirstOrDefault(s => s.type == type);
        return stat != null ? stat.value : 0f;
    }
}

/*
============================================================
5. UpgradeData.cs (MODIFIED)
- StatModifier가 string key 대신 StatType enum을 사용하도록 변경했습니다.
============================================================
*/


[CreateAssetMenu(fileName = "New Upgrade Data", menuName = "Game Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    [Header("기본 정보")]
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("업그레이드 효과")]
    public List<StatModifier> modifiers;
}

/*
============================================================
6. CardData.cs
- (변경 없음)
============================================================
*/
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

/*
============================================================
7. DeckData.cs
- (변경 없음)
============================================================
*/
[System.Serializable]
public class CardChance
{
    public CardData card;
    [Tooltip("가중치가 높을수록 뽑힐 확률이 높아집니다.")]
    public float weight = 1f;
}

[CreateAssetMenu(fileName = "New Deck Data", menuName = "Game Data/Deck")]
public class DeckData : ScriptableObject
{
    [Header("이 덱에 포함될 카드 목록과 가중치")]
    public List<CardChance> cardPool;

    // 가중치를 기반으로 랜덤하게 카드 하나를 뽑는 함수
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
