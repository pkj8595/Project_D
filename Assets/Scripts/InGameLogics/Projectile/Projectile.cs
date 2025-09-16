using UnityEngine;
using InGameLogics.Skill;

namespace InGameLogics
{
    public class Projectile : MonoBehaviour
    {
        private SkillInstance skill;

        public void Setup(SkillInstance skill)
        {
            this.skill = skill;
        }

        public void Update()
        {
            if (skill == null) return;
            // Update projectile position or logic here if needed
            // For example, move the projectile forward based on its speed
            skill.OnModuleUpdate(Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pawn"))
            {
                skill.OnHit(other.gameObject);

                var pawnBase = other.GetComponent<IPawnBase>();
                if (pawnBase != null && pawnBase.IsDead)
                {
                    skill.OnKill(other.gameObject);
                }
            }
        }

        public void DestroyProjectile()
        {
            skill.EndSkill();
            Destroy(gameObject);
        }
    }
}
