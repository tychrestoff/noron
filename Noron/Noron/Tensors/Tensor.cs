using System;
using Noron.Utilities;
namespace Noron.Tensors
{
    public class Tensor
    {
        public Tensor(int[] dimensions)
        {
            Dimensions = dimensions;
            Size = dimensions.Product();
            Data = new double[Size];
        }

        public double[] Data { get; }
        public int[] Dimensions { get; private set; }
        public int Rank => Dimensions.Length;
        public int Size { get; }

        public void Reshape(int[] newDimensions)
        {
            var newSize = newDimensions.Product();

            if (Size != newSize) throw new ArgumentException(
                "Invalid reshape size.");
            Dimensions = newDimensions;
        }
    }
}
