using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Projectbehaviour
{
    public bool ShopOpened = false;
    public int ArrowSpeedLevel = 1;
    public int PlayerTurnSpeedLevel = 1;
    public int ShootingCooldownLevel = 1;
    public int Bombs = 0;
    public int MaxBombs = 3;
    public bool HasForceField = false;
}
