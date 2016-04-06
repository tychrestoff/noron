using System;
using static Noron.Utilities.RandomNumberUtility;
namespace Noron.Volumes
{
    // Volume class
    // A generic, 3rd order tensorlike object that
    // also carries a gradient appropriately
    public class Volume
    {
        /// <summary>
        /// Volume constructor taking width, height, depth
        /// </summary>
        /// <param name="sx">Size of x dimension (width)</param>
        /// <param name="sy">Size of y dimension (height)</param>
        /// <param name="sz">Size of z dimension (depth)</param>
        public Volume(int sx, int sy, int sz)
        {
            Sx = sx; Sy = sy; Sz = sz;
            var n = sx * sy * sz;

            Values = new double[n];
            Grads = new double[n];

            var normScale = Math.Sqrt(1.0 / n);
            for (int i = 0; i < n; ++i)
            {
                Values[i] = PseudoGaussian(0.0, normScale);
            }
        }

        /// <summary>
        /// Volume constructor taking width, height, depth, initial value
        /// </summary>
        /// <param name="sx">Size of x dimension (width)</param>
        /// <param name="sy">Size of y dimension (height)</param>
        /// <param name="sz">Size of z dimension (depth)</param>
        /// <param name="initValue">Initialization value</param>
        public Volume(int sx, int sy, int sz, double initValue)
        {
            Sx = sx; Sy = sy; Sz = sz;
            var n = sx * sy * sz;

            Values = new double[n];
            Grads = new double[n];

            for (int i = 0; i < n; ++i)
            {
                Values[i] = initValue;
            }
        }

        /// <summary>
        /// Volume constructor taking another Volume to clone
        /// </summary>
        /// <param name="vol">Volume to clone</param>
        public Volume(Volume vol)
        {
            Sx = vol.Sx; Sy = vol.Sy; Sz = vol.Sz;
            var n = vol.Values.Length;

            Values = new double[n];
            Grads = new double[n];

            for (int i = 0; i < n; ++i)
            {
                Values[i] = vol.Values[i];
                Grads[i] = vol.Grads[i];
            }
        }


        // 1d array of gradients
        public double[] Grads { get; private set; }
        // Size of x dimension (width)
        public int Sx { get; }
        // Size of y dimension (height)
        public int Sy { get; }
        // Size of z dimension (depth)
        public int Sz { get; }
        // 1d array of values
        public double[] Values { get; private set; }


        /// <summary>
        /// Add value to gradient at specified coordinate
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="z">z index</param>
        /// <param name="value">Value to add</param>
        public void AddGrad(int x, int y, int z, double value)
            => Grads[indexer(x, y, z)] += value;

        /// <summary>
        /// Scale a Volume by 64bit float value and add to
        /// this Volume elementwise
        /// </summary>
        /// <param name="vol">Volume to scale and add</param>
        /// <param name="scale">64bit float scale</param>
        public void AddScaledVolume(Volume vol, double scale)
        {
            for (int i = 0; i < Values.Length; ++i)
            {
                Values[i] += scale * vol.Values[i];
            }
        }

        /// <summary>
        /// Add value to value at specified coordinate
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="z">z index</param>
        /// <param name="value">Value to add</param>
        public void AddValue(int x, int y, int z, double value)
            => Values[indexer(x, y, z)] += value;

        /// <summary>
        /// Add a Volume to this Volume elementwise
        /// </summary>
        /// <param name="vol">Volume to add</param>
        public void AddVolume(Volume vol)
        {
            for (int i = 0; i < Values.Length; ++i)
            {
                Values[i] += vol.Values[i];
            }
        }
        
        /// <summary>
        /// Wrapper for the cloning constructor
        /// </summary>
        /// <returns>Clone of this Volume</returns>
        public Volume DeepClone()
            => new Volume(this);

        /// <summary>
        /// Get gradient at specified 3d coordinate
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="z">z index</param>
        /// <returns>Gradient at specified coordinate</returns>
        public double GetGrad(int x, int y, int z)
            => Grads[indexer(x, y, z)];

        /// <summary>
        /// Get value at specified 3d coordinate
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="z">z index</param>
        /// <returns>Value at specified coordinate</returns>
        public double GetValue(int x, int y, int z)
            => Values[indexer(x, y, z)];
        
        /// <summary>
        /// Sets all values to constant
        /// </summary>
        /// <param name="c">Constant</param>
        public void SetConstant(double c)
        {
            for (int i = 0; i < Values.Length; ++i)
            {
                Values[i] = c;
            }
        }

        /// <summary>
        /// Set gradient at specified 3d coordiate to new value
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="z">z index</param>
        /// <param name="value">New value</param>
        public void SetGrad(int x, int y, int z, double value)
            => Grads[indexer(x, y, z)] = value;

        /// <summary>
        /// Set value at specified 3d coordinate to new value
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="z">z index</param>
        /// <param name="value">New value</param>
        public void SetValue(int x, int y, int z, double value)
            => Values[indexer(x, y, z)] = value;

        /// <summary>
        /// Creates new volume of equal dimensions with all
        /// zero values
        /// </summary>
        /// <returns>New zero volume</returns>
        public Volume ZeroClone()
            => new Volume(Sx, Sy, Sz, 0);


        /// <summary>
        /// Indexer helper function mapping 3d coordinate to
        /// a singular index for Values and Grads arrays
        /// </summary>
        /// <param name="x">x index</param>
        /// <param name="y">y index</param>
        /// <param name="z">z index</param>
        /// <returns>1d index for Values or Grads array</returns>
        private int indexer(int x, int y, int z)
            => ((Sx * y) + x) * Sz + z;
    }
}