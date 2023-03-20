using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerAimAtRayHit : ProjectBehaviour
{
    //[SerializeField] PlayerData playerData;

    [SerializeField] Transform target;
    float turnSpeed;
    Quaternion rotGoal;
    Vector3 direction;

    void Update()
    {
        switch (Game.PlayerData.PlayerTurnSpeedLevel)
        {
            case 1:
                turnSpeed = 2f;
                break;
            case 2:
                turnSpeed = 2.75f;
                break;
            case 3:
                turnSpeed = 3.75f;
                break;
            case 4:
                turnSpeed = 6f;
                break;
        }

        if (Game.ClampY == false)
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
