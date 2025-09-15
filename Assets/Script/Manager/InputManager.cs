using UnityEngine;
using Cysharp.Threading.Tasks;

public class InputManager : GlobalSingleton<InputManager>
{
    GameObject go = null;

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
            var cell = Managers.Map.ScreenToCell(Input.mousePosition);
            Debug.Log($"cell: {cell}");
            go = await Managers.Pool.Get(nameof(Gold));            
            go.transform.position = Managers.Map.CellToWorld(cell);
            go.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (go != null)
            {
                // Debug.Log("Push");
                Managers.Pool.Push(go);
            }
        }
    }
}
