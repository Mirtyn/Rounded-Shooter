using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HelpMenuScript : MonoBehaviour
{
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject enemiesPanel;
    [SerializeField] GameObject gameMechanicsPanel;
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject difficultyPanel;
    [SerializeField] GameObject backToHelpMenuButton;

    public void ShopButton()
    {
        shopPanel.SetActive(true);
        backToHelpMenuButton.SetActive(true);
    }

    public void EnemiesButton()
    {
        enemiesPanel.SetActive(true);
        backToHelpMenuButton.SetActive(true);
    }

    public void GameMechanicsButton()
    {
        gameMechanicsPanel.SetActive(true);
        backToHelpMenuButton.SetActive(true);
    }

    public void DifficultyButton()
    {
        difficultyPanel.SetActive(true);
        backToHelpMenuButton.SetActive(true);
    }

    public void BackButton()
    {
        helpPanel.SetActive(false);
    }

    public void BackToOptionsButton()
    {
        backToHelpMenuButton.SetActive(false);
        shopPanel.SetActive(false);
        enemiesPanel.SetActive(false);
        gameMechanicsPanel.SetActive(false);
        difficultyPanel.SetActive(false);
    }
}
