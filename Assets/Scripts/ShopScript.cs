using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopScript : ProjectBehaviour
{
    //[SerializeField] PlayerData PlayerData;
    [SerializeField] GoldScript goldScript;
    [SerializeField] HUDScript hUDScript;

    [SerializeField] TMP_Text playerRotateSpeedUpgradeText;
    [SerializeField] TMP_Text playerRotateSpeedCostText;

    [SerializeField] TMP_Text arrowSpeedUpgradeText;
    [SerializeField] TMP_Text arrowSpeedCostText;

    [SerializeField] TMP_Text shootingCooldownUpgradeText;
    [SerializeField] TMP_Text shootingCooldownCostText;

    int bombCost = 4;

    [SerializeField] int rotateSpeedUpgradeCost = 3;
    [SerializeField] int arrowSpeedUpgradeCost = 2;
    [SerializeField] int shootingCooldownUpgradeCost = 5;

    [SerializeField] Button buyBombButton;
    [SerializeField] GameObject upgradeArrowSpeedButton;
    [SerializeField] GameObject upgradePlayerTurnSpeedButton;
    [SerializeField] GameObject upgradeShootingCooldownButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerData.ShopOpened == true)
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

        if (PlayerData.Bombs >= PlayerData.MaxBombs)
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
        PlayerData.ShopOpened = false;
        this.GetComponent<RectTransform>().localPosition = new Vector3(1920, 0, 0);
    }

    public void BuyBombButtonPressed()
    {
        if (goldScript.Gold >= bombCost)
        {
            if (PlayerData.Bombs != PlayerData.MaxBombs)
            {
                goldScript.Gold -= bombCost;
                PlayerData.Bombs++;
                hUDScript.SetBombsOnScreen();
                goldScript.SendGoldToHUD();
            }
        }
    }

    public void UpgradePlayerRotateSpeedButtonPressed()
    {
        if (goldScript.Gold >= rotateSpeedUpgradeCost)
        {
            if (PlayerData.PlayerTurnSpeedLevel < 4)
            {
                goldScript.Gold -= rotateSpeedUpgradeCost;
                PlayerData.PlayerTurnSpeedLevel++;
                playerRotateSpeedUpgradeText.text = "Player Rotate Speed: Level " + PlayerData.PlayerTurnSpeedLevel.ToString();
                goldScript.SendGoldToHUD();
                
                switch (PlayerData.PlayerTurnSpeedLevel)
                {
                    case 1:
                        rotateSpeedUpgradeCost = 3;
                        break;
                    case 2:
                        rotateSpeedUpgradeCost = 7;
                        break;
                    case 3:
                        rotateSpeedUpgradeCost = 13;
                        break;
                }

                playerRotateSpeedCostText.text = "Cost: " + rotateSpeedUpgradeCost + " Gold";

                if (PlayerData.PlayerTurnSpeedLevel == 4)
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
            if (PlayerData.ArrowSpeedLevel < 4)
            {
                goldScript.Gold -= arrowSpeedUpgradeCost;
                PlayerData.ArrowSpeedLevel++;
                arrowSpeedUpgradeText.text = "Arrow Fly Speed: Level " + PlayerData.ArrowSpeedLevel.ToString();
                goldScript.SendGoldToHUD();

                switch (PlayerData.ArrowSpeedLevel)
                {
                    case 1:
                        arrowSpeedUpgradeCost = 2;
                        break;
                    case 2:
                        arrowSpeedUpgradeCost = 5;
                        break;
                    case 3:
                        arrowSpeedUpgradeCost = 16;
                        break;
                }

                arrowSpeedCostText.text = "Cost: " + arrowSpeedUpgradeCost + " Gold";

                if (PlayerData.ArrowSpeedLevel == 4)
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
            if (PlayerData.ShootingCooldownLevel < 4)
            {
                goldScript.Gold -= shootingCooldownUpgradeCost;
                PlayerData.ShootingCooldownLevel++;
                shootingCooldownUpgradeText.text = "Shooting Cooldown: Level " + PlayerData.ShootingCooldownLevel.ToString();
                goldScript.SendGoldToHUD();

                switch (PlayerData.ShootingCooldownLevel)
                {
                    case 1:
                        shootingCooldownUpgradeCost = 5;
                        break;
                    case 2:
                        shootingCooldownUpgradeCost = 11;
                        break;
                    case 3:
                        shootingCooldownUpgradeCost = 20;
                        break;
                }

                shootingCooldownCostText.text = "Cost: " + shootingCooldownUpgradeCost + " Gold";

                if (PlayerData.ShootingCooldownLevel == 4)
                {
                    upgradeShootingCooldownButton.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
