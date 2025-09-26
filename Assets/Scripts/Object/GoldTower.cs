using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoldTower : GridObject
{
    public override void Init(List<Stat> baseStats, Vector2 initPos)
    {
        base.Init(baseStats, initPos);

        // 골드 증가
        if (goldCoroutine != null) GoldCoroutineHelper.StopCoroutine(goldCoroutine);
        goldCoroutine = GoldCoroutineHelper.StartCoroutine(GoldCoroutine());
    }

    private Coroutine goldCoroutine;
    private IEnumerator GoldCoroutine()
    {
        while (true)
        {
            yield return YieldCache.WaitForSeconds(1f);
            Managers.State.Resource.Gold.Value += 10;            
        }
    }
}
