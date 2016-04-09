using System;

using static System.Math;

namespace Noron.Utilities
{
    public static class NoronUtilities
    {
        public static double Acosh(double x)
            => Log(x + Sqrt(Pow(x, 2) - 1));
        public static double Asinh(double x)
            => Log(x + Sqrt(Pow(x, 2) + 1));
        public static double Atanh(double x)
            => 0.5 * Log((1 + x) / (1 - x));
        public static double Sigmoid(double x)
            => 1 / (1 + Exp(-x));
    }
}
