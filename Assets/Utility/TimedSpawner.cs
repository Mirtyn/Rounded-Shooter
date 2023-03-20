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

		public float CasualMinSpeed { get; internal set; } = 0.75f;
		public float CasualMaxSpeed { get; internal set; } = 0.75f;
		public float FastMinSpeed { get; internal set; } = 1.55f;
		public float FastMaxSpeed { get; internal set; } = 1.55f;
		public float ToughMinSpeed { get; internal set; } = 0.45f;
		public float ToughMaxSpeed { get; internal set; } = 0.45f;

		private float _difficultyModifier = 1f;

		public float DifficultyModifier 
		{ 
			get { return _difficultyModifier; } 
			set { _difficultyModifier = Math.Max(Math.Min(2f, value), 1f); } 
		}

		public void SetDefaultSpeeds(float speedModifier = 1f)
        {
			var s = new TimedSpawner();

			CasualMinSpeed = s.CasualMinSpeed * speedModifier;
			CasualMaxSpeed = s.CasualMaxSpeed * speedModifier;
			FastMinSpeed = s.FastMinSpeed * speedModifier;
			FastMaxSpeed = s.FastMaxSpeed * speedModifier;
			ToughMinSpeed = s.ToughMinSpeed * speedModifier;
			ToughMaxSpeed = s.ToughMaxSpeed * speedModifier;
		}


		[Obsolete("Use Build(EnemyType enemyType, float arrivaltime, int count = 1) instead. The speed paramater is ignored from now on")]
		public IEnumerable<Enemy> Build(EnemyType enemyType, float arrivaltime, float speed, int count = 1)
		{
			return Build(enemyType, arrivaltime, count);
		}

		public IEnumerable<Enemy> Build(EnemyType enemyType, float arrivaltime, int count = 1)
        {
			if (enemyType == EnemyType.SpawnerBoss)
			{
				return new[] { Build(enemyType, new Vector3(0, 0, 5), arrivaltime) };
			}

			var speed = Speed(enemyType);

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

				spawns[c] = Build(enemyType, new Vector3(x, 0f, z), arrivaltime - time);
			}

			return spawns;
		}

        public Enemy Build(EnemyType enemyType, Vector3 position, float starttime)
        {
            return new Enemy
            {
                EnemyType = enemyType,
                Speed = Speed(enemyType),
                IsAlive = true,
                Position = position,
                StartTime = starttime,
				DifficultyModifier = DifficultyModifier,
			};
        }

		private float Speed(EnemyType enemyType)
        {
			switch(enemyType)
            {
				case EnemyType.Fast:
					return UnityEngine.Random.Range(FastMinSpeed, FastMaxSpeed);
				case EnemyType.Tough:
					return UnityEngine.Random.Range(ToughMinSpeed, ToughMaxSpeed);
				default:
					return UnityEngine.Random.Range(CasualMinSpeed, CasualMaxSpeed);
			}
		}
    }
}
