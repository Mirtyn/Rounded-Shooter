using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
	internal class TimedSpawner
	{
		public float Radius = 16f;

		private System.Random random = new System.Random();

		public TimedSpawner(float radius = 10f)
		{
			Radius = radius;
		}

		public IEnumerable<TimedEnemy> Build(EnemyType enemyType, float arrivaltime, float speed, int count = 1)
        {
			var radius = Radius;

			var time = radius / speed;

			if(time > arrivaltime)
            {
				time = arrivaltime;
				radius = time * speed;
			}

			var radians = (float)random.NextDouble() * MathF.PI * 2f;

			var x = MathF.Cos(radians) * radius;
			var z = MathF.Sin(radians) * radius;

			for(var c = 0; c < count; c++)
            {
				yield return new TimedEnemy
				{
					EnemyType = enemyType,
					Speed = speed,
					IsAlive = true,
					Position = new Vector3(x, 0f, z),
					StartTime = arrivaltime - time,
				};
			}
		}
	}
}
