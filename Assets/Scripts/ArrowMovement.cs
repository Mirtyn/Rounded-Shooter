using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : Projectbehaviour
{
    bool hitEnemy;
    bool move = true;

    void Update()
    {
        Vector3 translation;

        if (move == true)
        {
            translation = new Vector3(0, 0, 4f) * Time.deltaTime;
        }
        else
        {
            translation = Vector3.zero;
            //translation.x = 0f * Time.deltaTime;
            //translation.y = 0f * Time.deltaTime;
            //translation.z = 0f * Time.deltaTime;
        }

        transform.Translate(translation);
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                hitEnemy = false;
                ArrowHit(hitEnemy, collision);
                break;
            case "Enemy":
                hitEnemy = true;
                ArrowHit(hitEnemy, collision);
                break;
            default:
                break;

        }
    }

    void ArrowHit(bool hitEnemy, Collision collision)
    {
        this.GetComponent<BoxCollider>().enabled = false;
        
        if (hitEnemy == true)
        {
            this.transform.parent = collision.gameObject.transform;
            move = false;
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Destroy>());

        }
        else
        {
            move = false;
        }
    }
}
