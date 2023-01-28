using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : Projectbehaviour
{
    [SerializeField] PlayerData playerData;
    void Update()
    {
        if (Input.GetAxis("Cancel") > 0)
        {
            ExitShopButtonPressed();
        }
    }
    public void ExitShopButtonPressed()
    {
        this.GetComponent<RectTransform>().localPosition = new Vector3(1920, 0, 0);
    }

    public void BuyBombButtonPressed()
    {

    }
}
