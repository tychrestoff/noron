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
            double u, v, x, y, q;
            do
            {
                u = 1 - rand.NextDouble();
                v = 1.7156 * (rand.NextDouble() - 0.5);
                x = u - 0.449871;
                y = Abs(v) + 0.386595;
                q = x * x + y * (0.196 * y - 0.25472 * x);
            } while (q >= 0.27597 && (q > 0.27846 || v * v > -4 * u * u * Log(u)));
            return mu + sigma * v / u;
        }
    }
}
