using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Text goldText;
    public Text hpText;
    public Text waveText;

    void OnEnable()
    {
        GameEvents.OnGoldChanged += UpdateGold;
        GameEvents.OnPlayerHPChanged += UpdateHP;
        GameEvents.OnWaveChanged += UpdateWave;
    }

    void OnDisable()
    {
        GameEvents.OnGoldChanged -= UpdateGold;
        GameEvents.OnPlayerHPChanged -= UpdateHP;
        GameEvents.OnWaveChanged -= UpdateWave;
    }

    void UpdateGold(int amount) => goldText.text = "Gold: " + amount;
    void UpdateHP(int hp) => hpText.text = "HP: " + hp;
    void UpdateWave(int wave) => waveText.text = "Wave: " + wave;
}
