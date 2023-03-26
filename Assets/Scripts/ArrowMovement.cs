using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ArrowMovement : ProjectBehaviour
{
    //[SerializeField] PlayerData playerData;

    bool hitEnemy;
    bool move = true;

    float arrowSpeed;
    float arrowDownSpeed;

    //void Start()
    //{
    //    //playerData = FindObjectOfType<PlayerData>();
    //}

    void Update()
    {
        Vector3 translation;

        switch (Game.PlayerData.ArrowSpeedLevel)
        {
            case 1:
                arrowSpeed = 3f;
                break;
            case 2:
                arrowSpeed = 4f;
                break;
            case 3:
                arrowSpeed = 5.5f;
                break;
            case 4:
                arrowSpeed = 7.5f;
                break;
        }

        if (move == true)
        {
            translation = new Vector3(0, 0, arrowSpeed) * Time.deltaTime;
        }
        else
        {
            if (hitEnemy == true)
            {
                arrowDownSpeed = 0f;
            }
            else
            {
                arrowDownSpeed = -0.1f;
            }
            translation = new Vector3(0, arrowDownSpeed, 0) * Time.deltaTime; ;
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
            case "Boss":
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
