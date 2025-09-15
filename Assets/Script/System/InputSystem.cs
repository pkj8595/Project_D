using UnityEngine;

public class InputSystem : MonoBehaviour
{
    GameObject go = null;
    // temp - 추후 TimeManager로 이동 필요
    private async void Update()
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
