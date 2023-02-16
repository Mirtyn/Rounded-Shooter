using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Projectbehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] PlayerData playerData;

    public float cooldown = 0f;
    public float maxCooldown;

    void Update()
    {
        switch (playerData.ShootingCooldownLevel)
        {
            case 1:
                maxCooldown = 0.85f;
                break;
            case 2:
                maxCooldown = 0.7f;
                break;
            case 3:
                maxCooldown = 0.55f;
                break;
            case 4:
                maxCooldown = 0.4f;
                break;
        }

        if (Input.GetAxis("Fire1") > 0)
        {            
            if (cooldown <= 0f)
            {
                Instantiate<GameObject>(arrow, this.transform.position + (this.transform.forward * 1.1f), this.transform.rotation);
                cooldown = maxCooldown;
            }

        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
