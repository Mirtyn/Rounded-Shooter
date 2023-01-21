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

		public WaveBuilder AddSetting(EnemyType enemyType, int count, float speed = 0.5f)
		{
			Settings.Add(new Setting { EnemyType = enemyType, Count = count, Speed = speed });

			return this;
		}

		public Wave BuildWave()
		{
			var wave = new Wave { WaveStartTime = StartTime };

			foreach (var setting in Settings)
			{
				for (var i = 0; i < setting.Count; i++)
				{
					var enemy = new Enemy
					{
						EnemyType = setting.EnemyType,
						Speed = setting.Speed,
					};

					wave.Enemies.Add(enemy);
				}
			}

			return wave;
		}
	}
}
