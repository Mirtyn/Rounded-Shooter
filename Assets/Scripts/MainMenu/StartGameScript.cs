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
        StartGame(Assets.Models.GameType.Easy);
    }

    public void StartMediumGame()
    {
        StartGame(Assets.Models.GameType.Medium);
    }

    public void StartHardGame()
    {
        StartGame(Assets.Models.GameType.Hard);
    }

    public void StartMasterGame()
    {
        StartGame(Assets.Models.GameType.Master);
    }

    public void StartEndlessGame()
    {
        StartGame(Assets.Models.GameType.Endless);
    }

    private void StartGame(Assets.Models.GameType gameType)
    {
        Game.GameType = gameType;
        SceneManager.LoadScene(1);
    }
}
