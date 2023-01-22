using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
	internal class RadiusSpawner
	{
		public float MinRadius = 10f;
		public float MaxRadius = 10f;

		public RadiusSpawner(float radius)
			: this(radius, radius)
		{
		}

		public RadiusSpawner(float minRadius, float maxRadius)
		{
			MinRadius = minRadius;
			MaxRadius = maxRadius;
		}

		public Vector3 RandomPosition()
		{
			var radians = UnityEngine.Random.value * MathF.PI * 2f;

			var radius = MinRadius + (UnityEngine.Random.value * (MaxRadius - MinRadius));

			var x = MathF.Cos(radians) * radius;
			var z = MathF.Sin(radians) * radius;

			return new Vector3(x, 0f, z);
		}
	}
}
