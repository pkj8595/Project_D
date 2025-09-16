using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using Unity.Profiling;

public class InputManager : GlobalSingleton<InputManager>
{
    // test
    // GameObject go = null;

    public event Action OnLeftClickDown;
    public event Action OnRightClickDown;

    public void Init()
    {
        TimeManager.Instance.OnControlledUpdate -= InputEvent;
        TimeManager.Instance.OnControlledUpdate += InputEvent;
    }

    public void InputEvent(float deltaTime) => InputEventAsync(deltaTime).Forget(Debug.LogError);
    private async UniTask InputEventAsync(float deltaTime)
    {
        if (Input.GetMouseButtonDown(0))
        {
            // test
            // var cell = Managers.Map.ScreenToCell(Input.mousePosition);
            // Debug.Log($"cell: {cell}");
            // go = await Managers.Pool.Get(nameof(Gold));            
            // go.transform.position = Managers.Map.CellToWorld(cell);
            // go.SetActive(true);

            OnLeftClickDown?.Invoke();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            // test
            // if (go != null)
            // {
            //     // Debug.Log("Push");
            //     Managers.Pool.Push(go);
            // }

            OnRightClickDown?.Invoke();
        }
    }
}
