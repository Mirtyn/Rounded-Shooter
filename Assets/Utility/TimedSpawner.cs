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

		public IEnumerable<Enemy> Build(EnemyType enemyType, float arrivaltime, float speed, int count = 1)
        {
			if (enemyType == EnemyType.SpawnerBoss)
			{
				return new[] { Build(enemyType, new Vector3(0, 0, 5), arrivaltime, speed) };
			}

			var radius = Radius;

			var time = radius / speed;

			if(time > arrivaltime)
            {
				time = arrivaltime;
				radius = time * speed;
			}

			var spawns = new Enemy[count];

			for (var c = 0; c < count; c++)
            {
				var radians = (float)random.NextDouble() * MathF.PI * 2f;

				var x = MathF.Cos(radians) * radius;
				var z = MathF.Sin(radians) * radius;

				spawns[c] = new Enemy
				{
					EnemyType = enemyType,
					Speed = speed,
					IsAlive = true,
					Position = new Vector3(x, 0f, z),
					StartTime = arrivaltime - time,
				};
			}

			return spawns;
		}

        public Enemy Build(EnemyType enemyType, Vector3 position, float starttime, float speed)
        {
            return new Enemy
            {
                EnemyType = enemyType,
                Speed = speed,
                IsAlive = true,
                Position = position,
                StartTime = starttime,
            };
        }
    }
}
