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
                this.GetComponent<CasualEnemyScript>().OnHitByBomb();
                break;
            case "Fast":
                this.GetComponent<FastEnemyScript>().OnHitByBomb();
                break;
            case "Tough":
                this.GetComponent<ToughEnemyScript>().OnHitByBomb();
                break;
            case "Boss":
                break;
        }
    }
}
