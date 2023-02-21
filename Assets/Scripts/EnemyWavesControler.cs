using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;
using System.Linq;

public class EnemyWavesControler : ProjectBehaviour
{
    [SerializeField] TimerScript timerScript;
    [SerializeField] Transform enemiesHolder;

    //Vector3 spawnPos;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject toughEnemy;
    [SerializeField] GameObject boss;

    public bool BossSpawned = false;

    //List<Wave> Waves = new List<Wave>();

    //public GameObject[] EnemiesOnMap;

    public int LastSpawnedEnemy;

    //int currentWave = 0;
    //int CurrentTimedEnemy { get; set; }

    //private static SquareSpawner _squareSpawner = new SquareSpawner();

    public void Start()
    {
        Reset();

        // super easy
        BuildSuperEasyEnemyWaves(10f, 0.32f, 0);

        // easy
        //BuildEasyEnemyWaves(15f, 0.32f, 0);

        // medium
        //BuildEnemyWaves(15f, 0.50f, 4);

        // hard
        //BuildEnemyWaves(15f, 0.7f, 10);
    }

    public void BuildSuperEasyEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
    {
        var timedSpawner = new TimedSpawner();

        var time = basetime;

        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1.5f, 0.9f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * speedmofifier, 1));
        //Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.SpawnerBoss, time + 3, 1, 1));
        Game.EnemyManager.Enemies.AddRange(timedSpawner.Build(EnemyType.SpawnerBoss, new Vector3(0f, 0f, 5), time, 1, 1));
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
    }

    public void BuildEnemyWaves(float basetime, float speedmofifier, int additionalWavesCount)
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

            speedmofifier += 0.15f * speedmofifier;

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
        if(enemy.EnemyType == EnemyType.SpawnerBoss)
        {
            SpawnBoss(enemy);
        }

        var gameObject = Instantiate<GameObject>(FindPrefabForEnemy(enemy), enemy.Position, Quaternion.identity, enemiesHolder.transform);

        var enemyScript = gameObject.GetComponent<EnemyScript>();

        enemyScript.Speed = enemy.Speed;

        enemy.InstanceID = gameObject.GetInstanceID();

        enemy.HasSpawned = true;
    }

    void SpawnBoss(Enemy enemy)
    {
        BossSpawned = true;

        var gameObject = Instantiate(boss, enemy.Position, Quaternion.identity);

        enemy.InstanceID = gameObject.GetInstanceID();

        enemy.HasSpawned = true;
    }

    void CheckForGameEnding()
    {
        if(Game.EnemyManager.AreAllDead())
        {
            timerScript.KeepTrackOfTime = false;
        }

        //EnemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");

        //int o = 0;
        //foreach (GameObject i in EnemiesOnMap)
        //{
        //    if (Vector3.Distance(EnemiesOnMap[o].transform.position, this.transform.position) <= 4f)
        //    {
        //        EnemiesOnMap[o].GetComponent<BaseEnemyScript>().BombDeath();
        //    }
        //    o++;
        //}

        //if (LastSpawnedEnemy == TimedEnemies.Count && BossSpawned == true)
        //{

        //}
    }

    //void WaveSpawner()
    //{
    //    if(currentWave == Waves.Count)
    //    {
    //        return;
    //    }

    //    var wave = Waves[currentWave];

    //    if(wave.WaveStartTime <= timerScript.InGameTime)
    //    {
    //        Debug.Log("Wave: " + currentWave + " time: " + timerScript.InGameTime);

    //        SpawnWave(wave);

    //        if (currentWave < Waves.Count)
    //        {
    //            currentWave++;

    //            if (currentWave == Waves.Count)
    //            {
    //                Debug.Log("Waves finished");
    //            }
    //        }
    //    }
    //}

    //void SpawnWave(Wave wave)
    //{
    //    foreach (var enemy in wave.Enemies)
    //    {
    //        SpawnEnemy(enemy);
    //    }
    //}

    //void SpawnEnemy(Enemy enemy)
    //{
    //    spawnPos = Projectbehaviour.UseRadiusSpawner ? new RadiusSpawner(enemy.MinRadius, enemy.MaxRadius).RandomPosition() : _squareSpawner.RandomPosition(enemy);

    //    var gameObject =  Instantiate<GameObject>(FindPrefabForEnemy(enemy), spawnPos, Quaternion.identity, enemiesHolder.transform);

    //    var enemyScript = gameObject.GetComponent<EnemyScript>();

    //    enemyScript.Speed = enemy.Speed;
    //}

    //GameObject FindPrefabForEnemy(Enemy enemy)
    //{
    //    return FindPrefabForEnemyType(enemy.EnemyType);
    //}

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
            default:
                return casualEnemy;
        }
    }
}
