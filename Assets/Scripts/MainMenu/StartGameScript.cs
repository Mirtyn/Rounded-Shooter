using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : ProjectBehaviour
{
    [SerializeField] GameObject startGamePanel;

    public void BackButtonPressed()
    {
        startGamePanel.SetActive(false);
    }

    public void StartEasyGame()
    {
        Game_Type = GameType.Easy;
        SceneManager.LoadScene(1);
    }

    public void StartMediumGame()
    {
        Game_Type = GameType.Medium;
        SceneManager.LoadScene(1);
    }

    public void StartHardGame()
    {
        Game_Type = GameType.Hard;
        SceneManager.LoadScene(1);
    }

    public void StartMasterGame()
    {
        Game_Type = GameType.Master;
        SceneManager.LoadScene(1);
    }
}
