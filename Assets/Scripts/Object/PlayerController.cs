using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(UnitStats))]
[RequireComponent(typeof(Moveable))]
public class PlayerController : MonoBehaviour
{
    private UnitStats stats;
    private Moveable moveable;

    void Awake()
    {
        stats = GetComponent<UnitStats>();
        stats.OnHPChanged += hp => GameEvents.OnPlayerHPChanged?.Invoke(hp);
        stats.OnDeath += () => GameEvents.OnPlayerDeath?.Invoke();

        moveable = GetComponent<Moveable>();
    }

    public void Init(Vector2 initPos)
    {
        targetPos = initPos;

        Managers.Input.OnUpdate -= UpdateProcess;
        Managers.Input.OnUpdate += UpdateProcess;

        Managers.Input.OnRightClickUp -= OnRightClickUp;
        Managers.Input.OnRightClickUp += OnRightClickUp;
    }

    /// <summary>
    /// 플레이어가 최종적으로 움직일 위치
    /// </summary>
    private Vector2 targetPos;
    /// <summary>
    /// 업데이트 처리 - Update() 문과 동일 기능
    /// </summary>    
    private void UpdateProcess()
    {
        if (moveable != null)
            moveable.Move(targetPos);
    }
    /// <summary>
    /// 마우스 오른쪽 버튼을 클릭하고 뗐을 때 호출되는 이벤트 함수
    /// </summary>
    private void OnRightClickUp()
    {
        targetPos = Managers.Map.ScreenToWorld(Input.mousePosition);
    }

    // void Update()
    // {
    //     float h = Input.GetAxis("Horizontal");
    //     float v = Input.GetAxis("Vertical");
    //     transform.Translate(stats[StatType.MoveSpeed] * Time.deltaTime * new Vector3(h, 0, v));
    // }
}