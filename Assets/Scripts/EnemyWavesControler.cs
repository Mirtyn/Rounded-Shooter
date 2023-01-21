using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesControler : Projectbehaviour
{
    [SerializeField] TimerScript timerScript;

    Vector3 spawnPos;

    float height;

    [SerializeField] GameObject casualEnemy;
    [SerializeField] GameObject fastEnemy;
    [SerializeField] GameObject thoughEnemy;

    private static string CasualEnemyDescription = "Casual Enemy";
    private static string FastEnemyDescription = "Fast Enemy";
    private static string ToughEnemyDescription = "Tough Enemy";

    //bool spawnedExampleW = false;
    bool spawnedW1 = false;
    bool spawnedW2 = false;
    bool spawnedW3 = false;
    bool spawnedW4 = false;
    bool spawnedW5 = false;

    void Update()
    {
        WaveSpawner();
    }

    void WaveSpawner()
    {
        // W1
        if (timerScript.InGameTime >= 1f && spawnedW1 == false)
        {
            SpawnWave1();
        }

        // W2
        if (timerScript.InGameTime >= 20f && spawnedW2 == false)
        {
            SpawnWave2();
        }

        // W3
        if (timerScript.InGameTime >= 46f && spawnedW3 == false)
        {
            SpawnWave3();
        }

        // W4
        if (timerScript.InGameTime >= 72f && spawnedW4 == false)
        {
            SpawnWave4();
        }

        // W5
        if (timerScript.InGameTime >= 112f && spawnedW5 == false)
        {
            SpawnWave5();
        }
    }

    Vector3 GenerateRandomVector(string typeEnemy)
    {
        if (typeEnemy == CasualEnemyDescription || typeEnemy == ToughEnemyDescription)
        {
            height = 0.6f;
        }
        else // if (typeEnemy == FastEnemyDescripotion)
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

    //void SpawnCasualEnemy()
    //{
    //    string typeEnemy = "Casual Enemy";

    //    GenerateRandomVector(typeEnemy);

    //    Instantiate<GameObject>(casualEnemy, spawnPos, Quaternion.identity);
    //}

    //void SpawnFastEnemy()
    //{
    //    string typeEnemy = "Fast Enemy";

    //    GenerateRandomVector(typeEnemy);

    //    Instantiate<GameObject>(fastEnemy, spawnPos, Quaternion.identity);
    //}

    //void SpawnToughEnemy()
    //{
    //    string typeEnemy = "Tough Enemy";

    //    GenerateRandomVector(typeEnemy);

    //    Instantiate<GameObject>(thoughEnemy, spawnPos, Quaternion.identity);
    //}

    void SpawnEnemy(string typeEnemy, GameObject enemmy)
    {
        spawnPos = GenerateRandomVector(typeEnemy);

        Instantiate<GameObject>(enemmy, spawnPos, Quaternion.identity);
    }

    //void ExampleWave1()
    //{
    //    spawnedExampleW = true;

    //    for (var i = 0; i < 2; i++)
    //    {
    //        SpawnEnemy(CasualEnemyDescription, casualEnemy);
    //    }

    //    for (var i = 0; i < 2; i++)
    //    {
    //        SpawnEnemy(FastEnemyDescription, fastEnemy);
    //    }

    //    for (var i = 0; i < 2; i++)
    //    {
    //        SpawnEnemy(ToughEnemyDescription, thoughEnemy);
    //    }
    //}

    void SpawnWave1()
    {
        spawnedW1 = true;

        for (var i = 0; i < 3; i++)
        {
            SpawnEnemy(CasualEnemyDescription, casualEnemy);
        }
    }

    void SpawnWave2()
    {
        spawnedW2 = true;

        for (var i = 0; i < 5; i++)
        {
            SpawnEnemy(CasualEnemyDescription, casualEnemy);
        }
    }

    void SpawnWave3()
    {
        spawnedW3 = true;

        for (var i = 0; i < 3; i++)
        {
            SpawnEnemy(FastEnemyDescription, fastEnemy);
        }

        SpawnEnemy(CasualEnemyDescription, casualEnemy);
    }

    void SpawnWave4()
    {
        spawnedW4 = true;

        for (var i = 0; i < 4; i++)
        {
            SpawnEnemy(CasualEnemyDescription, casualEnemy);
        }

        for (var i = 0; i < 2; i++)
        {
            SpawnEnemy(FastEnemyDescription, fastEnemy);
        }
    }

    void SpawnWave5()
    {
        spawnedW5 = true;

        SpawnEnemy(FastEnemyDescription, fastEnemy);

        for (var i = 0; i < 3; i++)
        {
            SpawnEnemy(CasualEnemyDescription, casualEnemy);
        }

        for (var i = 0; i < 2; i++)
        {
            SpawnEnemy(ToughEnemyDescription, thoughEnemy);
        }
    }
}
