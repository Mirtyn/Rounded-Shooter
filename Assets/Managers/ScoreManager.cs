using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager
{
    private float Starttime = 0f;

    private long deathscore = 0;

    public void MarkStart(float starttime)
    {
        Starttime = starttime;
    }

    public long CalculateScore(float time, int gold)
    {
        var timescore = (long)((time - Starttime) * 100f);

        var goldscore = (long)((gold) * 1000f);

        return timescore + deathscore + goldscore;
    }

    public void Reset()
    {
        Starttime = 0;
        deathscore = 0;
    }

    internal void TrackEnemyDeath(Enemy enemy, GameObject enemyGameObject, GameObject playerGameObject)
    {
        if(enemyGameObject == null)
        {
            var t = 0;
        }

        if (playerGameObject == null)
        {
            var t = 0;
        }

        var d = Vector3.Distance(enemyGameObject.transform.position, playerGameObject.transform.position);

        var minscore = 20;
        var maxscore = 1000;
        var maxdistance = 6f;

        var f = 1f - (d / maxdistance);

        if(f < 0f)
        {
            f = 0f;
        }

        f = Mathf.Pow(f, 4);

        deathscore += (long)(minscore + (f * maxscore));
    }
}
