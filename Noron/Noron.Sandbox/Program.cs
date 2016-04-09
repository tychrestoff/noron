using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Noron.NdArrays;

namespace Noron.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            NdArray cube0 = new NdArray(new int[] { 10, 10, 10 }).FillRandom();
            NdArray cube1 = new NdArray(new int[] { 10, 10, 10 }).FillRandom();

            var asdf = cube0.Sum() + cube1.Sum();

            var softmax0 = cube0.Softmax();
            var softmax1 = cube1.Softmax();

            var two = softmax0.Sum() + softmax1.Sum();
        }
    }
}
