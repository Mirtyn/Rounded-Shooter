using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishAndDeathScreenScript : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }


}
