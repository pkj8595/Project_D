using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitStats))]
public class GoldTower : MonoBehaviour
{
    private UnitStats stats;

    void Start()
    {
        stats = GetComponent<UnitStats>();
    }

    public void Init(Vector2 initPos)
    {
        transform.position = initPos;

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
