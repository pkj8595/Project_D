using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class PlayerController : MonoBehaviour
{
    private UnitStats stats;
    private Moveable moveable;

    private void Awake()
    {
        stats = GetComponent<UnitStats>();

        moveable = GetComponent<Moveable>();
        Managers.Input.OnRightClickUp += OnRightClickUp;
    }

    public void Init(Vector2 initPos)
    {
        transform.position = initPos;

        if (moveable != null)
            moveable.TargetPos = initPos;
    }
    /// <summary>
    /// 마우스 오른쪽 버튼을 클릭하고 뗐을 때 호출되는 이벤트 함수
    /// </summary>
    private void OnRightClickUp()
    {
        if (moveable != null)
            moveable.TargetPos = Managers.Map.ScreenToWorld(Input.mousePosition);
    }

    private void OnDestroy()
    {
        if (!Managers.Input.IsDestroyed)
            Managers.Input.OnRightClickUp -= OnRightClickUp;
    }
}