using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "skillAction_", menuName = "ScriptableObjects/skillAction", order = 1)]
public class SOTargetFinder : SOBase
{
}

public abstract class SOSkillActionModule : SOBase
{
    public int priority = 0;

}


