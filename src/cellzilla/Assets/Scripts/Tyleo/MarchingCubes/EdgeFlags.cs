using System;

namespace Tyleo.MarchingCubes
{
    /// <summary>
    /// Flags which indicate a set of edges.
    /// </summary>
    [Flags]
    public enum EdgeFlags :
        ushort
    {
        /// <summary>
        /// The set containing no edges.
        /// </summary>
        None = 0,

        ZxNyNz = 1 << (byte)EdgeIndex.ZxNyNz,
        ZxNyPz = 1 << (byte)EdgeIndex.ZxNyPz,
        ZxPyNz = 1 << (byte)EdgeIndex.ZxPyNz,
        ZxPyPz = 1 << (byte)EdgeIndex.ZxPyPz,
        NxZyNz = 1 << (byte)EdgeIndex.NxZyNz,
        PxZyNz = 1 << (byte)EdgeIndex.PxZyNz,
        NxZyPz = 1 << (byte)EdgeIndex.NxZyPz,
        PxZyPz = 1 << (byte)EdgeIndex.PxZyPz,
        NxNyZz = 1 << (byte)EdgeIndex.NxNyZz,
        NxPyZz = 1 << (byte)EdgeIndex.NxPyZz,
        PxNyZz = 1 << (byte)EdgeIndex.PxNyZz,
        PxPyZz = 1 << (byte)EdgeIndex.PxPyZz,

        /// <summary>
        /// The set containing all edges.
        /// </summary>
        All =
            ZxNyNz |
            ZxNyPz |
            ZxPyNz |
            ZxPyPz |
            NxZyNz |
            PxZyNz |
            NxZyPz |
            PxZyPz |
            NxNyZz |
            NxPyZz |
            PxNyZz |
            PxPyZz
    }

    /// <summary>
    /// Provides extensions to the EdgeFlags enumeration.
    /// </summary>
    public static class EdgeFlagsExtensions
    {
        /// <summary>
        /// Indicates whether an EdgeFlags contains certain flags.
        /// </summary>
        /// <param name="this">
        /// The EdgeFlags to check.
        /// </param>
        /// <param name="flags">
        /// The EdgeFlags we are checking for.
        /// </param>
        /// <returns>
        /// True if the specified flags are contained.
        /// </returns>
        public static bool HasFlags(this EdgeFlags @this, EdgeFlags flags)
        {
            return (@this & flags) == flags;
        }
    }
}
