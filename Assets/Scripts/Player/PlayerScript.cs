using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Vector3 startPos;

    [SerializeField] GameObject BombExplode;

    [SerializeField] PlayerData playerData;

    float cooldown = 0f;
    void Start()
    {
        startPos = this.transform.position;
    }

    void Update()
    {
        this.transform.position = startPos;

        if (Input.GetAxis("UseBomb") > 0)
        {
            if (playerData.Bombs > 0)
            {
                if (cooldown <= 0f)
                {
                    Instantiate<GameObject>(BombExplode, new Vector3(0f, 0.2f, 0f), Quaternion.identity);
                    playerData.Bombs--;
                    cooldown = 0.2f;
                }
                
            }
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
