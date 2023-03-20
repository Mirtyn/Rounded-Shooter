using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ProjectBehaviour : MonoBehaviour
{
    public GameManager Game { get; private set; } = GameManager.Instance;
}
