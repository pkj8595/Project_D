using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "skillAction_", menuName = "ScriptableObjects/skillAction", order = 1)]
public class SOSkillActions : ScriptableObject
{
    public List<SOSkillActionModule> modules;
}

public abstract class SOSkillActionModule : ScriptableObject
{
    public int priority = 0;

}


public class SOSkillActionModule_CreateProjectile : SOSkillActionModule
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 5f;
    public void Execute(Vector3 position, Vector3 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * projectileSpeed;
        }
        Destroy(projectile, projectileLifetime);
    }
}