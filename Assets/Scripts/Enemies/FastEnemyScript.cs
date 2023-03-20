using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class FastEnemyScript : EnemyScript
{
    public FastEnemyScript()
        : base(2.0f)
    {
        Description = "Fast Enemy"; 
        HP = 1;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Game.PlayerData.IsDead == false)
        {
            Vector3 translation;

            //direction.x = (target.position.x - transform.position.x);
            //direction.z = (target.position.z - transform.position.z);
            //rotGoal = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

            //RotateTowardsPlayer(1f);

            translation = new Vector3(0, 0, Speed) * Time.deltaTime;

            transform.Translate(translation);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Arrow":
                OnHitByArrow();
                break;
            case "Bomb":
                OnHitByBomb();
                break;
            case "MyPlayer":
                playerScript.OnDeath();
                break;
            default:
                break;

        }
    }
}
