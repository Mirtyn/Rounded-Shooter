using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BossScript : EnemyScript
{
    private enum Movement
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest,
    }

    [SerializeField] GameObject body;
    [SerializeField] GameObject teleportParticle;

    float rnd;
    float teleportCooldown;
    float rndCooldown;

    [SerializeField] GameObject bossCasualEnemy;
    [SerializeField] GameObject bossFastEnemy;
    [SerializeField] GameObject bossThoughEnemy;

    float summonCooldown;
    
    float minSummonCooldown = 12f;
    float maxSummonCooldown = 18f;

    float minTeleportCooldown = 8f;
    float maxTeleportCooldown = 12f;

    //float timeAliveScinceSpawn = 1f;

    Movement Location = Movement.North;

    //float nextUpdateTime = 0f;

    public BossScript()
        : base(0f)
    {
        Description = "Boss";
        _onHitByArrowRotation = -2f;
    }

    protected override void Start()
    {
        base.Start();

        HP = 12 + (int)(12f * DifficultyModifier);
        rndCooldown = 2f;
        teleportCooldown = 0;// Random.Range(3f * DifficultyModifier, 5.5f * DifficultyModifier);

        Debug.Log($"BossScript.Start() - DifficultyModifier: {DifficultyModifier}  - HP: {HP}");
    }

    protected override void Update()
    {
        base.Update();

        teleportCooldown -= Time.deltaTime;
        summonCooldown -= Time.deltaTime;

        BossMovementOptions();
        BossSpawnEnemies();
    }

    private float InverseDifficultyModifier
    {
        get { return 1f / DifficultyModifier; }
    }

    void BossMovementOptions()
    {
        if (teleportCooldown > 0)
        {
            return;
        }

        teleportCooldown = Random.Range(minTeleportCooldown * InverseDifficultyModifier, maxTeleportCooldown * InverseDifficultyModifier);

        Debug.Log($"teleportCooldown: {teleportCooldown}");

        var location = Location;

        while (location == Location)
        {
            location = (Movement)Random.Range(0, 7);
        }

        Location = location;

        //Debug.Log($"Location: {Location}");

            switch (Location)
            {
                case Movement.North:
                    this.transform.position = new Vector3(0f, 0f, 5f);
                    break;
                case Movement.NorthEast:
                    this.transform.position = new Vector3(5f, 0f, 4f);
                    break;
                case Movement.East:
                    this.transform.position = new Vector3(6f, 0f, 0f);
                    break;
                case Movement.SouthEast:
                    this.transform.position = new Vector3(5f, 0f, -4f);
                    break;
                case Movement.South:
                    this.transform.position = new Vector3(0f, 0f, -5f);
                    break;
                case Movement.SouthWest:
                    this.transform.position = new Vector3(-5f, 0f, -4f);
                    break;
                case Movement.West:
                    this.transform.position = new Vector3(-6f, 0f, 0f);
                    break;
                default:
                    this.transform.position = new Vector3(-5f, 0f, 4f);
                    break;
            }

            Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            RotateTowardsPlayer(1);
            Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
        //}
    }

    void BossSpawnEnemies()
    {
        if (summonCooldown > 0)
        {
            return;
        }

        summonCooldown = Random.Range(minSummonCooldown * InverseDifficultyModifier, maxSummonCooldown * InverseDifficultyModifier);

        Debug.Log($"summonCooldown: {summonCooldown}");

        var enemyTypeChance = Random.Range(0, 9);

        var enemyType = EnemyType.Tough;

        if (enemyTypeChance < 4)
        {
            enemyType = EnemyType.Casual;
        }
        else if (rnd < 8)
        {
            enemyType = EnemyType.Fast;
        }

        var offset = Random.Range(0, 1) == 0 ? - 3f : 3f;

        Game.EnemyManager.Enemies.Add(Game.EnemyManager.Spawner.Build(enemyType, transform.position + transform.right * offset, 0));
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Arrow":
                OnHitByArrow();
                break;
            case "MyPlayer":
                playerScript.OnDeath();
                break;
            default:
                break;

        }
    }

    protected override void OnHitByArrow()
    {
        base.OnHitByArrow();

        switch (HP)
        {
            case 45:
                body.GetComponent<Renderer>().material.color = new Color(0.1f, 0f, 0f);
                break;
            case 40:
                body.GetComponent<Renderer>().material.color = new Color(0.2f, 0f, 0f);
                break;
            case 35:
                body.GetComponent<Renderer>().material.color = new Color(0.3f, 0f, 0f);
                break;
            case 30:
                body.GetComponent<Renderer>().material.color = new Color(0.4f, 0f, 0f);
                break;
            case 25:
                body.GetComponent<Renderer>().material.color = new Color(0.5f, 0f, 0f);
                break;
            case 20:
                body.GetComponent<Renderer>().material.color = new Color(0.6f, 0f, 0f);
                break;
            case 15:
                body.GetComponent<Renderer>().material.color = new Color(0.7f, 0f, 0f);
                break;
            case 10:
                body.GetComponent<Renderer>().material.color = new Color(0.8f, 0f, 0f);
                break;
            case 5:
                body.GetComponent<Renderer>().material.color = new Color(0.9f, 0f, 0f);
                break;
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
        Instantiate<GameObject>(hitParticle, this.transform.position, Quaternion.identity);
    }
}
