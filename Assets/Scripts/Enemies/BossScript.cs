using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript
{
    [SerializeField] PlayerScript playerScript;

    [SerializeField] GameObject eye_1;
    [SerializeField] GameObject eye_2;
    [SerializeField] GameObject body;
    [SerializeField] GameObject hitParticle;
    [SerializeField] GameObject teleportParticle;

    Transform target;
    float turnSpeed = 1f;
    Quaternion rotGoal;
    Vector3 direction;

    float rnd;
    float teleportCooldown;
    float rndCooldown;

    [SerializeField] GameObject bossCasualEnemy;
    [SerializeField] GameObject bossFastEnemy;
    [SerializeField] GameObject bossThoughEnemy;

    float summonCooldown;
    float maxSummonCooldown = 10f;

    float timeAliveScinceSpawn = 1f;

    //float nextUpdateTime = 0f;

    public BossScript()
        : base(0f)
    {
        Description = "Boss";
        HP = 50;
    }

    void Start()
    {
        target = FindObjectOfType<PlayerScript>().transform;
        goldScript = FindObjectOfType<GoldScript>();
        playerScript = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        timeAliveScinceSpawn += Time.deltaTime;

        maxSummonCooldown = 10 - (timeAliveScinceSpawn / 17);

        if (playerScript.IsDead == false)
        {
            direction.x = (target.position.x - transform.position.x);
            direction.z = (target.position.z - transform.position.z);
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);
        }

        if (rndCooldown <= 0)
        {
            rndCooldown = 2.0f;
            BossMovementOptions();
            BossSpawnEnemies();
        }

        if (rndCooldown > 0)
        {
            rndCooldown -= Time.deltaTime;
        }

        if (teleportCooldown > 0)
        {
            teleportCooldown -= Time.deltaTime;
        }

        if (summonCooldown > 0)
        {
            summonCooldown -= Time.deltaTime;
        }
    }

    void BossMovementOptions()
    {
        rnd = Random.Range(0f, 100f);

        if (teleportCooldown <= 0f && rnd <= 50f)
        {
            teleportCooldown = 2f;

            rnd = Random.Range(0f, 100f);

            if (rnd > 0 && rnd <= 12.5)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(0f, 0f, 5f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
            else if (rnd > 12.5 && rnd <= 25)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(5f, 0f, 4f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
            else if (rnd > 25 && rnd <= 37.5)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(6f, 0f, 0f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
            else if (rnd > 37.5 && rnd <= 50)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(5f, 0f, -4f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
            else if (rnd > 50 && rnd <= 62.5)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(0f, 0f, -5f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
            else if (rnd > 62.5 && rnd <= 75)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(-5f, 0f, -4f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
            else if (rnd > 75 && rnd <= 87.5)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(-6f, 0f, 0f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
            else if (rnd > 87.5 && rnd <= 100)
            {
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
                this.transform.position = new Vector3(-5f, 0f, 4f);
                Instantiate(teleportParticle, this.transform.position, Quaternion.identity);
            }
        }

        
    }

    void BossSpawnEnemies()
    {
        rnd = Random.Range(0f, 100f);

        if (summonCooldown <= 0 && rnd <= 50)
        {
            summonCooldown = maxSummonCooldown;

            rnd = Random.Range(0f, 100f);

            Vector3 right = transform.right;
            float offset;

            if (rnd > 0 && rnd <= 40)
            {
                rnd = Random.Range(0f, 100f);

                if (rnd < 50)
                {
                    offset = -3f;
                    Instantiate(bossFastEnemy, transform.position + right * offset, this.transform.rotation);
                }
                else
                {
                    offset = 3f;
                    Instantiate(bossFastEnemy, transform.position + right * offset, this.transform.rotation);
                }
            }
            else if (rnd > 40 && rnd <= 75)
            {
                rnd = Random.Range(0f, 100f);

                if (rnd < 50)
                {
                    offset = -3f;
                    Instantiate(bossCasualEnemy, transform.position + right * offset, this.transform.rotation);
                }
                else
                {
                    offset = 3f;
                    Instantiate(bossCasualEnemy, transform.position + right * offset, this.transform.rotation);
                }
            }
            else if (rnd > 75 && rnd <= 100)
            {
                rnd = Random.Range(0f, 100f);

                if (rnd < 50)
                {
                    offset = -3f;
                    Instantiate(bossThoughEnemy, transform.position + right * offset, this.transform.rotation);
                }
                else
                {
                    offset = 3f;
                    Instantiate(bossThoughEnemy, transform.position + right * offset, this.transform.rotation);
                }
            }
        }
        
        
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Arrow":
                HitByArrow();
                break;
            case "MyPlayer":
                playerScript.Death();
                break;
            default:
                break;

        }
    }

    void HitByArrow()
    {
        HP--;

        if (HP == 0)
        {
            Destroy(gameObject);
            goldScript.AddGold(Description);
            Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
            Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
            Instantiate<GameObject>(hitParticle, this.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate<GameObject>(hitParticle, this.transform.position, Quaternion.identity);
        }

        eye_1.GetComponent<Renderer>().material.color = Color.red;
        eye_2.GetComponent<Renderer>().material.color = Color.red;
        Invoke("TurnWhiteEyes", 0.5f);

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

    void TurnWhiteEyes()
    {
        eye_1.GetComponent<Renderer>().material.color = Color.white;
        eye_2.GetComponent<Renderer>().material.color = Color.white;
    }
}
