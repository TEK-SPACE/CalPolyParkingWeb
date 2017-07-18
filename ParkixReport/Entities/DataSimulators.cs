using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parkix.Report
{
    /// <summary>
    /// Simulate parking behaviors.
    /// </summary>
    public class SimulationHelpers
    {
        /// <summary>
        /// Sinusoidal simulation with noise.
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="maxRandom"></param>
        /// <returns></returns>
        public static double[] SimulationOne(int amplitude, int maxRandom = 0)
        {
            var data = new double[288];

            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 288; i++)
            {
                var value = Math.Sin(i * (Math.PI / 287)) * amplitude;

                if (maxRandom != 0)
                {
                    value += rand.Next() % maxRandom;
                }

                value = value < 0 ? 0 : value;
                value = value > amplitude ? amplitude : value;

                data[i] = value;
            }

            return data;
        }
    }

    
}
