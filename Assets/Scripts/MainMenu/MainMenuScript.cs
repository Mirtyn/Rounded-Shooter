using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : ProjectBehaviour
{
    [SerializeField] GameObject startGamePanel;

    public void StartButtonPressed()
    {
        startGamePanel.active = true;
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
