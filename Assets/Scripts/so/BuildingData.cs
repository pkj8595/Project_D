using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



[CreateAssetMenu(fileName = "New Building Data", menuName = "Game Data/Building")]
public class BuildingData : SOBase
{
    [Header("기본 정보")]
    public string buildingName;
    public string buildingDescription;
    public Sprite icon;
    public GameObject prefab;

    [Header("기본 스탯")]
    public List<Stat> baseStats;

    [Header("업그레이드 정보")]
    public UpgradeData nextUpgrade; // 다음 단계 업그레이드 타워 데이터 (없으면 null)
}