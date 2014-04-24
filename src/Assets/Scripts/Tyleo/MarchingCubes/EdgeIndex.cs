namespace Tyleo.MarchingCubes
{
    public enum EdgeIndex :
        byte
    {
        ZxNyNz = 0,
        ZxNyPz = 2,
        ZxPyNz = 4,
        ZxPyPz = 6,
        NxZyNz = 9,
        PxZyNz = 8,
        NxZyPz = 10,
        PxZyPz = 11,
        NxNyZz = 1,
        NxPyZz = 5,
        PxNyZz = 3,
        PxPyZz = 7
    }
}
