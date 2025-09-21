using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Data", menuName = "Game Data/Upgrade")]
public class UpgradeData : SOBase
{
    public enum UpgradeType { UpgradeBuilding, UpgradeStat, }
    [Header("기본 정보")]
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;
    public UpgradeType upgradeType;

    [Header("UpgradeStat 업그레이드 효과")]
    public List<StatModifier> modifiers;

    [Header("UpgradeBuilding 업그레이드 효과")]
    public BuildingData upgradeBuilding;
}
