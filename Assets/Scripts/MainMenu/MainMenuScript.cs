using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

internal class MainMenuScript : ProjectBehaviour
{
    [SerializeField] GameObject startGamePanel;

    public void StartButtonPressed()
    {
        startGamePanel.SetActive( true);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
