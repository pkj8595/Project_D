using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Game Data/Enemy")]
public class EnemyData : SOBase
{
    [Header("기본 정보")]
    public string enemyName;
    public GameObject prefab;

    [Header("기본 스탯")]
    public List<Stat> baseStats;

}

