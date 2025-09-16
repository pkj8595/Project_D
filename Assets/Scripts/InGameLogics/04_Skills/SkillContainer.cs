using System.Collections.Generic;
using UnityEngine;

namespace InGameLogics.Skill
{
    // 스킬 컨테이너
    public class SkillContainer 
    {
        private const int c_maxSkillCount = 4; 
        private IPawnBase _owner;
        private List<SkillInstance> _skillInstances = new List<SkillInstance>();

        public IReadOnlyList<SkillInstance> SkillInstances => _skillInstances;
        public bool IsPushableSkill => SkillInstances.Count >= c_maxSkillCount;

        public SkillContainer(IPawnBase owner)
        {
            _owner = owner; 
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var skillInstance in _skillInstances)
            {
                skillInstance.OnUpdate(deltaTime);
            }
        }

        public bool AddSkill(SOSkill skill)
        {
            if (skill == null)
            {
                Debug.LogError("Skill is null");
                return false;
            }

            if (IsPushableSkill)
            {
                Debug.LogWarning("SkillContainer is full, cannot add more skills.");
                return false;
            }

            _skillInstances.Add(new SkillInstance(skill, _owner));
            return false;
        }

        
    }
}
