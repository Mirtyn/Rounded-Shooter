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
        Game.GameType = Assets.Models.GameType.Easy;
        SceneManager.LoadScene(1);
    }

    public void StartMediumGame()
    {
        Game.GameType = Assets.Models.GameType.Medium;
        SceneManager.LoadScene(1);
    }

    public void StartHardGame()
    {
        Game.GameType = Assets.Models.GameType.Hard;
        SceneManager.LoadScene(1);
    }

    public void StartMasterGame()
    {
        Game.GameType = Assets.Models.GameType.Master;
        SceneManager.LoadScene(1);
    }
}
