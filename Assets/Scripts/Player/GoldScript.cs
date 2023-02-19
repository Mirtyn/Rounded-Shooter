using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    public int Gold;

    [SerializeField] HUDScript hudScript;

    public void AddGold(string typeEnemy)
    {
        if (typeEnemy == "Fast Enemy" || typeEnemy == "Casual Enemy")
        {
            Gold++;
        }
        else if (typeEnemy == "Tough Enemy")
        {
            Gold += 2;
        }
        else if (typeEnemy == "Boss")
        {
            Gold += 20;
        }

        SendGoldToHUD();
    }

    public void SendGoldToHUD()
    {
        hudScript.SetGoldOnHUD(Gold);
    }
}
