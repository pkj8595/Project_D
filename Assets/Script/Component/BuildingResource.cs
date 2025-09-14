using UnityEngine;

public class BuildingResource : MonoBehaviour
{
    public int resourcePerTick = 5;
    public float resourceInterval = 3f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= resourceInterval)
        {
            timer = 0f;
            ResourceManager.AddGold(resourcePerTick);
        }
    }
}