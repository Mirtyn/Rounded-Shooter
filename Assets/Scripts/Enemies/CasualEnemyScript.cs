using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class CasualEnemyScript : EnemyScript
{
    [SerializeField] GameObject body;

    public CasualEnemyScript()
        : base(0.9f)
    {
        Description = "Casual Enemy";
        HP = 3;
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

    protected override void OnHitByArrow()
    {
        base.OnHitByArrow();

        switch (HP)
        {
            case 2:
                body.GetComponent<Renderer>().material.color = new Color(0.4f, 0f, 0f);
                break;
            case 1:
                body.GetComponent<Renderer>().material.color = new Color(0.8f, 0f, 0f);
                break;
        }
    }
}
