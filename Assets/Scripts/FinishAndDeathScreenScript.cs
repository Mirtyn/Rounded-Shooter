using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class FinishAndDeathScreenScript : ProjectBehaviour
{
    [SerializeField] GameObject submitPanel;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Button submitForRealButton;
    [SerializeField] Button submitButton;
    [SerializeField] EnemyWavesControler enemyWavesControler;

    void Update()
    {
        if (nameText.text == "")
        {
            submitForRealButton.interactable = false;
        }
        else
        {
            submitForRealButton.interactable = true;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SubmitButtonPressed()
    {
        submitPanel.SetActive(true);
    }

    public void CancelSubmitButtonPressed()
    {
        submitPanel.SetActive(false);
    }

    public void SubmitForRealButtonPressed()
    {
        submitPanel.SetActive(false);
        enemyWavesControler.SubmitButtonPressed(nameText.text);
        submitButton.interactable = false;
    }
}
