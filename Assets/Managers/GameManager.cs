using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class GameManager
{
    public bool ClampY = false;

    //static public bool UseRadiusSpawner = true;

    public bool IsPaused = false;

    public static string Version = "1.0.0";

    public GameType GameType = GameType.None;

    public ScoreManager ScoreManager { get; set; } = new ScoreManager();

    public EnemyManager EnemyManager { get; set; } = new EnemyManager();

    public PlayerData PlayerData { get; set; } = new PlayerData();

    private GameObject _playerGameObject = null;

    private PlayerScript _playerScript = null;

    private TimerScript _timerScript = null;


    private static readonly GameManager _instance = new GameManager();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static GameManager()
    {
    }

    private GameManager()
    {
    }

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }





    public GameObject GetPlayerGameObject()
    {
        if(_playerGameObject == null)
        {
            _playerGameObject = GameObject.FindGameObjectWithTag("MyPlayer");
        }
        
        return _playerGameObject;
    }

    public TimerScript TimerScript
    {
        get
        {
            if (_timerScript == null)
            {
                _timerScript = GameObject.FindGameObjectWithTag("TimeKeeper").GetComponent<TimerScript>();
            }

            return _timerScript;
        }
    }

    public PlayerScript GetPlayerScript()
    {
        if (_playerScript == null)
        {
            _playerScript = GetPlayerGameObject().GetComponent<PlayerScript>();
        }

        return _playerScript;
    }

    public void Reset()
    {
        IsPaused = false;

        PlayerData.Reset();
        EnemyManager.Reset();
        ScoreManager.Reset();
    }
}
