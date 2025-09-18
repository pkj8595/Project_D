using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class Gold : MonoBehaviour
{
    private UnitStats stats;

    void Start()
    {
        stats = GetComponent<UnitStats>();
    }
}
