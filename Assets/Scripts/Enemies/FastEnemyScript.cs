using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : EnemyScript
{
    //[SerializeField] PlayerScript playerScript;

    [SerializeField] GameObject hitParticle;

    public FastEnemyScript()
        : base(2.0f)
    {
        Description = "Fast Enemy"; 
        HP = 1;
    }

    new void Start()
    {
        base.Start();

        //target = FindObjectOfType<PlayerScript>().transform;
        //goldScript = FindObjectOfType<GoldScript>();
        //playerScript = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        if (Game.PlayerData.IsDead == false)
        {
            Vector3 translation;

            //direction.x = (target.position.x - transform.position.x);
            //direction.z = (target.position.z - transform.position.z);
            //rotGoal = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

            RotateTowardsPlayer(1f);

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
        OnDeath();
    }

    public void HitByBomb()
    {
        OnDeath();
    }
}
