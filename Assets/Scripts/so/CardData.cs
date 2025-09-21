using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card Data", menuName = "Game Data/Card")]
public class CardData : SOBase
{
    public enum CardType { Building, Upgrade }

    [Header("카드 정보")]
    public string cardName;
    [TextArea] public string description;
    public Sprite cardArt;
    public CardType type;

    [Header("카드 내용물")]
    [Tooltip("카드가 Building 타입일 경우 연결")]
    public BuildingData buildingData;

    [Tooltip("카드가 Upgrade 타입일 경우 연결")]
    public UpgradeData upgradeData;
}
