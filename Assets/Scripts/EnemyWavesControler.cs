using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;
using System.Linq;
using RoundedShooter;

internal class EnemyWavesControler : ProjectBehaviour
{
    //[SerializeField] TimerScript timerScript;
    [SerializeField] Transform enemiesHolder;
    [SerializeField] GoldScript goldScript;

    //Vector3 spawnPos;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject toughEnemy;
    [SerializeField] GameObject boss;

    [SerializeField] GameObject finsishPanel;
    [SerializeField] GameObject submitButtonPanel;

    private bool _hasSubmitedScore = false;

    private long _nextSpawnerBossGamePoints = 250000;
    private long _nextSpawnerBossGamePointsOffset = 150000;
    private float _nextSpawnerBossDifficultyModifier = 0.85f;
    private int _nextSpawnerBossHP = 46;

    private float _nextEndlessWaveGameTime = 0f;
    private float _endlessSpeedModifier = 0.30f;
    private float _endlessEnemyCount = 3;

    private float _mediumOrHardSpeedModifier = 0.70f;

    private WeightedList2<EnemyType> _endlessWeightedList2 = new WeightedList2<EnemyType>();

    private int _endlessCasualWeight = 1000;
    private int _endlessFastWeight = 50;
    private int _endlessToughWeight = 0;

    private float _endlessWaveDuration = 12f;
    private float _bossWaveSpawnTime = 0f;
    private bool _bossWaveEven = false;

    public void Start()
    {
        Game.Reset();

#if _DEBUG

        // super easy
        BuildSuperEasyEnemyWaves(10f, 0.32f, 0);

#else

        if (Game.GameType == GameType.Easy)
        {
            BuildEasyEnemyWaves();
        }
        else if (Game.GameType == GameType.Medium)
        {
            BuildMediumEnemyWaves();
        }
        else if (Game.GameType == GameType.Hard)
        {
            BuildHardEnemyWaves();
        }
        else // endless
        {
            _endlessWeightedList2.Add(EnemyType.Casual, _endlessCasualWeight);
            _endlessWeightedList2.Add(EnemyType.Fast, _endlessFastWeight);
            _endlessWeightedList2.Add(EnemyType.Tough, _endlessToughWeight);

            BuildEndlessWave(8f);
        }

#endif
    }

    private void BuildEndlessWave(float inGameTime)
    {
        if(_nextSpawnerBossGamePoints < Game.ScoreManager.CurrentScore)
        {
            _nextSpawnerBossGamePoints += _nextSpawnerBossGamePointsOffset;

            _nextSpawnerBossGamePointsOffset = System.Math.Max(_nextSpawnerBossGamePointsOffset - 5000, 100000);
            _nextSpawnerBossDifficultyModifier = Mathf.Min(_nextSpawnerBossDifficultyModifier + 0.10f, 2f);

            Game.EnemyManager.Spawner.BossDifficultyModifier = _nextSpawnerBossDifficultyModifier;
            Game.EnemyManager.Enemies.Add(Game.EnemyManager.Spawner.BuildEnemy(EnemyType.SpawnerBoss, inGameTime, _nextSpawnerBossHP));
            
            _nextSpawnerBossHP += 4;
        }

        if (inGameTime < _nextEndlessWaveGameTime)
        {
            if(Game.EnemyManager.AreAllDead())
            {
                _nextEndlessWaveGameTime = Mathf.Min(_nextEndlessWaveGameTime, inGameTime + 1f);
            }
            return;
        }

        var count = (int)Mathf.Floor(_endlessEnemyCount);

        var t = 4f;

        for (var i = 0; i < count; i++)
        {
            var enemyType = _endlessWeightedList2.Next();
            //var speed = RandomEndlessWaveSpeedForEnemyType(enemyType) * _endlessSpeedModifier;

            Game.EnemyManager.Spawner.SetDefaultSpeeds(_endlessSpeedModifier);

            Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(enemyType, inGameTime + t, 1));

            t += 4f;
        }

        _nextEndlessWaveGameTime = inGameTime + Mathf.Ceil(_endlessWaveDuration);
        _endlessSpeedModifier = Mathf.Min(_endlessSpeedModifier + 0.0125f, 2.25f);
        _endlessWaveDuration = Mathf.Max(_endlessWaveDuration - 0.0125f, 8f);
        _endlessEnemyCount += 0.075f;

        _endlessWeightedList2[EnemyType.Casual] = Mathf.Max(_endlessWeightedList2[EnemyType.Casual] - 5, 500);
        _endlessWeightedList2[EnemyType.Fast] = Mathf.Min(_endlessWeightedList2[EnemyType.Fast] + 10, 1000);
        _endlessWeightedList2[EnemyType.Tough] = Mathf.Min(_endlessWeightedList2[EnemyType.Tough] + 2, 1000);
    }

    public void BuildSuperEasyEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {
        Game.EnemyManager.Spawner.SetDefaultSpeeds(0.25f);
        Game.EnemyManager.Spawner.BossDifficultyModifier = 0.75f;

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, 18, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, 24, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, 34, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.SpawnerBoss, 0, 1));
        Game.EnemyManager.Enemies.Add(Game.EnemyManager.Spawner.BuildEnemy(EnemyType.SpawnerBoss, 4, 32));
    }

    public void BuildEasyEnemyWaves()
    {
        Game.EnemyManager.Spawner.SetDefaultSpeeds(0.65f);
        Game.EnemyManager.Spawner.BossDifficultyModifier = 0.75f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 15, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 23, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 27, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 32, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 37, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 42, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 46, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 48, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 54, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 58, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 64, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 68, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 70, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 72, 1));

        Game.EnemyManager.Enemies.Add(Game.EnemyManager.Spawner.BuildEnemy(EnemyType.SpawnerBoss, 68, 32));
    }

    public void BuildMediumEnemyWaves()
    {
        _mediumOrHardSpeedModifier = 0.70f;

        //Game.PlayerData.ArrowSpeedLevel = 4;
        //Game.PlayerData.PlayerTurnSpeedLevel = 4;
        //Game.PlayerData.ShootingCooldownLevel = 4;
        //Game.PlayerData.Bombs = 3;

        Game.EnemyManager.Spawner.SetDefaultSpeeds(_mediumOrHardSpeedModifier);
        Game.EnemyManager.Spawner.BossDifficultyModifier = 0.95f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 15, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 18, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 23, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 27, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 32, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 37, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 42, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 46, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 48, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 54, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 58, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 64, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 68, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 72, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 74, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 80, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 84, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 86, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 88, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 94, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 98, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 100, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 102, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 104, 1));

        Game.EnemyManager.Enemies.Add(Game.EnemyManager.Spawner.BuildEnemy(EnemyType.SpawnerBoss, 100, 40));
    }

    public void BuildHardEnemyWaves()
    {
        _mediumOrHardSpeedModifier = 0.75f;

        Game.EnemyManager.Spawner.SetDefaultSpeeds(_mediumOrHardSpeedModifier);
        Game.EnemyManager.Spawner.BossDifficultyModifier = 1.20f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 15, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 18, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 23, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 27, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 32, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 37, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 42, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 46, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 48, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 54, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 58, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 64, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 68, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 72, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 74, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 80, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 84, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 86, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 88, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 94, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 98, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 100, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 102, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 104, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 105, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 106, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 108, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 110, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 112, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 118, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 122, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 124, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 126, 1));

        _mediumOrHardSpeedModifier += 0.05f;
        Game.EnemyManager.Spawner.SetDefaultSpeeds(_mediumOrHardSpeedModifier);

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 130, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 134, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 138, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 140, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 140, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 142, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 146, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 148, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 152, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 154, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 156, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 156, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 156, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 160, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 164, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 164, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 170, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 170, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 170, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 170, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 174, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 174, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 178, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, 180, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 180, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, 182, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, 182, 1));

        _mediumOrHardSpeedModifier += 0.05f;
        Game.EnemyManager.Spawner.SetDefaultSpeeds(_mediumOrHardSpeedModifier);

        Game.EnemyManager.Enemies.Add(Game.EnemyManager.Spawner.BuildEnemy(EnemyType.SpawnerBoss, 178, 90));
    }

    void Update()
    {
        SpawnEnemies();

        CheckForGameEnding();
    }

    void SpawnEnemies()
    {
        if(Game.GameType == GameType.Endless)
        {
            BuildEndlessWave(Game.TimerScript.InGameTime);
        }

        if (Game.GameType == GameType.Hard && _bossWaveSpawnTime <= Game.TimerScript.InGameTime && Game.EnemyManager.HasBossSpawnedAndIsAlive())
        {
            var time = Game.TimerScript.InGameTime;

            if(_bossWaveEven)
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 0f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 4f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, time + 7f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 10f, 1));
                //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 14f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, time + 14f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, time + 18f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 20f, 1));
            }
            else
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 0f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 4f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, time + 7f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 10f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, time + 12f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, time + 15f, 1));
                //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 18f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 19f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, time + 22f, 1));
            }

            _bossWaveEven = !_bossWaveEven;
            _bossWaveSpawnTime = Game.TimerScript.InGameTime + 24f;
            _mediumOrHardSpeedModifier += 0.02f;
            Game.EnemyManager.Spawner.SetDefaultSpeeds(_mediumOrHardSpeedModifier);

        }

        if (Game.GameType == GameType.Medium && _bossWaveSpawnTime <= Game.TimerScript.InGameTime && Game.EnemyManager.HasBossSpawnedAndIsAlive())
        {
            var time = Game.TimerScript.InGameTime;

            if (_bossWaveEven)
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 0f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 3f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, time + 6f, 1));
                //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, time + 8f, 1));
            }
            else
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 0f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Tough, time + 4f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Casual, time + 7f, 1));
                //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6f, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.BuildEnemies(EnemyType.Fast, time + 9f, 1));
            }

            _bossWaveEven = !_bossWaveEven;
            _bossWaveSpawnTime = Game.TimerScript.InGameTime + 10f;
            _mediumOrHardSpeedModifier += 0.03f;
            Game.EnemyManager.Spawner.SetDefaultSpeeds(_mediumOrHardSpeedModifier);
        }

        foreach (var enemy in Game.EnemyManager.Enemies.Where(o => !o.HasSpawned && o.StartTime <= Game.TimerScript.InGameTime))
        {
            Debug.Log($"Enemy spawned:{Game.EnemyManager.Enemies.Count(o => o.HasSpawned) + 1} /{Game.EnemyManager.Enemies.Count}");

            //LastSpawnedEnemy = Game.EnemyManager.Enemies.Count(o => o.HasSpawned) + 1;

            SpawnEnemy(enemy);
        }
    }

    void SpawnEnemy(Enemy enemy)
    {
        var gameObject = Instantiate<GameObject>(FindPrefabForEnemy(enemy), enemy.Position, Quaternion.identity, enemiesHolder.transform);

        var enemyScript = gameObject.GetComponent<EnemyScript>();

        enemyScript.Speed = enemy.Speed;

        enemy.InstanceID = gameObject.GetInstanceID();
        enemy.HasSpawned = true;

        if(enemy.EnemyType == EnemyType.SpawnerBoss)
        {
            var bossScript = gameObject.GetComponent<BossScript>();

            bossScript.DifficultyModifier = enemy.BossDifficultyModifier;
            bossScript.HP = enemy.BossHP;
            bossScript.StartHP = enemy.BossHP;
        }

    }

    void CheckForGameEnding()
    {
        if(Game.GameType == GameType.Endless)
        {
            if (Game.PlayerData.IsDead )//&& !_hasSubmitedScore)
            {
                submitButtonPanel.SetActive(true);
                //SubmitButtonPressed();
            }

            return;
        }

        if (Game.EnemyManager.AreAllDead())
        {
            Game.TimerScript.KeepTrackOfTime = false;

            finsishPanel.SetActive(true);
            submitButtonPanel.SetActive(true);

            //if (!Game.PlayerData.IsDead && !_hasSubmitedScore)
            //{
            //    SubmitButtonPressed();
            //}
        }
    }

    public void SubmitButtonPressed(string name)
    {
        var version = GameManager.Version;

        var ladderService = new LadderClientApi(@"https://mirtyn.be/rounded-shooter/ladder/post");

        //var ladderService = new LadderClientApi(@"https://localhost:7245/rounded-shooter/ladder/post");

        var points = Game.ScoreManager.CalculateScore(Game.TimerScript.InGameTime, goldScript.Gold, Game.GameType);

        var entry = new Ladder.Entry
        {
            Name = name,
            Points = points,
            Flag = (Ladder.Flag)Game.GameType,
            TimeInSeconds = Game.TimerScript.InGameTime,
        };

        if (ladderService.TryPost(
            entry, 
            version, 
            out LadderClientApi.PostResponse response))
        {
            _hasSubmitedScore = true;

            Debug.Log("The data was succesfully saved.");
            Debug.Log($"You are position {response.Position} on the ladder.");
        }
        else
        {
            Debug.Log("The data could not be saved.");
            Debug.Log("Please try again latter.");
        }
    }

    GameObject FindPrefabForEnemy(Enemy enemy)
    {
        return FindPrefabForEnemyType(enemy.EnemyType);
    }

    GameObject FindPrefabForEnemyType(EnemyType enemytype)
    {
        switch (enemytype)
        {
            case EnemyType.Fast:
                return fastEnemy;
            case EnemyType.Tough:
                return toughEnemy;
            case EnemyType.SpawnerBoss:
                return boss;
            default:
                return casualEnemy;
        }
    }
}
