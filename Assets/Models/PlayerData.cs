using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public bool ShopOpened;
    public int ArrowSpeedLevel;
    public int PlayerTurnSpeedLevel;
    public int ShootingCooldownLevel;
    public int Bombs;
    public int MaxBombs;
    public bool HasForceField;

    public PlayerData()
    {
        Reset();
    }

    public void Reset()
    {
        ShopOpened = false;
        ArrowSpeedLevel = 1;
        PlayerTurnSpeedLevel = 1;
        ShootingCooldownLevel = 1;
        Bombs = 0;
        MaxBombs = 3;
        HasForceField = false;
    }
}
