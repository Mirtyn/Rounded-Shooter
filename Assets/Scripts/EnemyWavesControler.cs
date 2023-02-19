using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;
using System.Linq;

public class EnemyWavesControler : Projectbehaviour
{
    [SerializeField] TimerScript timerScript;
    [SerializeField] Transform enemiesHolder;

    Vector3 spawnPos;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject toughEnemy;
    [SerializeField] GameObject boss;

    public bool BossSpawned = false;

    List<Wave> Waves = new List<Wave>();

    List<TimedEnemy> TimedEnemies = new List<TimedEnemy>();

    int currentWave = 0;
    //int CurrentTimedEnemy { get; set; }

    private static SquareSpawner _squareSpawner = new SquareSpawner();

    public EnemyWavesControler()
    {
        // easy
        //BuildEnemyWaves(0.35f);
        
        // hard
        BuildEnemyWaves(0.7f);
    }

    public void BuildEnemyWaves(float m)
    {
        var timedSpawner = new TimedSpawner();

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 16.5f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 21f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 27f, 0.9f * m, 1));

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 35f, 0.85f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 38f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 43f, 0.9f * m, 1));

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 50f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 53f, 2f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 59f, 2f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 59.8f, 2f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 60f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 62f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 63f, 2f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 66f, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 70f, 2f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 70f, 2f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, 74f, 0.55f * m, 1));

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 80f, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 82f, 0.8f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 82f, 1.75f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, 83f, 0.5f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 85f, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 87f, 0.85f * m, 1));

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 94f, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 97f, 1.8f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, 97f, 0.45f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 97f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 99.5f, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, 100.75f, 1.55f * m, 2));

        var time = 108f;
        m = 0.72f;

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.45f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * m, 1));

        time = 130f;
        m = 0.75f;

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 1, 0.8f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 1, 1.75f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 3, 0.5f * m, 2));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 4, 1f * m, 2));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 5, 0.85f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 5.5f, 1.75f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 7.5f, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 8.5f, 1.8f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 10, 0.55f * m, 2));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 13, 1.1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 14, 1f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16, 1.55f * m, 2));

        if (time >= 150 && BossSpawned == false)
        {
            BossSpawned = true;
            Instantiate(boss, new Vector3(0f, 0f, 15f), Quaternion.identity);
        }

        var t = 24.0f;

        var c = 1.0;

        var r = new System.Random();


        for (var i = 0; i < 10; i++)
        {
            var fast = r.NextDouble() < 0.25 ? true : false;

            time += t;

            t--;

            if (t < 14f)
            {
                t = 14f;
            }

            m += 0.12f;

            if (m > 1.9)
            {
                m = 1.9f;
            }

            if (fast)
            {
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 0.9f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3f, 2f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9f, 2f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 9.8f, 2f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 10f, 0.9f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 12f, 0.9f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 13f, 2f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16f, 1f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 20f, 2f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 24f, 0.55f * m, 1));
            }
            else
            {
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time, 1f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 3, 0.8f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 4, 0.45f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 6, 0.9f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 9, 1f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 12, 1.55f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, time + 13.5f, 0.5f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 15f, 1f * m, 1));
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Fast, time + 16f, 1.75f * m, 1));
            }

            for (var j = 0; j < (int)c; j++)
            {
                TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, time + 16.5f, 0.825f * m, (int)c));
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
        //CheckForWave15();
        //WaveSpawner();
        SpawnTimedEnemies();
    }

    void CheckForWave15()
    {
        if (timerScript.InGameTime >= 350 && BossSpawned == false)
        {
            BossSpawned = true;
            Instantiate(boss, new Vector3(0f, 0f, 4f), Quaternion.identity);
        }
    }

    void SpawnTimedEnemies()
    {
        if (TimedEnemies.Any(o => !o.HasSpawned))
        {
            foreach (var enemy in TimedEnemies.Where(o => !o.HasSpawned && o.StartTime <= timerScript.InGameTime))
            {
                //Debug.Log("Enemy time: " + enemy.StartTime);
                Debug.Log($"Enemy spawned:{TimedEnemies.Count(o => o.HasSpawned) + 1} /{TimedEnemies.Count}");

                SpawnTimedEnemy(enemy);
            }
        }
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

    void SpawnTimedEnemy(TimedEnemy enemy)
    {
        var gameObject = Instantiate<GameObject>(FindPrefabForEnemy(enemy), enemy.Position, Quaternion.identity, enemiesHolder.transform);

        var enemyScript = gameObject.GetComponent<EnemyScript>();

        enemyScript.Speed = enemy.Speed;

        enemy.HasSpawned = true;
    }

    //GameObject FindPrefabForEnemy(Enemy enemy)
    //{
    //    return FindPrefabForEnemyType(enemy.EnemyType);
    //}

    GameObject FindPrefabForEnemy(TimedEnemy enemy)
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
