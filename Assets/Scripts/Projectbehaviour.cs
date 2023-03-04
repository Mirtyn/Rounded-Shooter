using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectBehaviour : MonoBehaviour
{
    static public bool y_Clamp = false;

    static public bool UseRadiusSpawner = true;

    static public bool IsPaused = false;

    bool resetPlayerData;

    //public static ScoreManager ScoreCalculator = new ScoreManager();

    //public static PlayerData PlayerData = new PlayerData();

    public enum GameType
    {
        Easy,
        Medium,
        Hard,
        Random,
        Master,
    }

    public static GameType Game_Type;


    public static GameManager Game = new GameManager();

    public void Reset()
    {
        IsPaused = false;

        if (Game_Type == GameType.Master)
        {
            resetPlayerData = false;
            Game.Reset(resetPlayerData);
        }
        else
        {
            resetPlayerData = true;
            Game.Reset(resetPlayerData);
        }
    }
}
