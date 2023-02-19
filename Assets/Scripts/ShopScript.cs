using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopScript : Projectbehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] GoldScript goldScript;
    [SerializeField] HUDScript hUDScript;

    [SerializeField] TMP_Text playerRotateSpeedUpgradeText;
    [SerializeField] TMP_Text playerRotateSpeedCostText;

    [SerializeField] TMP_Text arrowSpeedUpgradeText;
    [SerializeField] TMP_Text arrowSpeedCostText;

    [SerializeField] TMP_Text shootingCooldownUpgradeText;
    [SerializeField] TMP_Text shootingCooldownCostText;

    int bombCost = 4;

    [SerializeField] int rotateSpeedUpgradeCost = 4;
    [SerializeField] int arrowSpeedUpgradeCost = 3;
    [SerializeField] int shootingCooldownUpgradeCost = 5;

    [SerializeField] Button buyBombButton;
    [SerializeField] GameObject upgradeArrowSpeedButton;
    [SerializeField] GameObject upgradePlayerTurnSpeedButton;
    [SerializeField] GameObject upgradeShootingCooldownButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerData.ShopOpened == true)
            {
                ExitShopButtonPressed();
            }
            else
            {
                hUDScript.ShopButtinPressed();
            }
        }

        if (Input.GetAxis("Cancel") > 0)
        {
            ExitShopButtonPressed();
        }

        if (playerData.Bombs >= playerData.MaxBombs)
        {
            buyBombButton.interactable = false;
        }
        else
        {
            buyBombButton.interactable = true;
        }
    }
    public void ExitShopButtonPressed()
    {
        playerData.ShopOpened = false;
        this.GetComponent<RectTransform>().localPosition = new Vector3(1920, 0, 0);
    }

    public void BuyBombButtonPressed()
    {
        if (goldScript.Gold >= bombCost)
        {
            if (playerData.Bombs != playerData.MaxBombs)
            {
                goldScript.Gold -= bombCost;
                playerData.Bombs++;
                hUDScript.SetBombsOnScreen();
                goldScript.SendGoldToHUD();
            }
        }
    }

    public void UpgradePlayerRotateSpeedButtonPressed()
    {
        if (goldScript.Gold >= rotateSpeedUpgradeCost)
        {
            if (playerData.PlayerTurnSpeedLevel < 4)
            {
                goldScript.Gold -= rotateSpeedUpgradeCost;
                playerData.PlayerTurnSpeedLevel++;
                playerRotateSpeedUpgradeText.text = "Player Rotate Speed: Level " + playerData.PlayerTurnSpeedLevel.ToString();
                goldScript.SendGoldToHUD();
                
                switch (playerData.PlayerTurnSpeedLevel)
                {
                    case 1:
                        rotateSpeedUpgradeCost = 4;
                        break;
                    case 2:
                        rotateSpeedUpgradeCost = 9;
                        break;
                    case 3:
                        rotateSpeedUpgradeCost = 16;
                        break;
                }

                playerRotateSpeedCostText.text = "Cost: " + rotateSpeedUpgradeCost + " Gold";

                if (playerData.PlayerTurnSpeedLevel == 4)
                {
                    upgradePlayerTurnSpeedButton.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void UpgradeArrowSpeedButtonPressed()
    {
        if (goldScript.Gold >= arrowSpeedUpgradeCost)
        {
            if (playerData.ArrowSpeedLevel < 4)
            {
                goldScript.Gold -= arrowSpeedUpgradeCost;
                playerData.ArrowSpeedLevel++;
                arrowSpeedUpgradeText.text = "Arrow Fly Speed: Level " + playerData.ArrowSpeedLevel.ToString();
                goldScript.SendGoldToHUD();

                switch (playerData.ArrowSpeedLevel)
                {
                    case 1:
                        arrowSpeedUpgradeCost = 3;
                        break;
                    case 2:
                        arrowSpeedUpgradeCost = 7;
                        break;
                    case 3:
                        arrowSpeedUpgradeCost = 18;
                        break;
                }

                arrowSpeedCostText.text = "Cost: " + arrowSpeedUpgradeCost + " Gold";

                if (playerData.ArrowSpeedLevel == 4)
                {
                    upgradeArrowSpeedButton.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void UpgradeShootingCooldownButtonPressed()
    {
        if (goldScript.Gold >= shootingCooldownUpgradeCost)
        {
            if (playerData.ShootingCooldownLevel < 4)
            {
                goldScript.Gold -= shootingCooldownUpgradeCost;
                playerData.ShootingCooldownLevel++;
                shootingCooldownUpgradeText.text = "Shooting Cooldown: Level " + playerData.ShootingCooldownLevel.ToString();
                goldScript.SendGoldToHUD();

                switch (playerData.ShootingCooldownLevel)
                {
                    case 1:
                        shootingCooldownUpgradeCost = 5;
                        break;
                    case 2:
                        shootingCooldownUpgradeCost = 13;
                        break;
                    case 3:
                        shootingCooldownUpgradeCost = 24;
                        break;
                }

                shootingCooldownCostText.text = "Cost: " + shootingCooldownUpgradeCost + " Gold";

                if (playerData.ShootingCooldownLevel == 4)
                {
                    upgradeShootingCooldownButton.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
