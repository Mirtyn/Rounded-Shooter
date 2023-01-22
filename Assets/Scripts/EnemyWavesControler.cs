using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;

public class EnemyWavesControler : Projectbehaviour
{
    [SerializeField] TimerScript timerScript;

    Vector3 spawnPos;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject toughEnemy;

    List<Wave> Waves = new List<Wave>();

    int currentWave = 0;

    private static SquareSpawner _squareSpawner = new SquareSpawner();

    public EnemyWavesControler()
    {
        var m = 1.55f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 3, 0.25f * m, 7f, 8f)
                .AddSetting(EnemyType.Fast, 1, 0.50f * m, 8f, 10f)
                //.AddSetting(EnemyType.Tough, 1, 0.25f * m, 10f, 12f)
                .AddStartTime(1f)
                .BuildWave());

        m = 1.65f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 4, 0.25f * m, 8f, 12f)
                .AddSetting(EnemyType.Fast, 1, 0.50f * m, 8f, 12f)
                .AddSetting(EnemyType.Tough, 1, 0.25f * m, 8f, 12f)
                .AddStartTime(10f)
                .BuildWave());

        m = 1.75f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 5, 0.30f * m, 12f, 20f)
                .AddSetting(EnemyType.Fast, 2, 0.55f * m, 12f, 20f)
                .AddSetting(EnemyType.Tough, 1, 0.25f * m, 14f, 24f)
                .AddStartTime(46f)
                .BuildWave());

        m = 1.75f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 5, 0.30f * m, 12f, 20f)
                //.AddSetting(EnemyType.Fast, 2, 0.55f * m, 12f, 20f)
                .AddSetting(EnemyType.Tough, 2, 0.25f * m, 16f, 24f)
                .AddStartTime(68f)
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

        var gameObject =  Instantiate<GameObject>(FindPrefabForEnemy(enemy), spawnPos, Quaternion.identity);

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
