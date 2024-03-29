﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    internal class Enemy
	{
		public EnemyType EnemyType;

		public float Speed;

		public Vector3 Position;

        public bool IsAlive = true;

        public bool HasSpawned = false;

        public float StartTime;

        public int InstanceID;

        public GameObject GameObject { get; set; }

        public float BossDifficultyModifier { get; set; } = 1f;

        public int BossHP { get; set; } = 40;

        //     public float Height
        //     {
        //get
        //         {
        //             switch(EnemyType)
        //             {
        //                 case EnemyType.Fast:
        //                     return 0.5f;
        //                 default:
        //                     return 0.6f;
        //             }
        //         }
        //     }
    }
}
