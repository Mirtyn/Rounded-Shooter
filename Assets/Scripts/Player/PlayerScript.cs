using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public bool IsDead = false;
    Vector3 startPos;

    [SerializeField] GameObject BombExplode;
    [SerializeField] GameObject playerDeathParticle;

    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject enemiesHolder;
    [SerializeField] HUDScript hUDScript;

    public GameObject[] EnemiesOnMap;

    float cooldown = 0f;
    void Start()
    {
        startPos = this.transform.position;
    }

    void Update()
    {
        if (playerData.ShopOpened == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        this.transform.position = startPos;
        if (IsDead == false)
        {
            if (Input.GetAxis("UseBomb") > 0)
            {
                if (playerData.Bombs > 0)
                {
                    if (cooldown <= 0f)
                    {
                        Instantiate<GameObject>(BombExplode, new Vector3(0f, 0.2f, 0f), Quaternion.identity);
                        playerData.Bombs--;
                        hUDScript.SetBombsOnScreen();
                        cooldown = 0.2f;

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

    public void Death()
    {
        IsDead = true;

        Instantiate(playerDeathParticle, new Vector3(this.transform.position.x, 2, this.transform.position.z), Quaternion.identity);
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        Destroy(gameObject.GetComponent<PlayerAimAtRayHit>());
        Destroy(gameObject.GetComponent<PlayerShooting>());
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.transform.GetChild(0).gameObject);
        Destroy(gameObject.transform.GetChild(1).gameObject);
        Destroy(gameObject.transform.GetChild(2).gameObject);
        Invoke("RestartScene", 3f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
