using UnityEngine;
//using InGameLogics;

namespace InGameLogics.SO
{

    public class SOPawn : ScriptableObject
    {
        public string MonsterName;
        public string Description;
        public Sprite Icon;
        
        public EElementType ElementType = EElementType.None;
        public SOStat stat;

    }
}