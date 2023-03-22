using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class EnemyScript : AnimatedTransform
{
    public float Speed = 0.9f;

    public string Description = "Casual Enemy";

    [SerializeField] protected GoldScript goldScript;
    [SerializeField] protected GameObject deathParticle;
    [SerializeField] protected GameObject eye_1;
    [SerializeField] protected GameObject eye_2;
    [SerializeField] protected PlayerScript playerScript;
    [SerializeField] protected GameObject hitParticle;

    protected float _onHitByArrowRotation = -3f;

    private float _initialY;

    Transform target;
    //float turnSpeed = 1f;
    //Quaternion rotGoal;
    Vector3 direction;

    public int HP = 3;

    [SerializeField] public bool getGold = true;

    GameObject Player;

    public float DifficultyModifier { get; internal set; } = 1f;

    public EnemyScript(float speed = 1f)
    {
        Speed = speed;
    }

    protected virtual void Start()
    {
        goldScript = FindObjectOfType<GoldScript>();
        Player = GameObject.FindGameObjectWithTag("MyPlayer");
        playerScript = FindObjectOfType<PlayerScript>();
        target = playerScript.transform;

        _initialY = transform.position.y;

        RotateTowardsPlayer(1);
    }

    protected override void Update()
    {
        base.Update();

        transform.position = new Vector3(transform.position.x, _initialY, transform.position.z);
    }

    protected void RotateTowardsPlayer(float time)
    {
        //Debug.Log($"RotateTowardsPlayer: {time}");

        direction.x = (target.position.x - transform.position.x);
        direction.z = (target.position.z - transform.position.z);

        //rotGoal = Quaternion.LookRotation(direction);

        //transform.rotation = Quaternion.Lerp(transform.rotation, rotGoal, time);
        transform.rotation = Quaternion.LookRotation(direction);

        //Debug.Log($"transform.localEulerAngles: {transform.localEulerAngles.x}, {transform.localEulerAngles.y}, {transform.localEulerAngles.z}");
    }

    protected virtual void OnHitByArrow()
    {
        HP--;

        if (HP == 0)
        {
            OnDeath();

            return;
        }
        else
        {
            RotateAddReturn(_onHitByArrowRotation, 0, 0, 0.05f, 0.20f);

            Instantiate<GameObject>(hitParticle, this.transform.position, Quaternion.identity);
        }

        TurnEyesRed();

        Invoke("TurnWhiteEyes", 0.5f);
    }

    public virtual void OnHitByBomb()
    {
        OnDeath();
    }

    protected virtual void OnDeath()
    {
        HP = 0;

        var enemy = Game.EnemyManager.Enemies.SingleOrDefault(o => o.InstanceID == gameObject.GetInstanceID());

        //if (enemy == null)
        //{
        //    var t = 0;
        //}

        if (enemy != null)
        {
            enemy.IsAlive = false;
            Game.ScoreManager.TrackKillScore(enemy, gameObject, Player, Game.GameType);
        }

        Destroy(gameObject);

        if (getGold == true)
        {
            goldScript.AddGold(Description);
        }

        Instantiate<GameObject>(deathParticle, this.transform.position, Quaternion.identity);
    }

    protected void TurnWhiteEyes()
    {
        eye_1.GetComponent<Renderer>().material.color = Color.white;
        eye_2.GetComponent<Renderer>().material.color = Color.white;
    }

    protected void TurnEyesRed()
    {
        eye_1.GetComponent<Renderer>().material.color = Color.red;
        eye_2.GetComponent<Renderer>().material.color = Color.red;
    }
}
