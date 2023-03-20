using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

internal class FinishAndDeathScreenScript : ProjectBehaviour
{
    [SerializeField] GameObject submitPanel;
    [SerializeField] TMP_InputField nameText;
    [SerializeField] Button submitForRealButton;
    [SerializeField] Button submitButton;
    [SerializeField] EnemyWavesControler enemyWavesControler;
    //string startString;

    //void Start()
    //{
    //    startString = nameText.text;
    //}

    void Update()
    {
        //if (nameText.text == startString)
        //{
        //    submitForRealButton.interactable = false;
        //}
        //else
        //{
        //    submitForRealButton.interactable = true;
        //}

        //submitForRealButton.interactable = nameText.text != startString;
        submitForRealButton.interactable = nameText.text.Length >= 2;
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
