using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour
{
    public string EnemyType = "Casual";

    public void BombDeath()
    {
        switch (EnemyType)
        {
            case "Casual":
                this.GetComponent<CasualEnemyScript>().HitByBomb();
                break;
            case "Fast":
                this.GetComponent<FastEnemyScript>().HitByBomb();
                break;
            case "Tough":
                this.GetComponent<ToughEnemyScript>().HitByBomb();
                break;
            case "Boss":
                break;
        }
    }
}
