using System;

namespace Tyleo.MarchingCubes
{
    [Flags]
    public enum PointFlags :
        byte
    {
        None = 0,

        NxNyNz = None + 1,
        NxNyPz = NxNyNz << 1,
        NxPyNz = NxNyPz << 1,
        NxPyPz = NxPyNz << 1,
        PxNyNz = NxPyPz << 1,
        PxNyPz = PxNyNz << 1,
        PxPyNz = PxNyPz << 1,
        PxPyPz = PxPyNz << 1,

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
