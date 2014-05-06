namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Provides integer indexes for the three standard axes: X, Y and Z.
    /// </summary>
    public static class AxisIndexProvider
    {
        private const int X_INDEX = 0;
        private const int Y_INDEX = 1;
        private const int Z_INDEX = 2;

        /// <summary>
        /// The index of the X-axis.
        /// </summary>
        public static int XIndex { get { return X_INDEX; } }
        /// <summary>
        /// The index of the Y-axis.
        /// </summary>
        public static int YIndex { get { return Y_INDEX; } }
        /// <summary>
        /// The index of the Z-axis.
        /// </summary>
        public static int ZIndex { get { return Z_INDEX; } }
    }
}
