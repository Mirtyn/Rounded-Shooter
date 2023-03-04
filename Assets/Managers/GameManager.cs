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

    private GameObject _playerGameObject = null;

    private PlayerScript _playerScript = null;

    public GameObject GetPlayerGameObject()
    {
        if(_playerGameObject == null)
        {
            _playerGameObject = GameObject.FindGameObjectWithTag("MyPlayer");
        }
        
        return _playerGameObject;
    }

    public PlayerScript GetPlayerScript()
    {
        if (_playerScript == null)
        {
            _playerScript = GetPlayerGameObject().GetComponent<PlayerScript>();
        }

        return _playerScript;
    }

    public void Reset(bool resetPlayerData)
    {
        if (resetPlayerData == true)
        {
            PlayerData.Reset();
        }

        EnemyManager.Reset();
        ScoreManager.Reset();
    }
}
