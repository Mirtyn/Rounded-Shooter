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

    //float rnd;
    float teleportCooldown;
    //float rndCooldown;

    [SerializeField] GameObject bossCasualEnemy;
    [SerializeField] GameObject bossFastEnemy;
    [SerializeField] GameObject bossThoughEnemy;

    float summonCooldown;

    const float AbsoluteMinSummonCooldown = 4f;
    const float AbsoluteMaxSummonCooldown = 8f;
    const float SummonCooldownDecrement = 0.125f;

    //const float AbsoluteMinSummonCount = 1f;
    const float AbsoluteMaxSummonCount = 8f;
    const float SummonCountIncrement = 0.125f;

    float minSummonCooldown = 8f;
    float maxSummonCooldown = 12f;
    float summonCount = 1f;

    const float AbsoluteMinTeleportCooldown = 3f;
    const float AbsoluteMaxTeleportCooldown = 6f;
    const float TeleportCooldownDecrement = 0.125f;

    float minTeleportCooldown = 4f;
    float maxTeleportCooldown = 8f;
    //float minTeleportCooldown = 2f;
    //float maxTeleportCooldown = 2f;

    public bool DisableSummon = false;

    //float timeAliveScinceSpawn = 1f;

    Movement Location = Movement.North;

    //float nextUpdateTime = 0f;

    public float DifficultyModifier { get; internal set; } = 1f;

    public int StartHP
    {
        get;
        set;
    }

    public BossScript()
        : base(0f)
    {
        Description = "Boss";
        _onHitByArrowRotation = -2f;
    }

    protected override void Start()
    {
        base.Start();

        //HP = 24 + (int)(24f * BossDifficultyModifier);

        //_startHP = HP;

        teleportCooldown = 0;// Random.Range(3f * DifficultyModifier, 5.5f * DifficultyModifier);
        RandomSummonCooldown();

        Debug.Log($"BossScript.Start() - DifficultyModifier: {DifficultyModifier}  - HP: {HP}");
    }

    protected override void Update()
    {
        base.Update();

        teleportCooldown -= Time.deltaTime;
        summonCooldown -= Time.deltaTime;

        BossTeleport();
        BossSpawnEnemies();
    }

    private float InverseDifficultyModifier
    {
        get { return 1f / DifficultyModifier; }
    }

    void BossTeleport()
    {
        if (teleportCooldown > 0)
        {
            return;
        }

        RandomTeleportCooldown();

        //try
        //{
            Instantiate(teleportParticle, this.transform.position, Quaternion.identity);

            var location = Location;

            while (location == Location)
            {
                location = (Movement)Random.Range(0, 7);
            }

            Location = location;

            //Debug.Log($"New Location: {Location}");

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

            //Debug.Log($"transform.position: {transform.position.x}, {transform.position.y}, {transform.position.z}");

            RotateTowardsPlayer(1);

            Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
        //}
        //catch (System.Exception x)
        //{
        //    Debug.Log($"Exception: {x.Message}");
        //}
    }

    private void RandomTeleportCooldown()
    {
        teleportCooldown = Random.Range(minTeleportCooldown * InverseDifficultyModifier, maxTeleportCooldown * InverseDifficultyModifier);

        var s = (int)minTeleportCooldown;
        var t = (int)maxTeleportCooldown;

        minTeleportCooldown = Mathf.Max(minTeleportCooldown - TeleportCooldownDecrement, AbsoluteMinTeleportCooldown);
        maxTeleportCooldown = Mathf.Max(maxTeleportCooldown - TeleportCooldownDecrement, AbsoluteMaxTeleportCooldown);

        if (s != (int)minTeleportCooldown)
        {
            Debug.Log($"minTeleportCooldown: {minTeleportCooldown}");
        }

        if (t != (int)maxTeleportCooldown)
        {
            Debug.Log($"maxTeleportCooldown: {maxTeleportCooldown}");
        }
    }

    private void RandomSummonCooldown()
    {
        summonCooldown = Random.Range(minSummonCooldown * InverseDifficultyModifier, maxSummonCooldown * InverseDifficultyModifier);

        minSummonCooldown = Mathf.Max(minSummonCooldown - SummonCooldownDecrement, AbsoluteMinSummonCooldown);
        maxSummonCooldown = Mathf.Max(maxSummonCooldown - SummonCooldownDecrement, AbsoluteMaxSummonCooldown);

        summonCount = Mathf.Min(summonCount + SummonCountIncrement, AbsoluteMaxSummonCount);

        Debug.Log($"summonCount: {summonCount}");
    }

    void BossSpawnEnemies()
    {
        if (summonCooldown > 0 || DisableSummon)
        {
            return;
        }

        RandomSummonCooldown();

        var left = Random.Range(0, 1) == 0;

        for (var i = 0; i < (int)summonCount; i++)
        {
            BossSpawnEnemy(Game.TimerScript.InGameTime + (i * InverseDifficultyModifier), left);

            left = !left;
        }
    }

    void BossSpawnEnemy(float starttime, bool left)
    {
        var enemyTypeChance = Random.Range(0, 9);

        var enemyType = EnemyType.Tough;

        if (enemyTypeChance < 5)
        {
            enemyType = EnemyType.Casual;
        }
        else if (enemyTypeChance < 8)
        {
            enemyType = EnemyType.Fast;
        }

        var offset = left ? -3f : 3f;

        Game.EnemyManager.Enemies.Add(Game.EnemyManager.Spawner.BuildEnemy(enemyType, transform.position + transform.right * offset, starttime));
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

        var p = 1f - (float)HP / (float)StartHP;

        body.GetComponent<Renderer>().material.color = new Color(p, 0f, 0f);

        //switch (HP)
        //{
        //    case startHP * :
        //        body.GetComponent<Renderer>().material.color = new Color(0.1f, 0f, 0f);
        //        break;
        //    case 40:
        //        body.GetComponent<Renderer>().material.color = new Color(0.2f, 0f, 0f);
        //        break;
        //    case 35:
        //        body.GetComponent<Renderer>().material.color = new Color(0.3f, 0f, 0f);
        //        break;
        //    case 30:
        //        body.GetComponent<Renderer>().material.color = new Color(0.4f, 0f, 0f);
        //        break;
        //    case 25:
        //        body.GetComponent<Renderer>().material.color = new Color(0.5f, 0f, 0f);
        //        break;
        //    case 20:
        //        body.GetComponent<Renderer>().material.color = new Color(0.6f, 0f, 0f);
        //        break;
        //    case 15:
        //        body.GetComponent<Renderer>().material.color = new Color(0.7f, 0f, 0f);
        //        break;
        //    case 10:
        //        body.GetComponent<Renderer>().material.color = new Color(0.8f, 0f, 0f);
        //        break;
        //    case 5:
        //        body.GetComponent<Renderer>().material.color = new Color(0.9f, 0f, 0f);
        //        break;
        //}
    }

    protected override void OnDeath()
    {
        base.OnDeath();

        Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
        Instantiate<GameObject>(hitParticle, this.transform.position, Quaternion.identity);
    }
}
