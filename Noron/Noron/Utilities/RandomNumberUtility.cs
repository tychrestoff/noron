using System;
using static System.Math;

namespace Noron.Utilities
{
    public static class RandomNumberUtility
    {
        private static Random rand = new Random();

        /// <summary>
        /// Pseudorandom Gaussian distrubution sampler
        /// </summary>
        /// <param name="mu">Expected value, mean</param>
        /// <param name="sigma">Standard deviation</param>
        /// <returns>Pseudorandom number normally distributed about
        /// the specifed mu with specified variance</returns>
        public static double PseudoGaussian(double mu, double sigma)
        {
            double u1 = rand.NextDouble();
            double u2 = rand.NextDouble();

            double randNorm =
                Sqrt(-2.0 * Log(u1)) * Sin(2.0 * PI * u2);
            return mu + sigma * randNorm;
        }
    }
}
