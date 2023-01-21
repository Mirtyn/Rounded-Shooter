using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
	internal class Wave
	{
		public List<Enemy> Enemies;

		//public float WaveTime = 10f;

		public float WaveStartTime = 1f;

        public Wave()
        {
			Enemies = new List<Enemy>();
		}

		public bool AreAllDead()
		{
			return !Enemies.Any(o => o.IsAlive);
		}
	}
}
