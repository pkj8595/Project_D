using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;
using Data;

public class UIManager : LocalSingleton<UIManager>
{
    public TMP_Text goldText;
    // public TMP_Text hpText;
    // public TMP_Text waveText;

    public void Init()
    {
        // 유저 골드 데이터 변경 시, UI 업데이트
        Managers.State.Resource.Gold.Subscribe(amount =>
        {
            //string 이 2번 할당되서 메모리 낭비 방지
            // goldText.text = "Gold: " + amount;
            goldText.text = $"Gold: {amount}";
        })
        .AddTo(this);
    }
}