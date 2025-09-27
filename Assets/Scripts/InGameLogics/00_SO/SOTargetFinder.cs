using System.Collections.Generic;
using UnityEngine;


public abstract class SOTargetFinder : SOBase
{
    public abstract IEnumerable<GameObject> FindTarget(InGameLogics.Skill.SkillInstance skillInstance);
}

public abstract class SOSkillActionModule : SOBase
{
    public int priority = 0;

}


