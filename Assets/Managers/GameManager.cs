using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public ScoreManager ScoreManager { get; set; } = new ScoreManager();
    public EnemyManager EnemyManager { get; set; } = new EnemyManager();

    public GameObject GetPlayerGameObject()
    {
        return GameObject.FindGameObjectWithTag("MyPlayer");
    }
}
