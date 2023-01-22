using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
	internal class WaveBuilder
	{
		public class Setting
		{
			public EnemyType EnemyType;
			public int Count;
			public float Speed;
			public float MinRadius;
			public float MaxRadius;
		}

		public float StartTime = 1f;

		public WaveBuilder()
		{
			Settings = new List<Setting>();
		}

		private List<Setting> Settings;

		public WaveBuilder AddStartTime(float starttime)
		{
			StartTime = starttime;

			return this;
		}

		public WaveBuilder AddSetting(EnemyType enemyType, int count, float speed, float minRadius, float maxRadius)
		{
			Settings.Add(new Setting { EnemyType = enemyType, Count = count, Speed = speed, MinRadius = minRadius, MaxRadius = maxRadius });

			return this;
		}

		public Wave BuildWave()
		{
			return BuildWave(Settings, StartTime);
		}

		public Wave BuildWave(IEnumerable<Setting> settings, float startTime)
		{
			var wave = new Wave { WaveStartTime = startTime };

			foreach (var setting in settings)
			{
				for (var i = 0; i < setting.Count; i++)
				{
					var enemy = new Enemy
					{
						EnemyType = setting.EnemyType,
						Speed = setting.Speed,
						MinRadius = setting.MinRadius,
						MaxRadius = setting.MaxRadius,
					};

					wave.Enemies.Add(enemy);
				}
			}

			return wave;
		}
	}
}
