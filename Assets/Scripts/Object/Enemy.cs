using System.Collections.Generic;
using UnityEngine;

public class Enemy : GridObject
{
    private Moveable moveable;
    private void Awake()
    {
        moveable = GetComponent<Moveable>();
    }

    public override void Init(List<Stat> baseStats, Vector2 initPos)
    {
        base.Init(baseStats, initPos);
    }

    protected override void UpdateProcess()
    {
        base.UpdateProcess();

        if (moveable != null)
        {
            moveable.TargetPos = Managers.Map.PlayerController.transform.position;
        }
    }
}