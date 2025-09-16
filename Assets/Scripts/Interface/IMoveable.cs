using UnityEngine;

public interface IMoveable
{
    public Vector2 TargetPos { get; set; }
    public void Move();
}
