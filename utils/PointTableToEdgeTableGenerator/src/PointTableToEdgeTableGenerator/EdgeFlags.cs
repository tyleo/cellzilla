using System;

namespace PointTableToEdgeTableGenerator
{
    [Flags]
    public enum EdgeFlags :
        ushort
    {
        None = 0,

        ZxNyNz = 1 << (ushort)EdgeIndex.ZxNyNz,
        ZxNyPz = 1 << (ushort)EdgeIndex.ZxNyPz,
        ZxPyNz = 1 << (ushort)EdgeIndex.ZxPyNz,
        ZxPyPz = 1 << (ushort)EdgeIndex.ZxPyPz,
        NxZyNz = 1 << (ushort)EdgeIndex.NxZyNz,
        PxZyNz = 1 << (ushort)EdgeIndex.PxZyNz,
        NxZyPz = 1 << (ushort)EdgeIndex.NxZyPz,
        PxZyPz = 1 << (ushort)EdgeIndex.PxZyPz,
        NxNyZz = 1 << (ushort)EdgeIndex.NxNyZz,
        NxPyZz = 1 << (ushort)EdgeIndex.NxPyZz,
        PxNyZz = 1 << (ushort)EdgeIndex.PxNyZz,
        PxPyZz = 1 << (ushort)EdgeIndex.PxPyZz,

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

        public static string ToShortString(this EdgeFlags @this)
        {
            switch (@this)
            {
                case EdgeFlags.ZxNyNz:
                    return "ZxNyNz";
                case EdgeFlags.ZxNyPz:
                    return "ZxNyPz";
                case EdgeFlags.ZxPyNz:
                    return "ZxPyNz";
                case EdgeFlags.ZxPyPz:
                    return "ZxPyPz";
                case EdgeFlags.NxZyNz:
                    return "NxZyNz";
                case EdgeFlags.PxZyNz:
                    return "PxZyNz";
                case EdgeFlags.NxZyPz:
                    return "NxZyPz";
                case EdgeFlags.PxZyPz:
                    return "PxZyPz";
                case EdgeFlags.NxNyZz:
                    return "NxNyZz";
                case EdgeFlags.NxPyZz:
                    return "NxPyZz";
                case EdgeFlags.PxNyZz:
                    return "PxNyZz";
                case EdgeFlags.PxPyZz:
                    return "PxPyZz";
                default:
                    throw new System.Exception();
            }
        }
    }
}
