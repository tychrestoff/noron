using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Noron.NdArrays;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var asdf = new NdArray(new int[] { 3, 3, 3 }).FillRandom();
            var asdf1 = new NdArray(new int[] { 3, 3, 3 }).FillRandom();

            var asdfasdf = asdf.GreaterThan(asdf1);
        }
    }
}
