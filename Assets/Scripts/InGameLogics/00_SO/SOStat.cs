using UnityEngine;

namespace InGameLogics.SO
{
    [CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObjects/Stat", order = 1)]
    public class SOStat : ScriptableObject
    {
        public float[] baseStat = new float[(int)EStatType.Count];
        public float[] multiplier = new float[(int)EStatType.Count];
    }
}