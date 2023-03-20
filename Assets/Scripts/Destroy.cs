using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Destroy : ProjectBehaviour
{
    [SerializeField] float timeInSec = 5f;
    void Update()
    {
        if (timeInSec > 0)
        {
            timeInSec -= Time.deltaTime;
        }

        if (timeInSec <= 0)
        {
            Destroy(gameObject);
        }
    }
}
