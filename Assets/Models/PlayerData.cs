using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerData
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

    public bool IsDead { get; internal set; }

    public void Reset()
    {
        IsDead = false;
        ShopOpened = false;
        ArrowSpeedLevel = 1;
        PlayerTurnSpeedLevel = 1;
        ShootingCooldownLevel = 1;
        Bombs = 0;
        MaxBombs = 3;
        HasForceField = false;
    }
}
