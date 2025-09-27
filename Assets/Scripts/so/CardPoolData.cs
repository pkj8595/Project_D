using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "CardPoolData_0", menuName = "Game Data/Deck")]
public class CardPoolData : SOBase
{
    [Header("prob data")]
    public List<CardChance> cardPool;

    public CardData DrawCard()
    {
        if (cardPool == null || cardPool.Count == 0) return null;

        float totalWeight = 0;
        foreach (var chance in cardPool)
        {
            totalWeight += chance.weight;
        }

        float randomPoint = UnityEngine.Random.Range(0, totalWeight);

        foreach (var chance in cardPool)
        {
            if (randomPoint < chance.weight)
            {
                return chance.card;
            }
            else
            {
                randomPoint -= chance.weight;
            }
        }
        return cardPool.Last().card;
    }
}
