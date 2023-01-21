using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToughEnemyScript : Projectbehaviour
{
    string ToughEnemyDescription = "Tough Enemy";
    [SerializeField] GoldScript goldScript;

    [SerializeField] GameObject eye_1;
    [SerializeField] GameObject eye_2;
    [SerializeField] GameObject body;
    [SerializeField] GameObject hitParticle;
    [SerializeField] GameObject deathParticle;

    public int HP = 6;

    [SerializeField] Transform target;
    float turnSpeed = 0.5f;
    Quaternion rotGoal;
    Vector3 direction;

    void Start()
    {
        goldScript = FindObjectOfType<GoldScript>();
    }

    void Update()
    {
        Vector3 translation;

        direction.x = (target.position.x - transform.position.x);
        direction.z = (target.position.z - transform.position.z);
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed);

        translation = new Vector3(0, 0, 0.55f) * Time.deltaTime;

        transform.Translate(translation);
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Arrow":
                HitByArrow();
                break;
            case "MyPlayer":
                Debug.Log("You died");
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
            goldScript.AddGold(ToughEnemyDescription);
            Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
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

    void TurnWhiteEyes()
    {
        eye_1.GetComponent<Renderer>().material.color = Color.white;
        eye_2.GetComponent<Renderer>().material.color = Color.white;
    }


}
