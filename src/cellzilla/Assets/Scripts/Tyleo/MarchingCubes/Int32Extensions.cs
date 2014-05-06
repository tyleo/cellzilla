namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Provides extensions to the Int32 struct.
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// Clamps an Int32 between a min and a max.
        /// </summary>
        /// <param name="this">
        /// The Int32 to clamp.
        /// </param>
        /// <param name="min">
        /// The minimum value of the Int32.
        /// </param>
        /// <param name="max">
        /// The maximum value of the Int32.
        /// </param>
        /// <returns>
        /// If the Int32 is less than the min, the min is return, if the Int32 is greater than the
        /// max, the max is returned otherwise the original value is returned.
        /// </returns>
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
