using System.Collections.Generic;
using System.Linq;

namespace InGameLogics.Skill
{
    public class SkillAction
    {
        private List<ISkillActionModule> _baseModules = new();
        private List<ISkillActionModule> _addedModules = new();

        private List<IOnExecute> _onExecute = new();
        private List<IOnHit> _onHitModules = new();
        private List<IOnKill> _onKillModules = new();
        private List<IOnCrit> _onCritModules = new();
        private List<IOnSkillEnd> _onSkillEndModules = new();
        private List<IOnUpdate> _onUpdateModules = new();

        public IReadOnlyList<IOnExecute> OnExecute => _onExecute;
        public IReadOnlyList<IOnHit> OnHit => _onHitModules;
        public IReadOnlyList<IOnKill> OnKill => _onKillModules;
        public IReadOnlyList<IOnCrit> OnCrit => _onCritModules;
        public IReadOnlyList<IOnSkillEnd> OnSkillEnd => _onSkillEndModules;
        public IReadOnlyList<IOnUpdate> OnUpdate => _onUpdateModules;

        bool _isDirty = false;

        public SkillAction(SOSkillAugment[] skillAug)
        {
            _baseModules.Clear();
            foreach (SOSkillAugment aug in skillAug)
            {
                if (aug.SkillActionModules != null)
                    _baseModules.AddRange(aug.SkillActionModules);
            }

            _addedModules.Clear();
            _isDirty = true;
        }

        public void AddModule(ISkillActionModule module)
        {
            if (module == null) return;
            _addedModules.Add(module);
            _isDirty = true;
        }

        public void AddRangeModules(IList<ISkillActionModule> modules)
        {
            if (modules == null || modules.Count == 0) return;
            _addedModules.AddRange(modules);
            _isDirty = true;
        }

        public void RemoveModule(ISkillActionModule module)
        {
            if (module == null) return;
            _addedModules.Remove(module);
            _isDirty = true;
        }

        public void RemoveRangeModules(IList<ISkillActionModule> modules)
        {
            if (modules == null || modules.Count == 0) return;
            foreach (var module in modules)
            {
                RemoveModule(module);
            }
            _isDirty = true;
        }

        private void RecalculateModules()
        {
            var finalModules = _baseModules
                .Concat(_addedModules)
                .OrderBy(m => m.Priority)
                .ToList();

            _onExecute.Clear();
            _onHitModules.Clear();
            _onKillModules.Clear();
            _onCritModules.Clear();
            _onSkillEndModules.Clear();

            foreach (var module in finalModules)
            {
                if (module is IOnExecute onExecute)
                    _onExecute.Add(onExecute);

                if (module is IOnHit onHit)
                    _onHitModules.Add(onHit);

                if (module is IOnKill onKill)
                    _onKillModules.Add(onKill);

                if (module is IOnCrit onCrit)
                    _onCritModules.Add(onCrit);

                if (module is IOnSkillEnd onSkillEnd)
                    _onSkillEndModules.Add(onSkillEnd);

                if (module is IOnUpdate onUpdate)
                    _onUpdateModules.Add(onUpdate);
            }
        }

        public void Ready()
        {
            if (_isDirty)
            {
                RecalculateModules();
                _isDirty = false;
            }
        }

    }

}
