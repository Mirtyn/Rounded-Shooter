using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    private float Starttime = 0f;

    private long KillScore = 0;

    public long CurrentScore { get; set; } = 0;

    public void MarkStart(float starttime)
    {
        Starttime = starttime;
    }

    public long CalculateScore(float time, int gold, GameType gameType)
    {
        var timescore = (long)((time - Starttime) * TimeScoreMultiplier(gameType));

        var goldscore = gameType == GameType.Endless ? 0 : (long)((gold) * 1000f);

        CurrentScore = timescore + KillScore + goldscore;

        return CurrentScore;
    }

    private float TimeScoreMultiplier(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.Easy:
                return 100f;
            case GameType.Medium:
                return 250f;
            case GameType.Hard:
                return 500f;
            case GameType.Master:
                return 250f;
            case GameType.Endless:
                return 250f;
            default:
                return 0f;
        }
    }


    public void Reset()
    {
        CurrentScore = 0;
        Starttime = 0;
        KillScore = 0;
    }

    internal void TrackKillScore(Enemy enemy, GameObject enemyGameObject, GameObject playerGameObject, GameType gameType)
    {
        var d = Vector3.Distance(enemyGameObject.transform.position, playerGameObject.transform.position);

        var minscore = MinKillScore(gameType);
        var maxscore = MaxKillScore(gameType);
        var maxdistance = 6f;

        var f = 1f - (d / maxdistance);

        if (f < 0f)
        {
            f = 0f;
        }

        f = Mathf.Pow(f, 4);

        KillScore += (long)(minscore + (f * maxscore));
    }

    private int MinKillScore(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.Easy:
                return 20;
            case GameType.Medium:
                return 50;
            case GameType.Hard:
                return 100;
            case GameType.Master:
                return 50;
            case GameType.Endless:
                return 1000;
            default:
                return 0;
        }
    }

    private int MaxKillScore(GameType gameType)
    {
        switch (gameType)
        {
            case GameType.Easy:
                return 1000;
            case GameType.Medium:
                return 2500;
            case GameType.Hard:
                return 5000;
            case GameType.Master:
                return 2500;
            case GameType.Endless:
                return 5000;
            default:
                return 0;
        }
    }
}
