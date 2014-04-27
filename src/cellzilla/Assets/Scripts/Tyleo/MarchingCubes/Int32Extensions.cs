namespace Tyleo.MarchingCubes
{
    public static class Int32Extensions
    {
        public static int Clamp(this int @this, int min, int max)
        {
            if (@this >= min)
            {
                if (@this <= max)
                {
                    return @this;
                }
                return max;
            }
            return min;
        }
    }
}
