using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ToughEnemyScript : EnemyScript
{
    [SerializeField] GameObject body;

    public ToughEnemyScript()
        : base(0.55f)
    {
        Description = "Tough Enemy";
        HP = 6;
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

            //RotateTowardsPlayer(1f);

            //direction.x = (target.position.x - transform.position.x);
            //direction.z = (target.position.z - transform.position.z);
            //rotGoal = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

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
            case 5:
                body.GetComponent<Renderer>().material.color = new Color(0.2f, 0f, 0f);
                break;
            case 4:
                body.GetComponent<Renderer>().material.color = new Color(0.35f, 0f, 0f);
                break;
            case 3:
                body.GetComponent<Renderer>().material.color = new Color(0.5f, 0f, 0f);
                break;
            case 2:
                body.GetComponent<Renderer>().material.color = new Color(0.65f, 0f, 0f);
                break;
            case 1:
                body.GetComponent<Renderer>().material.color = new Color(0.8f, 0f, 0f);
                break;
        }
    }
}
