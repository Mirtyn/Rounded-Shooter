using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : ProjectBehaviour
{
    public float Speed = 0.9f;

    public string Description = "Casual Enemy";

    [SerializeField] public GoldScript goldScript;
    [SerializeField] public GameObject deathParticle;

    public int HP = 3;

    [SerializeField] public bool getGold = true;

    GameObject Player;

    public EnemyScript(float speed = 1f)
    {
        Speed = speed;
    }

    public void Start()
    {
        goldScript = FindObjectOfType<GoldScript>();
        Player = GameObject.FindGameObjectWithTag("MyPlayer");
    }

    public void OnDeath()
    {
        HP = 0;

        var enemy = TimedEnemies.SingleOrDefault(o => o.InstanceID == gameObject.GetInstanceID());

        if(enemy != null)
        {
            enemy.IsAlive = false;
            ScoreCalculator.TrackEnemyDeath(enemy, gameObject, Player);
        }

        Destroy(gameObject);

        if (getGold == true)
        {
            goldScript.AddGold(Description);
        }

        Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
    }
}
