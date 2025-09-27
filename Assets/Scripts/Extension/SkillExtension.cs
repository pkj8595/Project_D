using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consts;


public static class SkillExtension
{
    public static ESkillTarget GetSkillTarget(this EUnitFlags targetType, EUnitFlags unitFlags)
    {
        if ((targetType & EUnitFlags.PlayerTeam) != 0)
        {
            return ESkillTarget.Ally;
        }
        else if ((targetType & EUnitFlags.EnemyTeam) != 0)
        {
            return ESkillTarget.Enemy;
        }
        else
        {
            return ESkillTarget.Neutral;
        }
    }

}