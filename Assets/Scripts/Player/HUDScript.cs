using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScript : ProjectBehaviour
{
    [SerializeField] TimerScript timerScript;
    [SerializeField] GoldScript goldScript;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text bombAmountText;
    [SerializeField] GameObject shopPanel;
    //[SerializeField] PlayerData playerData;
    [SerializeField] TMP_Text scoreText;

    void Update()
    {
        SetScoreOnHUD();
        SetTimeOnHUD();
    }

    void SetScoreOnHUD()
    {
        scoreText.text = "Score: " + Game.ScoreManager.CalculateScore(timerScript.InGameTime, goldScript.Gold);
    }

    void SetTimeOnHUD()
    {
        timerText.text = "Time: " + timerScript.InGameTime.ToString("F2") + " sec";
    }

    public void SetGoldOnHUD(int gold)
    {
        goldText.text = "Gold: " + gold.ToString();
    }

    public void ShopButtinPressed()
    {
        Game.PlayerData.ShopOpened = true;
        shopPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
    }

    public void SetBombsOnScreen()
    {
        bombAmountText.text = Game.PlayerData.Bombs.ToString();
    }
}
