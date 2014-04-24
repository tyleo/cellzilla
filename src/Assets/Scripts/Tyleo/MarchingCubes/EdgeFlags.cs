using System;

namespace Tyleo.MarchingCubes
{
    [Flags]
    public enum EdgeFlags :
        ushort
    {
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

    public static class EdgeFlagsExtensions
    {
        public static bool HasFlags(this EdgeFlags @this, EdgeFlags flags)
        {
            return (@this & flags) == flags;
        }
    }
}
