using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class InputManager : GlobalSingleton<InputManager>
{
    public event Action OnLeftClickDown;
    public event Action OnLeftClick;
    public event Action OnLeftClickUp;
    public event Action OnRightClickDown;
    public event Action OnRightClick;
    public event Action OnRightClickUp;
    public event Action OnUpdate;

    public void Init()
    {
        Managers.Time.OnUpdate -= InputEvent;
        Managers.Time.OnUpdate += InputEvent;
    }

    public void InputEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClickDown?.Invoke();
        }
        else if (Input.GetMouseButton(0))
        {
            OnLeftClick?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnLeftClickUp?.Invoke();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnRightClickDown?.Invoke();
        }
        else if (Input.GetMouseButton(1))
        {
            OnRightClick?.Invoke();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            OnRightClickUp?.Invoke();
        }

        OnUpdate?.Invoke();
    }
}
