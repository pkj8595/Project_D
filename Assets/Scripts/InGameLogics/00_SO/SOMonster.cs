using InGameLogics.SO;
using InGameLogics;
using UnityEngine;


public enum EMonsterMoveType
{
    Linear,
    X,
}

public class SOMonster : ScriptableObject
{
    public string MonsterName;
    public string Description;
    public Sprite Icon;

    public EElementType ElementType = EElementType.None;
    public SOStat stat;

    public EMonsterMoveType MoveType = EMonsterMoveType.Linear;
}

