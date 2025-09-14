using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class PlayerController : MonoBehaviour
{
    private UnitStats stats;

    void Awake()
    {
        stats = GetComponent<UnitStats>();
        stats.OnHPChanged += hp => GameEvents.OnPlayerHPChanged?.Invoke(hp);
        stats.OnDeath += () => GameEvents.OnPlayerDeath?.Invoke();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(stats.moveSpeed * Time.deltaTime * new Vector3(h, 0, v));
    }
}