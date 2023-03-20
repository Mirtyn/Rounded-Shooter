using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;
using System.Linq;
using RoundedShooter;

internal class EnemyWavesControler : ProjectBehaviour
{
    [SerializeField] TimerScript timerScript;
    [SerializeField] Transform enemiesHolder;
    [SerializeField] GoldScript goldScript;

    //Vector3 spawnPos;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject toughEnemy;
    [SerializeField] GameObject boss;

    [SerializeField] GameObject finsishPanel;
    [SerializeField] GameObject submitButtonPanel;

    //List<Wave> Waves = new List<Wave>();

    //public GameObject[] EnemiesOnMap;

    //public int LastSpawnedEnemy;

    private bool _hasSubmitedScore = false;

    private long _nextSpawnerBossGamePoints = 300000;
    private long _nextSpawnerBossGamePointsOffset = 150000;
    private float _nextSpawnerBossDifficultyModifier = 0.75f;

    private float _nextEndlessWaveGameTime = 0f;
    private float _endlessSpeedModifier = 0.30f;
    private float _endlessEnemyCount = 3;
    //private WeightedList<EnemyType> _endlessWeightedList = new WeightedList<EnemyType>();
    private WeightedList2<EnemyType> _endlessWeightedList2 = new WeightedList2<EnemyType>();

    private int _endlessCasualWeight = 1000;
    private int _endlessFastWeight = 50;
    private int _endlessToughWeight = 0;

    private float _endlessWaveDuration = 12f;

    //private System.Random _endlessRandom = new System.Random();


    //int currentWave = 0;
    //int CurrentTimedEnemy { get; set; }

    //private static SquareSpawner _squareSpawner = new SquareSpawner();

    public void Start()
    {
        Game.Reset();

#if _DEBUG

        // super easy
        BuildSuperEasyEnemyWaves(10f, 0.32f, 0);

#else

        if (Game.GameType == GameType.Easy)
        {
            BuildEasyEnemyWaves(15f, 0.32f, 0);
        }
        else if (Game.GameType == GameType.Medium)
        {
            BuildMediumEnemyWaves(15f, 0.50f, 4);
        }
        else if (Game.GameType == GameType.Hard)
        {
            BuildHardEnemyWaves(15f, 0.7f, 10);
        }
        //else if (Game.GameType == GameType.Master)
        //{
        //    BuildMasterEnemyWaves(15f, 1f, 8);
        //}
        else // endless
        {
            _endlessWeightedList2.Add(EnemyType.Casual, _endlessCasualWeight);
            _endlessWeightedList2.Add(EnemyType.Fast, _endlessFastWeight);
            _endlessWeightedList2.Add(EnemyType.Tough, _endlessToughWeight);

            BuildEndlessWave(8f);
        }

#endif
    }

    //private void BuildEndlessWave(float inGameTime)
    //{
    //    if (inGameTime < _nextEndlessWaveGameTime)
    //    {
    //        return;
    //    }

    //    var count = (int)_endlessEnemyCount;

    //    _endlessWeightedList.Clear();

    //    _endlessWeightedList.Add(EnemyType.Casual, _endlessCasualWeight);
    //    _endlessWeightedList.Add(EnemyType.Fast, _endlessFastWeight);
    //    _endlessWeightedList.Add(EnemyType.Tough, _endlessToughWeight);

    //    var t = 4f;

    //    for (var i = 0; i < count; i++)
    //    {
    //        var enemyType = _endlessWeightedList.Next();
    //        var speed = RandomSpeedForEnemyType(enemyType) * _endlessSpeedModifier;

    //        Game.EnemyManager.Enemies.AddRange(_Game.EnemyManager.Spawner.Build(enemyType, inGameTime + t, speed, 1));

    //        t += 4f;
    //    }

    //    _nextEndlessWaveGameTime = inGameTime + 12;
    //    _endlessSpeedModifier = Mathf.Min(_endlessSpeedModifier + 0.025f, 2.25f);
    //    _endlessEnemyCount += 0.15f;
    //    _endlessCasualWeight = Mathf.Max(_endlessCasualWeight - 5, 1);
    //    _endlessFastWeight = Mathf.Min(_endlessCasualWeight + 4, 1000);
    //    _endlessToughWeight = Mathf.Min(_endlessCasualWeight + 2, 1000);

    //    //Debug.Log($"_endlessSpeedModifier: {_endlessSpeedModifier}");
    //}

    private void BuildEndlessWave(float inGameTime)
    {
        if(_nextSpawnerBossGamePoints < Game.ScoreManager.CurrentScore)
        {
            _nextSpawnerBossGamePoints += _nextSpawnerBossGamePointsOffset;

            _nextSpawnerBossGamePointsOffset = System.Math.Max(_nextSpawnerBossGamePointsOffset - 5000, 100000);
            _nextSpawnerBossDifficultyModifier = Mathf.Min(_nextSpawnerBossDifficultyModifier + 0.05f, 2f);

            Game.EnemyManager.Spawner.DifficultyModifier = _nextSpawnerBossDifficultyModifier;
            Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.SpawnerBoss, inGameTime, 1));
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

            Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(enemyType, inGameTime + t, 1));

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
        Game.EnemyManager.Spawner.DifficultyModifier = 0.75f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, 18, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, 24, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, 34, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.SpawnerBoss, 0, 1));
    }

    public void BuildEasyEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {
        var time = basetime;

        //Game.EnemyManager.Spawner.CasualMinSpeed = 0.85f * speedmofifier;
        //Game.EnemyManager.Spawner.CasualMaxSpeed = 0.9f * speedmofifier;

        //Game.EnemyManager.Spawner.FastMinSpeed = 1.75f * speedmofifier;
        //Game.EnemyManager.Spawner.FastMaxSpeed = 2.0f * speedmofifier;

        //Game.EnemyManager.Spawner.ToughMinSpeed = 0.50f * speedmofifier;
        //Game.EnemyManager.Spawner.ToughMaxSpeed = 0.55f * speedmofifier;

        Game.EnemyManager.Spawner.SetDefaultSpeeds(speedmofifier);
        Game.EnemyManager.Spawner.DifficultyModifier = 0.75f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 8, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 20, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 24, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 30, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 34, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 40, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 42, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 48, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 52, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 55, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 58, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 62, 1));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.SpawnerBoss, time + 44, 1));

        //time = 35f;

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 3, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 8, 1));

        //time = 50f;

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 7, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 10, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 14, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 19, 1));

        //time = 75f;

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 2, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 9, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12, 1));

        //time = 94f;

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3f, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 3f, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5f, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 7.75f, 1));

        //time = 108f;
        //speedmofifier += 0.15f * speedmofifier;

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 5, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 7.5f, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 13.5f, 1));

        //time = 130f;
        //speedmofifier += 0.35f * speedmofifier;

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 3, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 5, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 7, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 10, 2));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 12, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 15, 2));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 18, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 19, 1));
        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 22, 2));

        //Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.SpawnerBoss, time + 12, 1));
    }

    public void BuildMediumEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {

        Game.EnemyManager.Spawner.CasualMinSpeed = 0.75f * speedmofifier;
        Game.EnemyManager.Spawner.CasualMaxSpeed = 0.9f * speedmofifier;

        Game.EnemyManager.Spawner.FastMinSpeed = 1.55f * speedmofifier;
        Game.EnemyManager.Spawner.FastMaxSpeed = 2.0f * speedmofifier;

        Game.EnemyManager.Spawner.ToughMinSpeed = 0.45f * speedmofifier;
        Game.EnemyManager.Spawner.ToughMaxSpeed = 0.55f * speedmofifier;


        var time = basetime;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));

        time = 35f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 3, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 8, 0.9f * speedmofifier, 1));

        time = 55f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6, 1.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6.75f, 1.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 10, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 13, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 24, 0.55f * speedmofifier, 1));

        time = 100f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 2, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 2, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 7, 0.85f * speedmofifier, 1));

        time = 119f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 3f, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 3f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6f, 1.55f * speedmofifier, 1));

        time = 128f;
        speedmofifier += 0.125f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 4, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 6, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 9, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 12, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 13.5f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 15f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 16f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16.5f, 1));

        time = 155f;
        speedmofifier += 0.2f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 1.25f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 2.5f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 4.5f, 2));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5.5f, 2));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 7.5f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 9.5f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 10f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 11f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 13f, 2));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 15.5f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 17.5f, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20.5f, 2));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.SpawnerBoss, time + 19.5f, 1));



        var t = 34f;

        var c = 1.0;

        var r = new System.Random();


        for (var i = 0; i < additionalWavesCount; i++)
        {
            var fast = r.NextDouble() < 0.25 ? true : false;

            time += t;

            t--;

            if (t < 16f)
            {
                t = 16f;
            }

            speedmofifier += 0.1f * speedmofifier;

            if (speedmofifier > 1.6f)
            {
                speedmofifier = 1.6f;
            }

            if (fast)
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 9f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 9.8f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 10f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 13f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 24f, 0.55f * speedmofifier, 1));
            }
            else
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
            }

            for (var j = 0; j < (int)c; j++)
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, (int)c));
            }

            if (i < 8)
            {
                c += 0.25f;
            }
            else
            {
                c += 0.5f;
            }
        }
    }

    public void BuildHardEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {

        var time = basetime;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));

        time = 35f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 3, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 8, 0.9f * speedmofifier, 1));

        time = 50f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6.75f, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 10, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 13, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 24, 0.55f * speedmofifier, 1));

        time = 80f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 2, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 2, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 7, 0.85f * speedmofifier, 1));

        time = 94f;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 3f, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 3f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 6f, 1.55f * speedmofifier, 2));

        time = 108f;
        speedmofifier += 0.3f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, 1));

        time = 130f;
        speedmofifier += 0.5f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 1, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 1, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 4, 1f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 5, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 5.5f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 7.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 8.5f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 10, 0.55f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 13, 1.1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 14, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 16, 1.55f * speedmofifier, 2));

        Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.SpawnerBoss, time + 18, 1f, 1));



        var t = 24.0f;

        var c = 1.0;

        var r = new System.Random();


        for (var i = 0; i < additionalWavesCount; i++)
        {
            var fast = r.NextDouble() < 0.25 ? true : false;

            time += t;

            t--;

            if (t < 14f)
            {
                t = 14f;
            }

            speedmofifier += 0.1f * speedmofifier;

            if (speedmofifier > 1.9)
            {
                speedmofifier = 1.9f;
            }

            if (fast)
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 9f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 9.8f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 10f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 12f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 13f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 24f, 0.55f * speedmofifier, 1));
            }
            else
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
            }

            for (var j = 0; j < (int)c; j++)
            {
                Game.EnemyManager.Enemies.AddRange(Game.EnemyManager.Spawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, (int)c));
            }

            if (i < 8)
            {
                c += 0.25f;
            }
            else
            {
                c += 0.5f;
            }
        }
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
            BuildEndlessWave(timerScript.InGameTime);
        }

        foreach (var enemy in Game.EnemyManager.Enemies.Where(o => !o.HasSpawned && o.StartTime <= timerScript.InGameTime))
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
        enemyScript.DifficultyModifier = enemy.DifficultyModifier;

        enemy.InstanceID = gameObject.GetInstanceID();
        enemy.HasSpawned = true;

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
            timerScript.KeepTrackOfTime = false;

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

        var points = Game.ScoreManager.CalculateScore(timerScript.InGameTime, goldScript.Gold, Game.GameType);

        var entry = new Ladder.Entry
        {
            Name = name,
            Points = points,
            Flag = (Ladder.Flag)Game.GameType,
            TimeInSeconds = timerScript.InGameTime,
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
