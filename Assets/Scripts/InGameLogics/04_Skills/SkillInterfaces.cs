using System.Collections.Generic;
using UnityEngine;

namespace InGameLogics.Skill
{
    public interface IOnExecute
    {
        void OnExecute(SkillInstance skill);
    }

    public interface IOnHit
    {
        void OnHit(SkillInstance skill, IPawnBase target);
    }

    public interface IOnKill
    {
        void OnKill(SkillInstance skill, IPawnBase target);
    }

    public interface IOnCrit
    {
        void OnCrit(SkillInstance skill, IPawnBase target);
    }

    public interface IOnSkillEnd
    {
        void OnSkillEnd(SkillInstance skill);
    }

    public interface IOnUpdate
    {
        void OnUpdate(SkillInstance skill, float deltaTime);
    }

}
