using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDScript : Projectbehaviour
{
    [SerializeField] TimerScript timerScript;

    [SerializeField] GoldScript goldScript;

    [SerializeField] TMP_Text timerText;

    [SerializeField] TMP_Text goldText;
    void Update()
    {
        SetTimeOnHUD();
    }

    void SetTimeOnHUD()
    {
        timerText.text = "Time: " + timerScript.InGameTime.ToString("F3") + " sec";
    }

    public void SetGoldOnHUD(int gold)
    {
        goldText.text = "Gold: " + gold.ToString();
    }
}
