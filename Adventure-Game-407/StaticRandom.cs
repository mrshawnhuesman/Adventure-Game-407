using System;
using System.Threading;

namespace Adventure_Game_407
{
    // StaticRandom class
    // Uses multi-threading to reduce the need to declare local random variables
    public static class StaticRandom
    {
        private static int _seed;

        private static ThreadLocal<Random> threadLocal = new ThreadLocal<Random>
            (() => new Random(Interlocked.Increment(ref _seed)));

        static StaticRandom()
        {
            _seed = Environment.TickCount;
        }

        public static Random Instance
        {
            get { return threadLocal.Value; }
        }
    }
}