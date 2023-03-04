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

    [SerializeField] GameObject shopPannel;

    public float EscapePressedCooldown;

    void Start()
    {
        if (Game_Type == GameType.Master)
        {
            Game.PlayerData.ShopOpened = false;
            Game.PlayerData.ArrowSpeedLevel = 4;
            Game.PlayerData.PlayerTurnSpeedLevel = 4;
            Game.PlayerData.ShootingCooldownLevel = 4;
            Game.PlayerData.Bombs = 5;
            Game.PlayerData.MaxBombs = 0;
            Game.PlayerData.HasForceField = false;

            hUDScript.SetBombsOnScreen();

            shopPannel.active = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Game.PlayerData.ShopOpened == true)
            {
                ExitShopButtonPressed();
            }
            else
            {
                hUDScript.ShopButtinPressed();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapePressedCooldown = 0.1f;
            ExitShopButtonPressed();
        }

        if (EscapePressedCooldown > 0)
        {
            EscapePressedCooldown -= Time.deltaTime;
        }

        if (Game.PlayerData.Bombs >= Game.PlayerData.MaxBombs)
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
        Game.PlayerData.ShopOpened = false;
        this.GetComponent<RectTransform>().localPosition = new Vector3(1920, 0, 0);
    }

    public void BuyBombButtonPressed()
    {
        if (goldScript.Gold >= bombCost)
        {
            if (Game.PlayerData.Bombs != Game.PlayerData.MaxBombs)
            {
                goldScript.Gold -= bombCost;
                Game.PlayerData.Bombs++;
                hUDScript.SetBombsOnScreen();
                goldScript.SendGoldToHUD();
            }
        }
    }

    public void UpgradePlayerRotateSpeedButtonPressed()
    {
        if (goldScript.Gold >= rotateSpeedUpgradeCost)
        {
            if (Game.PlayerData.PlayerTurnSpeedLevel < 4)
            {
                goldScript.Gold -= rotateSpeedUpgradeCost;
                Game.PlayerData.PlayerTurnSpeedLevel++;
                playerRotateSpeedUpgradeText.text = "Level " + Game.PlayerData.PlayerTurnSpeedLevel.ToString();
                goldScript.SendGoldToHUD();
                
                switch (Game.PlayerData.PlayerTurnSpeedLevel)
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

                if (Game.PlayerData.PlayerTurnSpeedLevel == 4)
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
            if (Game.PlayerData.ArrowSpeedLevel < 4)
            {
                goldScript.Gold -= arrowSpeedUpgradeCost;
                Game.PlayerData.ArrowSpeedLevel++;
                arrowSpeedUpgradeText.text = "Level " + Game.PlayerData.ArrowSpeedLevel.ToString();
                goldScript.SendGoldToHUD();

                switch (Game.PlayerData.ArrowSpeedLevel)
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

                if (Game.PlayerData.ArrowSpeedLevel == 4)
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
            if (Game.PlayerData.ShootingCooldownLevel < 4)
            {
                goldScript.Gold -= shootingCooldownUpgradeCost;
                Game.PlayerData.ShootingCooldownLevel++;
                shootingCooldownUpgradeText.text = "Level " + Game.PlayerData.ShootingCooldownLevel.ToString();
                goldScript.SendGoldToHUD();

                switch (Game.PlayerData.ShootingCooldownLevel)
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

                if (Game.PlayerData.ShootingCooldownLevel == 4)
                {
                    upgradeShootingCooldownButton.GetComponent<Button>().interactable = false;
                }
            }
        }
    }
}
