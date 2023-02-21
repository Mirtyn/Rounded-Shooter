using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectBehaviour : MonoBehaviour
{
    static public bool y_Clamp = false;

    static public bool UseRadiusSpawner = true;
        
    //public static ScoreManager ScoreCalculator = new ScoreManager();

    //public static PlayerData PlayerData = new PlayerData();

    public static GameManager Game = new GameManager();

    public void Reset()
    {
        Game.Reset();
    }
}
