using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Projectbehaviour
{
    [SerializeField] GameObject arrow;

    public float cooldown = 0f;
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {            
            if (cooldown <= 0f)
            {
                Instantiate<GameObject>(arrow, this.transform.position + (this.transform.forward * 1.1f), this.transform.rotation);
                cooldown = 0.7f;
            }

        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
