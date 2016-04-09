namespace Noron.Utilities
{
    public static class IntArrayUtilities
    {
        public static int Product(this int[] xs)
        {
            var y = 1;
            foreach (var x in xs) y *= x;
            return y;
        }
    }
}
