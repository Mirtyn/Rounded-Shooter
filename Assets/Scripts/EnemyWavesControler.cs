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
        var m = 0.7f;

        var timedSpawner = new TimedSpawner();

        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 12.0f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 14.8f, 0.9f * m, 1));
        TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 18.6f, 0.9f * m, 1));

        //TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 18.0f, 0.9f * m, 1));
        //TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 19.3f, 0.9f * m, 1));
        //TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Casual, 20.0f, 0.9f * m, 1));
        //TimedEnemies.AddRange(timedSpawner.Build(EnemyType.Tough, 19f, 0.9f * m, 1));

        //// W1
        //m = 0.7f;

        //Waves.Add(new WaveBuilder()
        //        //.AddSetting(EnemyType.Casual, 1, 0.9f * m, 11.5f, 13.5f)
        //        .AddSetting(EnemyType.Casual, 1, 0.9f * m, 10f, 12f)
        //        .AddSetting(EnemyType.Casual, 1, 0.9f * m, 14f, 15f)
        //        //.AddSetting(EnemyType.Fast, 1, 0.50f * m, 8f, 10f)
        //        //.AddSetting(EnemyType.Tough, 1, 0.25f * m, 10f, 12f)
        //        .AddStartTime(1f)
        //        .BuildWave());

        //// W2
        //m = 0.7f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 1, 0.9f * m, 11f, 12f)
        //        .AddSetting(EnemyType.Casual, 2, 0.9f * m, 15f, 18f)
        //        //.AddSetting(EnemyType.Fast, 1, 2f * m, 8f, 12f)
        //        //.AddSetting(EnemyType.Tough, 1, 0.25f * m, 8f, 12f)
        //        .AddStartTime(18f)
        //        .BuildWave());

        //// W3
        //m = 0.7f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 1, 0.9f * m, 11.5f, 12f)
        //        .AddSetting(EnemyType.Fast, 2, 2f * m, 19f, 23f)
        //        .AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
        //        //.AddSetting(EnemyType.Tough, 1, 0.25f * m, 14f, 24f)
        //        .AddStartTime(38f)
        //        .BuildWave());

        //// W4
        //m = 0.7f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 3, 0.9f * m, 12f, 25f)
        //        .AddSetting(EnemyType.Fast, 2, 2f * m, 12f, 30f)
        //        //.AddSetting(EnemyType.Tough, 2, 0.25f * m, 16f, 24f)
        //        .AddStartTime(60f)
        //        .BuildWave());

        //// W5
        //m = 0.7f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 0.9f * m, 12f, 25f)
        //        .AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 30f)
        //        .AddSetting(EnemyType.Tough, 1, 0.75f * m, 16f, 24f)
        //        .AddStartTime(80f)
        //        .BuildWave());

        //// W6
        //m = 0.73f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 30f)
        //        .AddSetting(EnemyType.Fast, 1, 2f * m, 18f, 20f)
        //        .AddSetting(EnemyType.Tough, 2, 0.75f * m, 22f, 24f)
        //        .AddSetting(EnemyType.Tough, 1, 0.75f * m, 11f, 18f)
        //        .AddStartTime(100f)
        //        .BuildWave());

        //// W7
        //m = 0.76f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Tough, 3, 0.75f * m, 16f, 35f)
        //        .AddStartTime(127f)
        //        .BuildWave());

        //// W8
        //m = 0.79f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 3, 1f * m, 12f, 28f)
        //        //.AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Tough, 2, 0.75f * m, 16f, 34f)
        //        .AddStartTime(152f)
        //        .BuildWave());

        //// W9
        //m = 0.82f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 34f)
        //        .AddSetting(EnemyType.Fast, 4, 2f * m, 18f, 36f)
        //        //.AddSetting(EnemyType.Tough, 3, 0.75f * m, 16f, 24f)
        //        .AddStartTime(178f)
        //        .BuildWave());

        //// W10
        //m = 0.85f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 5, 1f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Fast, 3, 2f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Tough, 1, 0.75f * m, 12f, 24f)
        //        .AddStartTime(205f)
        //        .BuildWave());

        //// W11
        //m = 0.88f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 4, 1f * m, 12f, 35f)
        //        .AddSetting(EnemyType.Fast, 2, 2f * m, 12f, 25f)
        //        .AddSetting(EnemyType.Tough, 2, 0.75f * m, 12f, 30f)
        //        .AddStartTime(230f)
        //        .BuildWave());

        //// W12
        //m = 0.91f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Fast, 6, 2f * m, 12f, 38f)
        //        .AddSetting(EnemyType.Tough, 1, 0.75f * m, 12f, 24f)
        //        .AddStartTime(260f)
        //        .BuildWave());

        //// W13
        //m = 0.94f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 7, 1f * m, 12f, 25f)
        //        //.AddSetting(EnemyType.Fast, 3, 2f * m, 12f, 20f)
        //        //.AddSetting(EnemyType.Tough, 1, 0.75f * m, 12f, 24f)
        //        .AddStartTime(290f)
        //        .BuildWave());

        //// W14
        //m = 0.97f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 14f, 14.1f)
        //        .AddSetting(EnemyType.Casual, 3, 1f * m, 12f, 26f)
        //        .AddSetting(EnemyType.Fast, 3, 2f * m, 20f, 32f)
        //        .AddSetting(EnemyType.Tough, 2, 0.7f * m, 12f, 38f)
        //        .AddStartTime(320f)
        //        .BuildWave());

        //// W15   //BOSS
        //m = 1f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 1, 1f * m, 14f, 14.1f)
        //        .AddSetting(EnemyType.Fast, 4, 2f * m, 12f, 40f)
        //        //.AddSetting(EnemyType.Tough, 1, 0.7f * m, 12f, 24f)
        //        .AddStartTime(350f)
        //        .BuildWave());

        //// W16
        //m = 1.04f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 5, 1f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Fast, 3, 2f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Tough, 1, 0.75f * m, 12f, 24f)
        //        .AddStartTime(375f)
        //        .BuildWave());

        //// W17
        //m = 1.08f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 4, 1f * m, 12f, 35f)
        //        .AddSetting(EnemyType.Fast, 2, 2f * m, 12f, 25f)
        //        .AddSetting(EnemyType.Tough, 2, 0.75f * m, 12f, 30f)
        //        .AddStartTime(400f)
        //        .BuildWave());

        //// W18
        //m = 1.12f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 20f)
        //        .AddSetting(EnemyType.Fast, 6, 2f * m, 12f, 38f)
        //        .AddSetting(EnemyType.Tough, 1, 0.75f * m, 12f, 24f)
        //        .AddStartTime(425f)
        //        .BuildWave());

        //// W19
        //m = 1.16f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 7, 1f * m, 12f, 25f)
        //        //.AddSetting(EnemyType.Fast, 3, 2f * m, 12f, 20f)
        //        //.AddSetting(EnemyType.Tough, 1, 0.75f * m, 12f, 24f)
        //        .AddStartTime(450f)
        //        .BuildWave());

        //// W20
        //m = 1.20f;

        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 14f, 14.1f)
        //        .AddSetting(EnemyType.Casual, 3, 1f * m, 12f, 26f)
        //        .AddSetting(EnemyType.Fast, 3, 2f * m, 20f, 32f)
        //        .AddSetting(EnemyType.Tough, 2, 0.7f * m, 12f, 38f)
        //        .AddStartTime(475f)
        //        .BuildWave());

        //// W21
        //m = 1.25f;
        //Waves.Add(new WaveBuilder()
        //        .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 18f)
        //        .AddSetting(EnemyType.Casual, 5, 1f * m, 12f, 40f)
        //        .AddSetting(EnemyType.Fast, 5, 2f * m, 12f, 40f)
        //        .AddSetting(EnemyType.Tough, 2, 0.7f * m, 12f, 40f)
        //        .AddStartTime(500f)
        //        .BuildWave());
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
                Debug.Log("Enemy time: " + enemy.StartTime);

                SpawnTimedEnemy(enemy);
            }
        }
    }

    void WaveSpawner()
    {
        if(currentWave == Waves.Count)
        {
            return;
        }

        var wave = Waves[currentWave];

        if(wave.WaveStartTime <= timerScript.InGameTime)
        {
            Debug.Log("Wave: " + currentWave + " time: " + timerScript.InGameTime);

            SpawnWave(wave);

            if (currentWave < Waves.Count)
            {
                currentWave++;

                if (currentWave == Waves.Count)
                {
                    Debug.Log("Waves finished");
                }
            }
        }
    }

    void SpawnWave(Wave wave)
    {
        foreach (var enemy in wave.Enemies)
        {
            SpawnEnemy(enemy);
        }
    }

    void SpawnEnemy(Enemy enemy)
    {
        spawnPos = Projectbehaviour.UseRadiusSpawner ? new RadiusSpawner(enemy.MinRadius, enemy.MaxRadius).RandomPosition() : _squareSpawner.RandomPosition(enemy);

        var gameObject =  Instantiate<GameObject>(FindPrefabForEnemy(enemy), spawnPos, Quaternion.identity, enemiesHolder.transform);

        var enemyScript = gameObject.GetComponent<EnemyScript>();

        enemyScript.Speed = enemy.Speed;
    }

    void SpawnTimedEnemy(TimedEnemy enemy)
    {
        var gameObject = Instantiate<GameObject>(FindPrefabForEnemy(enemy), enemy.Position, Quaternion.identity, enemiesHolder.transform);

        var enemyScript = gameObject.GetComponent<EnemyScript>();

        enemyScript.Speed = enemy.Speed;

        enemy.HasSpawned = true;
    }

    GameObject FindPrefabForEnemy(Enemy enemy)
    {
        return FindPrefabForEnemyType(enemy.EnemyType);
    }

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
