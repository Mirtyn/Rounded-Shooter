using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : ProjectBehaviour
{
    Vector3 startPos;

    [SerializeField] GameObject BombExplode;
    [SerializeField] GameObject playerDeathParticle;
    [SerializeField] GameObject pausePanel;

    //[SerializeField] PlayerData playerData;
    [SerializeField] GameObject enemiesHolder;
    [SerializeField] HUDScript hUDScript;
    [SerializeField] TimerScript timerScript;
    [SerializeField] ShopScript shopScript;

    public float GameSpeed = 1f;

    public GameObject[] EnemiesOnMap;

    float cooldown = 0f;

    //float escapeCooldown = 0f;

    [SerializeField] GameObject deathPanel;

    void Start()
    {
        Time.timeScale = GameSpeed;
        startPos = this.transform.position;
    }

    void Update()
    {
        if (!Game.IsPaused)
        {
            Time.timeScale = GameSpeed;
        }
        
        if (Game.PlayerData.ShopOpened == false)
        {
            if (shopScript.EscapePressedCooldown <= 0 || Game.IsPaused == true)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (Game.IsPaused == false)
                    {
                        PauseGame();
                        shopScript.EscapePressedCooldown = 0f;
                    }
                    else
                    {
                        ResumeGame();
                    }
                }
            }
        }

        //if (escapeCooldown > 0)
        //{
        //    escapeCooldown -= Time.deltaTime;
        //}

        this.transform.position = startPos;
        if (Game.PlayerData.IsDead == false)
        {
            if (Input.GetAxis("UseBomb") > 0)
            {
                if (Game.PlayerData.Bombs > 0)
                {
                    if (cooldown <= 0f)
                    {
                        Instantiate<GameObject>(BombExplode, new Vector3(0f, 0.2f, 0f), Quaternion.identity);
                        Game.PlayerData.Bombs--;
                        hUDScript.SetBombsOnScreen();
                        cooldown = 0.5f;

                        EnemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy");

                        int o = 0;
                        foreach (GameObject i in EnemiesOnMap)
                        {
                            if (Vector3.Distance(EnemiesOnMap[o].transform.position, this.transform.position) <= 4f)
                            {
                                EnemiesOnMap[o].GetComponent<BaseEnemyScript>().BombDeath();
                            }
                            o++;
                        }
                    }
                }
            }

            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    public bool IsDead()
    {
        return Game.PlayerData.IsDead;
    }

    public bool IsDead(bool dead)
    {
        Game.PlayerData.IsDead = dead;

        return IsDead();
    }

    public void Death()
    {
        IsDead(true);
        Instantiate(playerDeathParticle, new Vector3(this.transform.position.x, 2, this.transform.position.z), Quaternion.identity);
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        Destroy(gameObject.GetComponent<PlayerAimAtRayHit>());
        Destroy(gameObject.GetComponent<PlayerShooting>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Destroy(gameObject.transform.GetChild(1).gameObject);
        Destroy(gameObject.transform.GetChild(2).gameObject);

        deathPanel.SetActive(true);

        timerScript.KeepTrackOfTime = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Game.IsPaused = true;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Game.IsPaused = false;
        pausePanel.SetActive(false);
    }
}
