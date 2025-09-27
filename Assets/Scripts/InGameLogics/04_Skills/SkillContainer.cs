using System.Collections.Generic;
using UnityEngine;

namespace InGameLogics.Skill
{
    // 스킬 컨테이너
    public class SkillContainer : MonoBehaviour
    {
        private IPawnBase _owner;
        [Header("Initial Skills")]
        [SerializeField] private List<SOSkill> _initialSkills = new();

        [Header("Skill Instances")]
        [SerializeField] private List<SkillInstance> _skillInstances = new();
        public IReadOnlyList<SkillInstance> SkillInstances => _skillInstances;

        public event System.Action<SOSkill> OnSkillAdded;

        void Awake()
        {
            _owner = TryGetComponent(out IPawnBase pawnBase) ? pawnBase : null;
            foreach (var skill in _initialSkills)
                AddSkill(skill);
        }

        public bool AddSkill(SOSkill skill)
        {
            if (skill == null)
            {
                Debug.LogError("Skill is null");
                return false;
            }

            _skillInstances.Add(new SkillInstance(skill, _owner));
            OnSkillAdded?.Invoke(skill);
            return false;
        }




    }
}
