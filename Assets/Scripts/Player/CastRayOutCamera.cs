using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CastRayOutCamera : ProjectBehaviour
{
    [SerializeField] Camera mainCamera;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point;
        }
    }
}
