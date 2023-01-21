using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyScript : Projectbehaviour
{
    string FastEnemyDescription = "Fast Enemy";
    [SerializeField] GoldScript goldScript;

    [SerializeField] GameObject hitParticle;
    [SerializeField] GameObject deathParticle;

    public int HP = 1;

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

        translation = new Vector3(0, 0, 2f) * Time.deltaTime;

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
        Destroy(gameObject);
        goldScript.AddGold(FastEnemyDescription);
        Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
    }
}
