namespace Tyleo.MarchingCubes
{
    public enum EdgeIndex :
        ushort
    {
        None = 0,

        ZxNyNz = None + 1,
        ZxNyPz = ZxNyNz + 1,
        ZxPyNz = ZxNyPz + 1,
        ZxPyPz = ZxPyNz + 1,
        NxZyNz = ZxPyPz + 1,
        PxZyNz = NxZyNz + 1,
        NxZyPz = PxZyNz + 1,
        PxZyPz = NxZyPz + 1,
        NxNyZz = PxZyPz + 1,
        NxPyZz = NxNyZz + 1,
        PxNyZz = NxPyZz + 1,
        PxPyZz = PxNyZz + 1,
    }
}
