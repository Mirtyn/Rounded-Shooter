using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;
using System.Linq;
using RoundedShooter;

public class EnemyWavesControler : ProjectBehaviour
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

    //List<Wave> Waves = new List<Wave>();

    //public GameObject[] EnemiesOnMap;

    public int LastSpawnedEnemy;

    private bool _hasSubmitedScore = false;

    //int currentWave = 0;
    //int CurrentTimedEnemy { get; set; }

    //private static SquareSpawner _squareSpawner = new SquareSpawner();

    public void Start()
    {
        Game.Reset();

//#if DEBUG

//        // super easy
//        BuildSuperEasyEnemyWaves(10f, 0.32f, 0);

//#else

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
        else if (Game.GameType == GameType.Master)
        {
            BuildMasterEnemyWaves(15f, 1f, 8);
        }
        else // Random
        {

        }

//#endif
    }

    public void BuildSuperEasyEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {
        var timedSpawner = new TimedSpawner();

        var time = basetime;

        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.SpawnerBoss, time + 3, 1, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.35f, 1));
    }

    public void BuildEasyEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {
        var timedSpawner = new TimedSpawner();

        var time = basetime;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));

        time = 35f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 8, 0.9f * speedmofifier, 1));

        time = 50f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 2f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6, 2f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6.75f, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13, 2f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16, 1f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24, 0.55f * speedmofifier, 1));

        time = 80f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 2, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 2, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 1f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7, 0.85f * speedmofifier, 1));

        time = 94f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3f, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3f, 0.9f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6f, 1.55f * speedmofifier, 2));

        time = 108f;
        speedmofifier += 0.15f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, 1));

        time = 130f;
        speedmofifier += 0.35f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 1, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 4, 1f * speedmofifier, 2));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 0.85f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 5.5f, 1.75f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7.5f, 1f * speedmofifier, 1));
        //Game.EnemyManager.TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 8.5f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 10, 0.55f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 13, 1.1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 14, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16, 1.55f * speedmofifier, 2));

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.SpawnerBoss, time + 10, 1f, 1));
    }

    public void BuildMediumEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {
        var timedSpawner = new TimedSpawner();

        var time = basetime;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));

        time = 35f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 8, 0.9f * speedmofifier, 1));

        time = 55f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6, 1.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6.75f, 1.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24, 0.55f * speedmofifier, 1));

        time = 100f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 2, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 2, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7, 0.85f * speedmofifier, 1));

        time = 119f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3f, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6f, 1.55f * speedmofifier, 1));

        time = 128f;
        speedmofifier += 0.125f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, 1));

        time = 155f;
        speedmofifier += 0.2f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.95f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1.25f, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 2.5f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4.5f, 0.5f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5.5f, 1f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7.5f, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9.5f, 1.7f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 11f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13f, 0.55f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15.5f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 17.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20.5f, 1.55f * speedmofifier, 2));

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.SpawnerBoss, time + 19.5f, 1f, 1));



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
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9.8f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24f, 0.55f * speedmofifier, 1));
            }
            else
            {
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
            }

            for (var j = 0; j < (int)c; j++)
            {
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, (int)c));
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
        var timedSpawner = new TimedSpawner();

        var time = basetime;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));

        time = 35f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 8, 0.9f * speedmofifier, 1));

        time = 50f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6.75f, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24, 0.55f * speedmofifier, 1));

        time = 80f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 2, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 2, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7, 0.85f * speedmofifier, 1));

        time = 94f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3f, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6f, 1.55f * speedmofifier, 2));

        time = 108f;
        speedmofifier += 0.3f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, 1));

        time = 130f;
        speedmofifier += 0.5f * speedmofifier;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 1, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.5f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 4, 1f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 5.5f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 8.5f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 10, 0.55f * speedmofifier, 2));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 13, 1.1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 14, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16, 1.55f * speedmofifier, 2));

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.SpawnerBoss, time + 18, 1f, 1));



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
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9.8f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24f, 0.55f * speedmofifier, 1));
            }
            else
            {
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.45f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
            }

            for (var j = 0; j < (int)c; j++)
            {
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, (int)c));
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

    public void BuildMasterEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {
        var timedSpawner = new TimedSpawner();

        var time = basetime;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 14, 0.35f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16, 2f * speedmofifier, 1));


        time = 35f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 8, 0.9f * speedmofifier, 1));

        time = 50f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6.75f, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));

        speedmofifier += 0.05f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24, 0.35f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 20, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 24, 2f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 28, 0.35f * speedmofifier, 1));

        time = 90f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 2, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 2, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.3f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3f, 0.3f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 3f, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 6f, 1.55f * speedmofifier, 1));

        time = 120f;
        speedmofifier += 0.05f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.3f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.3f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 17f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 19f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 21f, 0.825f * speedmofifier, 1));

        time = 150f;
        speedmofifier += 0.1f;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 1, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.3f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 4, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 5.5f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7.5f, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 8.5f, 1.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 10, 0.35f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 13, 1.1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 14, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16, 1.55f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10, 0.8f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 11, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13, 0.3f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15, 1f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 17, 0.85f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 1.75f * speedmofifier, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 22f, 1f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 24, 1.8f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 26, 0.35f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 27, 1.1f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 30, 1f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 34, 1.55f * speedmofifier, 2));

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.SpawnerBoss, time + 25, 1f, 1));



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

            speedmofifier += 0.02f;

            if (fast)
            {
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9.8f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12f, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24f, 0.35f * speedmofifier, 1));
            }
            else
            {
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.3f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.3f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * speedmofifier, 1));
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 17f, 1.75f * speedmofifier, 1));
            }

            for (var j = 0; j < (int)c; j++)
            {
                Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * speedmofifier, (int)c));
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
        //CheckForBossSpawn();

        SpawnEnemies();

        CheckForGameEnding();
    }

    //void CheckForBossSpawn()
    //{
    //    if (timerScript.InGameTime >= 140 && BossSpawned == false)
    //    {
    //        BossSpawned = true;
    //        Instantiate(boss, new Vector3(0f, 0f, 5), Quaternion.identity);
    //    }
    //}

    void SpawnEnemies()
    {
        //if (Game.EnemyManager.TimedEnemies.Any(o => !o.HasSpawned))
        //{
            foreach (var enemy in Game.EnemyManager.Enemies.Where(o => !o.HasSpawned && o.StartTime <= timerScript.InGameTime))
            {
                Debug.Log($"Enemy spawned:{Game.EnemyManager.Enemies.Count(o => o.HasSpawned) + 1} /{Game.EnemyManager.Enemies.Count}");

                LastSpawnedEnemy = Game.EnemyManager.Enemies.Count(o => o.HasSpawned) + 1;

                SpawnEnemy(enemy);
            }
        //}
    }

    void SpawnEnemy(Enemy enemy)
    {
        var gameObject = enemy.EnemyType != EnemyType.SpawnerBoss
            ? Instantiate<GameObject>(FindPrefabForEnemy(enemy), enemy.Position, Quaternion.identity, enemiesHolder.transform)
            : Instantiate<GameObject>(FindPrefabForEnemy(enemy), enemy.Position, Quaternion.identity, enemiesHolder.transform);

        var enemyScript = gameObject.GetComponent<EnemyScript>();

        enemyScript.Speed = enemy.Speed;

        enemy.InstanceID = gameObject.GetInstanceID();

        enemy.HasSpawned = true;
    }

    void CheckForGameEnding()
    {
        if (Game.EnemyManager.AreAllDead())
        {
            timerScript.KeepTrackOfTime = false;

            finsishPanel.SetActive(true);

            if (!Game.PlayerData.IsDead && !_hasSubmitedScore)
            {
                SubmitButtonPressed();
            }
        }
    }

    public void SubmitButtonPressed()
    {
        var version = GameManager.Version;

        var ladderService = new LadderClientApi(@"https://mirtyn.be/rounded-shooter/ladder/post");

        var points = Game.ScoreManager.CalculateScore(timerScript.InGameTime, goldScript.Gold);

        var entry = new Ladder.Entry
        {
            Name = "[unknown]",
            Points = points,
            Flag = (Ladder.Flag)Game.GameType,
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
