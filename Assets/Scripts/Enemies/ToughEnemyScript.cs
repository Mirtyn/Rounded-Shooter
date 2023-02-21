using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToughEnemyScript : EnemyScript
{
    [SerializeField] PlayerScript playerScript;

    [SerializeField] GameObject body;
    [SerializeField] GameObject hitParticle;

    Transform target;
    float turnSpeed = 0.5f;
    Quaternion rotGoal;
    Vector3 direction;

    public ToughEnemyScript()
        : base(0.55f)
    {
        Description = "Tough Enemy";
        HP = 6;
    }

    new void Start()
    {
        base.Start();

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
        HP--;

        if (HP == 0)
        {
            OnDeath();
        }
        else
        {
            Instantiate<GameObject>(hitParticle, this.transform.position, Quaternion.identity);
        }

        TurnEyesRed();

        Invoke("TurnWhiteEyes", 0.5f);

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

    public void HitByBomb()
    {
        OnDeath();
    }
}
