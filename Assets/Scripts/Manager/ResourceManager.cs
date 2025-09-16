using UnityEngine;

public static class ResourceManager 
{

    public static int gold = 0;

    public static void AddGold(int amount)
    {
        gold += amount;
        GameEvents.OnGoldChanged?.Invoke(gold);
    }

    public static bool SpendGold(int amount)
    {
        if (gold < amount) return false;
        gold -= amount;
        GameEvents.OnGoldChanged?.Invoke(gold);
        return true;
    }
}
