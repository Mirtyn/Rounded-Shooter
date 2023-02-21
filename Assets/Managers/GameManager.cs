using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public ScoreManager ScoreManager { get; set; } = new ScoreManager();
    public EnemyManager EnemyManager { get; set; } = new EnemyManager();
    public PlayerData PlayerData { get; set; } = new PlayerData();

    public GameObject GetPlayerGameObject()
    {
        return GameObject.FindGameObjectWithTag("MyPlayer");
    }

    public void Reset()
    {
        PlayerData.Reset();
        EnemyManager.Reset();
        ScoreManager.Reset();
    }
}
