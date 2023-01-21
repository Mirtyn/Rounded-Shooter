using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : Projectbehaviour
{
    public float Speed = 0.9f;

    public EnemyScript(float speed = 1f)
    {
        Speed = speed;
    }
}
