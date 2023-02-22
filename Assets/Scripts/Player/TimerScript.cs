using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : ProjectBehaviour
{
    public float InGameTime;

    public bool KeepTrackOfTime = true;

    void Start()
    {
        Game.ScoreManager.Reset();
        Game.ScoreManager.MarkStart(InGameTime);
    }


    void Update()
    {
        if (KeepTrackOfTime)
        {
            InGameTime += Time.deltaTime;
        }
    }
}
