﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    /// <summary>
    /// A thread-aware version of the Random class.
    /// It is still NOT thread-safe, but different instances can still be used by different threads without issues
    /// (which is not true of Random - two instances of Random created on separate threads at nearly the same time
    /// will return all the same results, which is not what we want)
    /// 
    /// Could also be used later, if I want, to allow a common interface between Random and RNGCryptoServiceProvider
    /// </summary>
    public class ThreadSafeRandom
    {
        private static readonly System.Random _global = new System.Random();
        private System.Random _local; //Could make it completely thread-safe using ThreadLocal<>, but that is .Net 4.0 only :(

        public ThreadSafeRandom()
        {
            //Instantiating multiple Random() instances in a row very quickly will result in
            //all of them returning the same numbers.  This is a workaround for that problem.
            int seed;
            lock (_global)
            {
                seed = _global.Next();
            }
            _local = new System.Random(seed);
        }

        public ThreadSafeRandom(int seed)
        {
            _local = new System.Random(seed);
        }

        public int Next()
        {
            return _local.Next();
        }

        public int Next(int maxValue)
        {
            return _local.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return _local.Next(minValue, maxValue);
        }

        public double NextDouble()
        {
            return _local.NextDouble();
        }

        public long NextLong()
        {
            return _local.NextLong();
        }

        public long NextLong(long max)
        {
            return _local.NextLong(max);
        }

        public long NextLong(long min, long max)
        {
            return _local.NextLong(min, max);
        }
    }
}
