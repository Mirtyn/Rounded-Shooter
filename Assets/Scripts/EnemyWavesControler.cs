using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Models;

public class EnemyWavesControler : Projectbehaviour
{
    [SerializeField] TimerScript timerScript;

    Vector3 spawnPos;

    float height;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject toughEnemy;

    private static string CasualEnemyDescription = "Casual Enemy";
    private static string FastEnemyDescription = "Fast Enemy";
    private static string ToughEnemyDescription = "Tough Enemy";

    List<Wave> Waves = new List<Wave>();

    int currentWave = 0;

    public EnemyWavesControler()
    {
        var m = 1.55f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 3, 0.25f * m)
                .AddSetting(EnemyType.Fast, 1, 0.50f * m)
                //.AddSetting(EnemyType.Tough, 1, 0.25f * m)
                .AddStartTime(1f)
                .BuildWave());

        m = 1.65f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 4, 0.25f * m)
                .AddSetting(EnemyType.Fast, 1, 0.50f * m)
                .AddSetting(EnemyType.Tough, 1, 0.25f * m)
                .AddStartTime(20f)
                .BuildWave());

        m = 1.75f;

        Waves.Add(new WaveBuilder()
                .AddSetting(EnemyType.Casual, 5, 0.30f * m)
                .AddSetting(EnemyType.Fast, 2, 0.55f * m)
                .AddSetting(EnemyType.Tough, 1, 0.25f * m)
                .AddStartTime(46f)
                .BuildWave());

        m = 1.75f;

        Waves.Add(new WaveBuilder()
                //.AddSetting(EnemyType.Casual, 5, 0.30f * m)
                //.AddSetting(EnemyType.Fast, 2, 0.55f * m)
                .AddSetting(EnemyType.Tough, 3, 0.30f * m)
                .AddStartTime(46f)
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
            Debug.Log("Game over");

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
            }
        }
    }

    Vector3 GenerateRandomVector(Enemy enemy)
    {
        if (enemy.EnemyType == EnemyType.Casual || enemy.EnemyType == EnemyType.Tough)
        {
            height = 0.6f;
        }
        else // if (enemy.EnemyType == EnemyType.Fast)
        {
            height = 0.5f;
        }

        Vector3 v;

        int rArea = Random.Range(0, 4);

        if (rArea == 0)
        {
            v = new Vector3(Random.Range(20f, -10f), height, Random.Range(20f, 10f));
        }
        else if (rArea == 1)
        {
            v = new Vector3(Random.Range(-20f, -10f), height, Random.Range(20f, -10f));
        }
        else if (rArea == 2)
        {
            v = new Vector3(Random.Range(-20f, 10f), height, Random.Range(-20f, -10f));
        }
        else // if (rArea == 3)
        {
            v = new Vector3(Random.Range(20f, 10f), height, Random.Range(-20f, 10f));
        }

        return v;
    }

    void SpawnEnemy(Enemy enemy)
    {
        spawnPos = GenerateRandomVector(enemy);

        var gameObject =  Instantiate<GameObject>(FindGameObjectForEnemy(enemy), spawnPos, Quaternion.identity);

        var enemyScript = gameObject.GetComponent<EnemyScript>();

        enemyScript.Speed = enemy.Speed;
    }

    GameObject FindGameObjectForEnemy(Enemy enemy)
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

    void SpawnWave(Wave wave)
    {
        foreach(var enemy in wave.Enemies)
        {
            SpawnEnemy(enemy);
        }
    }
}
