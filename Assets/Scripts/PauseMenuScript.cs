using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class PauseMenuScript : ProjectBehaviour
{
    void Update()
    {
        if (Game.IsPaused == true && Input.GetKeyDown(KeyCode.Return))
        {
            QuitBackToMainMenuButtonPressed();
        }
    }

    public void QuitBackToMainMenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void CancelQuitBackToMainMenuButtonPressed()
    {
        Time.timeScale = 1f;
        Game.IsPaused = false;
        this.gameObject.SetActive(false);
    }
}
