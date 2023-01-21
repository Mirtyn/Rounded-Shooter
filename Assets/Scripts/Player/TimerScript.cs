using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : Projectbehaviour
{
    public float InGameTime;
    void Update()
    {
        InGameTime += Time.deltaTime;
    }
}
