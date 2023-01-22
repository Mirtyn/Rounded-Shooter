using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
	internal class SquareSpawner
	{
		public Vector3 RandomPosition(Enemy enemy)
		{
            var height = enemy.Height;

            Vector3 v;

            int rArea = UnityEngine.Random.Range(0, 4);

            if (rArea == 0)
            {
                v = new Vector3(UnityEngine.Random.Range(20f, -10f), height, UnityEngine.Random.Range(20f, 10f));
            }
            else if (rArea == 1)
            {
                v = new Vector3(UnityEngine.Random.Range(-20f, -10f), height, UnityEngine.Random.Range(20f, -10f));
            }
            else if (rArea == 2)
            {
                v = new Vector3(UnityEngine.Random.Range(-20f, 10f), height, UnityEngine.Random.Range(-20f, -10f));
            }
            else // if (rArea == 3)
            {
                v = new Vector3(UnityEngine.Random.Range(20f, 10f), height, UnityEngine.Random.Range(-20f, 10f));
            }

            return v;
        }
    }
}
