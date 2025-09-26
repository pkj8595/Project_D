using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum EStatType
{
    MaxHP,
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
    public EStatType type;
    public float value;
}


[System.Serializable]
public class CardChance
{
    public CardData card;
    [Tooltip("가중치가 높을수록 뽑힐 확률이 높아집니다.")]
    public float weight = 1f;
}

public enum ResourceType { Wood, Mineral } // 자원 종류 Enum

[System.Serializable]
public class ResourceCost
{
    public ResourceType type;
    public int amount;
}


