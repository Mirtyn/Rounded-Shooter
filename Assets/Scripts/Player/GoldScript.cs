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
        else
        {
            Gold += 2;
        }

        hudScript.SetGoldOnHUD(Gold);
    }
}
