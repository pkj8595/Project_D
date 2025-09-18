using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;
using Data;

public class UIManager : View<UIManagerPresenter, UIManagerModel>
{
    public TMP_Text goldText;
    // public TMP_Text hpText;
    // public TMP_Text waveText;

    public override void Init()
    {
        Debug.Log("UIManager Init");
        // 유저 골드 데이터 변경 시, UI 업데이트
        Managers.State.Resource.Gold.Subscribe(UpdateGold);
    }

    public override void UpdateUI(UIManagerModel model)
    {
        UpdateGold(model.Gold.Value);
    }

    void UpdateGold(int amount) => goldText.text = "Gold: " + amount;
    // void UpdateHP(float hp) => hpText.text = "HP: " + hp;
    // void UpdateWave(int wave) => waveText.text = "Wave: " + wave;
}

public class UIManagerPresenter : Presenter<UIManagerModel>
{

}

public class UIManagerModel : Model
{
    public ReactiveProperty<int> Gold => Managers.State.Resource.Gold;
}