using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : GlobalSingleton<TimeManager>
{
    public event Action OnPaused;
    public event Action OnResumed;
    public event Action OnUpdate;

    // 시간 스케일 변경 요청을 스택으로 관리하여 여러 효과가 겹쳐도 안전하게 처리합니다.
    private readonly Stack<float> _timeScaleStack = new ();
    private float _timeAccumulator = 0f;

    /// <summary>
    /// 현재 적용되고 있는 커스텀 시간 배율을 반환합니다.
    /// </summary>
    public float CurrentTimeScale => _timeScaleStack.Peek();

    public bool IsPaused { get; private set; }

    /// <summary>
    /// 현재 게임 진행 시간 (초)
    /// </summary>
    public float Timer { get; private set; }

    public void Awake()
    {
        ClearTimeScale();
        Timer = 0f;
    }

    public void Update()
    {
        if (IsPaused) return;

        Timer += Time.deltaTime;    // 게임 시간 같은 경우는 TimeScale이 0일땐 증가하면 안됨

        // 업데이트 주기(기본 0.02초)를 기준으로 커스텀 업데이트를 실행할지 결정합니다.
        float tickInterval = Time.unscaledDeltaTime;
        // Debug.Log($"TickInterval: {tickInterval}");

        // 현재 시간 배율에 맞게 unscaledDeltaTime을 누적합니다.
        _timeAccumulator += tickInterval * CurrentTimeScale;
        // Debug.Log($"TimeAccumulator: {_timeAccumulator}");

        if (_timeAccumulator >= tickInterval)
        {
            _timeAccumulator -= tickInterval;
            OnUpdate?.Invoke();
            // Debug.Log($"OnUpdate");
        }
    }
    
    public void Pause()
    {
        if (IsPaused) return;

        IsPaused = true;
        OnPaused?.Invoke(); // OnPaused 이벤트 호출
    }

    public void Resume()
    {
        if (!IsPaused) return;

        IsPaused = false;
        OnResumed?.Invoke();
    }

    /// <summary>
    /// 새로운 시간 스케일을 스택에 추가하고 적용합니다. (예: 슬로우 모션 시작)
    /// </summary>
    /// <param name="newScale">새로운 시간 스케일 값</param>
    /// <param name="duration">부드럽게 변경될 시간 (0이면 즉시 변경)</param>
    public void PushTimeScale(float newScale)
    {
        _timeScaleStack.Push(newScale);
    }

    /// <summary>
    /// 스택의 가장 마지막 시간 스케일을 제거하고 이전 스케일로 복원합니다. (예: 슬로우 모션 종료)
    /// </summary>
    /// <param name="duration">부드럽게 변경될 시간 (0이면 즉시 변경)</param>
    public void PopTimeScale()
    {
        if (_timeScaleStack.Count > 1)
        {
            _timeScaleStack.Pop();
        }
    }

    /// <summary>
    /// 모든 시간 스케일 효과를 제거하고 기본 상태(1.0f)로 초기화합니다.
    /// </summary>
    /// <param name="duration">부드럽게 변경될 시간 (0이면 즉시 변경)</param>
    public void ClearTimeScale()
    {
        _timeScaleStack.Clear();
        _timeScaleStack.Push(1.0f);
    }
}
