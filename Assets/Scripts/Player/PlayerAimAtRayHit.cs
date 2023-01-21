using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimAtRayHit : Projectbehaviour
{
    [SerializeField] Transform target;
    float turnSpeed = 3.00f;
    Quaternion rotGoal;
    Vector3 direction;

    void Update()
    {
        if (y_Clamp == false)
        {
            direction.y = (target.position.y - transform.position.y);
        }
        else
        {
            direction.y = 0;
        }
        direction.x = (target.position.x - transform.position.x);
        direction.z = (target.position.z - transform.position.z);
        rotGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, turnSpeed * Time.deltaTime);
    }
}
