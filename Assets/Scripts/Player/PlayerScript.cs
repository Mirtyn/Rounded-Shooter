using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Vector3 startPos;
    void Start()
    {
        startPos = this.transform.position;
    }

    void Update()
    {
        this.transform.position = startPos;
    }
}
