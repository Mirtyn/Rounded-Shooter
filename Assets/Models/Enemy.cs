using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
	internal class Enemy
	{
		public EnemyType EnemyType;

		public float Speed;

		//public Vector3 Position;

		public bool IsAlive = true;

        public float MinRadius;
        public float MaxRadius;

        public float Height
        {
			get
            {
                switch(EnemyType)
                {
                    case EnemyType.Fast:
                        return 0.5f;
                    default:
                        return 0.6f;
                }
            }
        }
	}
}
