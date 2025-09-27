using System;

namespace Consts
{
    public enum ELayer
    {
        Default = 1 << 0,
        TransparentFX = 1 << 1,
        IgnoreRayCast = 1 << 2,
        Water = 1 << 4,
        UI = 1 << 5,
        Unit = 1 << 6,
    }

    [Flags]
    public enum EUnitFlags
    {
        PlayerTeam = 1 << 0,
        EnemyTeam = 1 << 1,
        Building = 1 << 2,
        Unit = 1 << 3,
    }
    public enum CardType
    {
        Building,
        Unit,
        Spell,
    }

    public enum BuildingType
    {
        Resource,
        Attack,
        Wall,
    }

    public enum ESkillTarget
    {
        Neutral,
        Ally,
        Enemy,
    }



}