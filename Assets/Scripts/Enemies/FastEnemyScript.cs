using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : EnemyScript
{
    string FastEnemyDescription = "Fast Enemy";
    [SerializeField] GoldScript goldScript;
    [SerializeField] PlayerScript playerScript;

    [SerializeField] GameObject hitParticle;
    [SerializeField] GameObject deathParticle;

    public int HP = 1;

    Transform target;
    float turnSpeed = 0.5f;
    Quaternion rotGoal;
    Vector3 direction;

    public FastEnemyScript()
        : base(2.0f)
    {
    }

    void Start()
    {
        target = FindObjectOfType<PlayerScript>().transform;
        goldScript = FindObjectOfType<GoldScript>();
        playerScript = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        if (playerScript.IsDead == false)
        {
            Vector3 translation;

            direction.x = (target.position.x - transform.position.x);
            direction.z = (target.position.z - transform.position.z);
            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

            translation = new Vector3(0, 0, Speed) * Time.deltaTime;

            transform.Translate(translation);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Arrow":
                HitByArrow();
                break;
            case "Bomb":
                HitByBomb();
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
        Destroy(gameObject);
        goldScript.AddGold(FastEnemyDescription);
        Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
    }

    public void HitByBomb()
    {
        HP = 0;
        Destroy(gameObject);
        goldScript.AddGold(FastEnemyDescription);
        Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
    }
}
