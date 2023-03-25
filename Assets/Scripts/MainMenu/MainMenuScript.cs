using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

internal class MainMenuScript : ProjectBehaviour
{
    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject helpPanel;

    public void StartButtonPressed()
    {
        startGamePanel.SetActive(true);
    }

    public void HelpButtonPressed()
    {
        helpPanel.SetActive(true);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
