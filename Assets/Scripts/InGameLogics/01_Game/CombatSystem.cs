using System;
using UnityEngine;

namespace InGameLogics
{
    public enum EElementType
    {
        None,
        Fire,
        Water,
        Earth,
        Air
    }

    public static class CombatSystem
    {
        public static void ApplyDamage(IPawnBase attacker, IPawnBase taker, float damage)
        {
            if (attacker == null || taker == null || attacker.IsDead || taker.IsDead)
                return;
            float multiplier = GetElementalMultiplier(attacker.ElementType, taker.ElementType);
            taker.TakeDamage(damage * multiplier);
        }

        private static float GetElementalMultiplier(EElementType attacker, EElementType taker)
        {
            if (attacker == EElementType.None || taker == EElementType.None)
                return 1f;

            if (IsStrongAgainst(attacker, taker))
                return 1.5f;
            else if (IsWeakAgainst(attacker, taker))
                return 0.75f;
            else
                return 1f;
        }

        private static bool IsStrongAgainst(EElementType attacker, EElementType taker)
        {
            return (attacker == EElementType.Fire && taker == EElementType.Earth) ||
                   (attacker == EElementType.Earth && taker == EElementType.Air) ||
                   (attacker == EElementType.Air && taker == EElementType.Water) ||
                   (attacker == EElementType.Water && taker == EElementType.Fire);
        }

        private static bool IsWeakAgainst(EElementType attacker, EElementType taker)
        {
            return (attacker == EElementType.Fire && taker == EElementType.Water) ||
                   (attacker == EElementType.Earth && taker == EElementType.Fire) ||
                   (attacker == EElementType.Air && taker == EElementType.Earth) ||
                   (attacker == EElementType.Water && taker == EElementType.Air);
        }
    }
}
