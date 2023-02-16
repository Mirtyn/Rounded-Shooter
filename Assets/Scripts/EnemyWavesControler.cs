using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;

public class EnemyWavesControler : Projectbehaviour
{
    [SerializeField] TimerScript timerScript;
    [SerializeField] Transform enemiesHolder;

    Vector3 spawnPos;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject toughEnemy;

    List<Wave> Waves = new List<Wave>();

    int currentWave = 0;

    private static SquareSpawner _squareSpawner = new SquareSpawner();

    public EnemyWavesControler()
    {
        // W1
        var m = 0.9f;

        Waves.Add(new WaveBuilder()
                //.AddSetting(EnemyType.Casual, 1, 0.9f * m, 11.5f, 13.5f)
                .AddSetting(EnemyType.Casual, 1, 0.9f * m, 10f, 12f)
                .AddSetting(EnemyType.Casual, 1, 0.9f * m, 13f, 15f)
                //.AddSetting(EnemyType.Fast, 1, 0.50f * m, 8f, 10f)
                //.AddSetting(EnemyType.Tough, 1, 0.25f * m, 10f, 12f)
                .AddStartTime(1f)
                .BuildWave());

        // W2
        m = 0.9f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 1, 0.9f * m, 10f, 13f)
                .AddSetting(EnemyType.Casual, 2, 0.9f * m, 12f, 15f)
                //.AddSetting(EnemyType.Fast, 1, 2f * m, 8f, 12f)
                //.AddSetting(EnemyType.Tough, 1, 0.25f * m, 8f, 12f)
                .AddStartTime(20f)
                .BuildWave());

        // W3
        m = 0.9f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 1, 0.9f * m, 12f, 16f)
                .AddSetting(EnemyType.Fast, 2, 2f * m, 15f, 20f)
                .AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
                //.AddSetting(EnemyType.Tough, 1, 0.25f * m, 14f, 24f)
                .AddStartTime(46f)
                .BuildWave());

        // W4
        m = 0.9f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 3, 0.9f * m, 12f, 20f)
                .AddSetting(EnemyType.Fast, 2, 2f * m, 12f, 20f)
                //.AddSetting(EnemyType.Tough, 2, 0.25f * m, 16f, 24f)
                .AddStartTime(70f)
                .BuildWave());

        // W5
        m = 0.9f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 2, 0.9f * m, 12f, 20f)
                .AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
                .AddSetting(EnemyType.Tough, 1, 0.75f * m, 16f, 24f)
                .AddStartTime(100f)
                .BuildWave());

        // W6
        m = 1f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 20f)
                .AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
                .AddSetting(EnemyType.Tough, 3, 0.75f * m, 16f, 24f)
                .AddStartTime(134f)
                .BuildWave());

        // W7
        m = 1.05f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 20f)
                .AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
                .AddSetting(EnemyType.Tough, 3, 0.75f * m, 16f, 24f)
                .AddStartTime(170f)
                .BuildWave());

        // W8
        m = 1.1f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 3, 1f * m, 12f, 20f)
                //.AddSetting(EnemyType.Fast, 1, 2f * m, 12f, 20f)
                .AddSetting(EnemyType.Tough, 2, 0.75f * m, 16f, 24f)
                .AddStartTime(200f)
                .BuildWave());

        // W9
        m = 1.15f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 2, 1f * m, 12f, 20f)
                .AddSetting(EnemyType.Fast, 4, 2f * m, 12f, 20f)
                //.AddSetting(EnemyType.Tough, 3, 0.75f * m, 16f, 24f)
                .AddStartTime(236f)
                .BuildWave());

        // W10
        m = 1f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 5, 1f * m, 12f, 20f)
                .AddSetting(EnemyType.Fast, 3, 2f * m, 12f, 20f)
                .AddSetting(EnemyType.Tough, 1, 0.75f * m, 16f, 24f)
                .AddStartTime(280f)
                .BuildWave());
    }

    void Update()
    {
        WaveSpawner();
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

    GameObject FindPrefabForEnemy(Enemy enemy)
    {
        switch(enemy.EnemyType)
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
