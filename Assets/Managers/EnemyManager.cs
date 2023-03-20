using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class EnemyManager
{
    public TimedSpawner Spawner { get; set; } = new TimedSpawner();

    public List<Enemy> Enemies { get; set; } = new List<Enemy>();

    public void Reset()
    {
        Enemies.Clear();
    }

    public bool AreAllDead()
    {
        return Enemies.All(o => !o.IsAlive);
    }

    public bool HaveAllSpawned()
    {
        return Enemies.All(o => o.HasSpawned);
    }

}
