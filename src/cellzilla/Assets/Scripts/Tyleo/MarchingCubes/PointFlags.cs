using System;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Flags which indicate a set of points.
    /// </summary>
    [Flags]
    public enum PointFlags :
        byte
    {
        /// <summary>
        /// The set containing no points.
        /// </summary>
        None = 0,

        NxNyNz = 1 << (byte)PointIndex.NxNyNz,
        NxNyPz = 1 << (byte)PointIndex.NxNyPz,
        NxPyNz = 1 << (byte)PointIndex.NxPyNz,
        NxPyPz = 1 << (byte)PointIndex.NxPyPz,
        PxNyNz = 1 << (byte)PointIndex.PxNyNz,
        PxNyPz = 1 << (byte)PointIndex.PxNyPz,
        PxPyNz = 1 << (byte)PointIndex.PxPyNz,
        PxPyPz = 1 << (byte)PointIndex.PxPyPz,

        /// <summary>
        /// The set containing all points.
        /// </summary>
        All =
            NxNyNz |
            NxNyPz |
            NxPyNz |
            NxPyPz |
            PxNyNz |
            PxNyPz |
            PxPyNz |
            PxPyPz
    }
}
