using System;
using Noron.Utilities;

using static Noron.Utilities.RandomNumberUtility;

namespace Noron.NdArrays
{
    public partial class NdArray
    {
        public NdArray(int[] dimensions)
        {
            Dimensions = dimensions;
            Length = dimensions.Product();
            Data = new double[Length];
        }


        public double[] Data { get; private set; }
        public int[] Dimensions { get; private set; }
        public int Length { get; private set; }
        public int Rank => Dimensions.Length;


        public NdArray Abs() => UnaryFunc(x => Math.Abs(x));
        public NdArray Acos() => UnaryFunc(x => Math.Acos(x));
        public NdArray Acosh() => UnaryFunc(x => NoronUtilities.Acosh(x));
        public NdArray Add(double x) => BinaryFunc((a, b) => a + b, x);
        public NdArray Add(NdArray x) => BinaryFunc((a, b) => a + b, x);
        public NdArray Asin() => UnaryFunc(x => Math.Asin(x));
        public NdArray Asinh() => UnaryFunc(x => NoronUtilities.Asinh(x));
        public NdArray Atan() => UnaryFunc(x => Math.Atan(x));
        public NdArray Atanh() => UnaryFunc(x => NoronUtilities.Atanh(x));
        public NdArray Ceiling() => UnaryFunc(x => Math.Ceiling(x));
        public NdArray Cos() => UnaryFunc(x => Math.Cos(x));
        public NdArray Cosh() => UnaryFunc(x => Math.Cosh(x));
        public NdArray Clone()
        {
            var clone = new NdArray(Dimensions);
            return clone.Copy(this);
        }
        public NdArray CloneRef()
        {
            var a = new NdArray(Dimensions);
            return a.CopyRef(this);
        }
        public NdArray Copy(NdArray other)
        {
            if (Length != other.Length) throw new ArgumentException(
                $"Invalid copy array. Expected array length = {Length}. " +
                $"Array passed has length = {other.Length}.");
            for (int i = 0; i < Length; ++i) Data[i] = other.Data[i];
            return this;
        }
        public NdArray CopyRef(NdArray other)
        {
            Dimensions = other.Dimensions;
            Length = other.Length;
            Data = other.Data;
            return this;
        }
        public NdArray Divide(double x) => BinaryFunc((a, b) => a / b, x);
        public NdArray Divide(NdArray x) => BinaryFunc((a, b) => a / b, x);
        public NdArray Exp() => UnaryFunc(x => Math.Exp(x));
        public NdArray Floor() => UnaryFunc(x => Math.Floor(x));
        public NdArray Fill(double c)
        {
            for (int i = 0; i < Length; ++i) Data[i] = c;
            return this;
        }
        public NdArray FillRandom()
        {
            var normScale = Math.Sqrt(1.0 / Length);
            for (int i = 0; i < Length; ++i) Data[i] = PseudoGaussian(0, normScale);
            return this;
        }
        public double Get(int[] coords)
        {
            if (coords.Length != Rank) throw new ArgumentException(
                $"Invalid coordinate count. Expected {Rank} coordinates " +
                $"and {coords.Length} were given.");

            var idx = 0;
            for (var i = 0; i < Rank; ++i)
            {
                idx = idx * Dimensions[i] + coords[i];
            }

            return Data[idx];
        }
        public NdArray Invert() => UnaryFunc(x => 1 / x);
        public NdArray Log() => UnaryFunc(x => Math.Log(x));
        public NdArray Max(double x) => BinaryFunc((a, b) => Math.Max(a, b), x);
        public NdArray Max(NdArray x) => BinaryFunc((a, b) => Math.Max(a, b), x);
        public NdArray Min(double x) => BinaryFunc((a, b) => Math.Min(a, b), x);
        public NdArray Min(NdArray x) => BinaryFunc((a, b) => Math.Min(a, b), x);
        public NdArray Modulo(double x) => BinaryFunc((a, b) => a % b, x);
        public NdArray Module(NdArray x) => BinaryFunc((a, b) => a % b, x);
        public NdArray Multiply(double x) => BinaryFunc((a, b) => a * b, x);
        public NdArray Multiply(NdArray x) => BinaryFunc((a, b) => a * b, x);
        public NdArray Negate() => UnaryFunc(x => -x);
        public NdArray Pow(double x) => BinaryFunc((a, b) => Math.Pow(a, b), x);
        public NdArray Pow(NdArray x) => BinaryFunc((a, b) => Math.Pow(a, b), x);
        public NdArray PseudoInvert() => UnaryFunc(x => x == 0 ? 0 : 1 / x);
        public NdArray Reshape(int[] newDimensions)
        {
            var newSize = newDimensions.Product();

            if (newSize != Length) throw new ArgumentException(
                $"Invalid reshape dimensions. Expected size {Length}. " +
                $"The {newDimensions.Length} dimensions passed have " +
                $"total size equal to {newSize}.");

            Dimensions = newDimensions;
            return this;
        }
        public NdArray Round() => UnaryFunc(x => Math.Round(x));
        public void Set(int[] coords, double val)
        {
            if (coords.Length != Rank) throw new ArgumentException(
                $"Invalid coordinate count. Expected {Rank} coordinates " +
                $"and {coords.Length} were given.");

            var idx = 0;
            for (var i = 0; i < Rank; ++i)
            {
                idx = idx * Dimensions[i] + coords[i];
            }

            Data[idx] = val;
        }
        public NdArray Sin() => UnaryFunc(x => Math.Sin(x));
        public NdArray Sinh() => UnaryFunc(x => Math.Sinh(x));
        public NdArray Sqrt() => UnaryFunc(x => Math.Sqrt(x));
        public NdArray Subtract(double x) => BinaryFunc((a, b) => a - b, x);
        public NdArray Subtract(NdArray x) => BinaryFunc((a, b) => a - b, x);
        public NdArray Tan() => UnaryFunc(x => Math.Tan(x));
        public NdArray Tanh() => UnaryFunc(x => Math.Tanh(x));
        public NdArray Zero() => Fill(0);


        private NdArray UnaryFunc(Func<double, double> unaryFunction)
        {
            var newArray = Clone();
            for (int i = 0; i < Length; ++i) newArray.Data[i] = unaryFunction(Data[i]);
            return newArray;
        }
        private NdArray BinaryFunc(Func<double, double, double> binaryFunction, double x)
        {
            var newArray = Clone();
            for (int i = 0; i < Length; ++i) newArray.Data[i] = binaryFunction(Data[i], x);
            return newArray;
        }
        private NdArray BinaryFunc(Func<double, double, double> binaryFunction, NdArray x)
        {
            var newArray = Clone();
            for (int i = 0; i < Length; ++i) newArray.Data[i] = binaryFunction(Data[i], x.Data[i]);
            return newArray;
        }
    }
}
